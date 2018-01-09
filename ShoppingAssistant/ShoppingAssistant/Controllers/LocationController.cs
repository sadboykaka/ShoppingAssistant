using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;
using XLabs;
using XLabs.Platform.Services.Geolocation;

namespace ShoppingAssistant.Controllers
{
    /// <summary>
    /// Controller class for the LocationModels
    /// Gets LocationMOdels from the local database and API
    /// </summary>
    public class LocationController
    {
        /// <summary>
        /// Static reference to database helper class
        /// </summary>
        private static LocationModelDatabaseHelper databaseHelper;

        /// <summary>
        /// Static reference to api helper class
        /// </summary>
        private static LocationModelAPIHelper apiHelper;

        /// <summary>
        /// GeolocationController class that gets current location using platofmr specific methods
        /// </summary>
        private readonly GeolocationController geolocationController = App.GeolocationController;

        /// <summary>
        /// Collection of LocationModels
        /// </summary>
        public ObservableCollection<LocationModel> LocationModels { get; private set; }

        private string localDatabaseName;

        private string baseApiUrl;

        public LocationController(string localDatabaseName, string baseApiUrl, ApiHelper apiHelperParam)
        {
            this.localDatabaseName = localDatabaseName;
            this.baseApiUrl = baseApiUrl;

            databaseHelper = new LocationModelDatabaseHelper(this.localDatabaseName, true);
            apiHelper = new LocationModelAPIHelper(baseApiUrl, apiHelperParam);

            this.LocationModels = new ObservableCollection<LocationModel>();

            this.geolocationController.NewPositionEvent += NewPositionEventHandler;
            this.geolocationController.GetCurrentLocation();
            
        }
        
        public void GetLocationModelsAsync()
        {
            // Get the current position
            this.geolocationController.GetCurrentLocation();
            
        }

        private async void NewPositionEventHandler(object sender, PositionEventArgs args)
        {
            try
            {
                // Get the location models for the current location
                this.AddLocationModels(databaseHelper.GetLocationModels());
                this.SaveAndAddLocationModels(await apiHelper.GetLocationModelsAsync(
                    this.geolocationController.Position.Latitude,
                    this.geolocationController.Position.Longitude));
            }
            catch (Exception ex)
            {
                App.Log.Error("NewPositionEventHandler", ex.Message + "\n" + ex.GetBaseException().StackTrace);
            }
        }

        private void SaveAndAddLocationModels(IEnumerable<LocationModel> models)
        {
            models.ForEach(databaseHelper.SaveLocationModelAsync);

            AddLocationModels(models);
        }

        /// <summary>
        /// Method to delete the given LocationModel from each database
        /// </summary>
        /// <param name="model"></param>
        private async void DeleteModelAsync(LocationModel model)
        {
            // Remove from the ObservableCollection
            LocationModels.Remove(model);

            // Set the delete flag
            model.Deleted = true;

            // Save this item to the database as deleted so that it can be deleted if the api call fails
            databaseHelper.SaveItemsAsync(model);

            // Try to delete this item from the API database, await response befoe continuing this exectuion
            bool deleted = await apiHelper.DeleteLocationModelAsync(model);

            // Delete the item from the local database if successfully deleted from API database
            if (deleted) databaseHelper.DeleteItemAsync(model);
        }

        /// <summary>
        /// Add locations to the LocationModel observable collection
        /// </summary>
        /// <param name="models"></param>
        private void AddLocationModels(IEnumerable<LocationModel> models)
        {
            // Check models is not null
            if (models == null) return;

            foreach (var model in models)
            {
                // Check if the item should have been deleted (but has not been)
                if (model.Deleted)
                {
                    // Delete the item properly without adding it to the collection
                    this.DeleteModelAsync(model);
                    break;
                }

                // Calculate the distance for this model
                model.Distance = CalculateDistance(
                    geolocationController.Position.Latitude,
                    geolocationController.Position.Longitude,
                    model.Latitude,
                    model.Longitude);

                // Find the first location model with the same RemoteDbId (if it exists)
                var oldList = this.LocationModels.FirstOrDefault(l => l.RemoteDbId == model.RemoteDbId);
                if (oldList == null)
                {
                    // Insert the list as no list with the same RemoteDbId could be found
                    this.LocationModels.Add(model);

                    // Add all the items to the Items collection
                    model.ItemPriceLocations.Select(i => i.Name).ForEach(App.MasterController.AddItem);
                }
                else if (RubyDateParser.Compare(oldList.LastUpdated, model.LastUpdated) < 0)
                {
                    // Replace the old list with the new if it was last updated more recently
                    oldList = model;
                }
            }
        }

        public void SaveLocationModel(LocationModel location)
        {
            databaseHelper.SaveLocationModelAsync(location);
            apiHelper.SaveLocationModelAsync(location);
        }
            
        /// <summary>
        /// Method that returns an approximate distance between two latitude and longitude values using equirectangular approximation
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        private static double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
            /*
                x = Δλ ⋅ cos φm
                y = Δφ
                d = R ⋅ √x² + y²

                where R is the radius of the earth
             */

            // Radius of earth - 6371km
            double R = 6371;

            // Latitude - could be negative but is squared later so does not matter
            var y = lat1 - lat2;

            // Longitude - could be negative but is squared later so does not matter
            var x = (lng1 - lng2) * Math.Cos((lng1 + lng2) / 2);

            // Get the distance in metres
            var distance = Math.Round(R * Math.Sqrt((x * x) + (y * y)), 0);

            // Return the distance in kilometres
            return distance / 100;
        }
    }
}
