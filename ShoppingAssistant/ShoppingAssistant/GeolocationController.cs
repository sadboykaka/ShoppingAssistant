using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using XLabs.Platform.Services.Geolocation;

namespace ShoppingAssistant
{
    /// <summary>
    /// PositionEvent delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void PositionEvent(object sender, PositionEventArgs args);

    /// <summary>
    /// Geolocation controller class that deals with getting the current location of the device
    /// Raises a PositionEvent when new position data has been retrieved
    /// </summary>
    public class GeolocationController
    {
        /// <summary>
        /// IGeolocator - device specific implementation
        /// </summary>
        private IGeolocator geolocator;

        /// <summary>
        /// Last known position property
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        /// Raised when a new position is determined
        /// </summary>
        public event PositionEvent NewPositionEvent;

        /// <summary>
        /// Constructor
        /// </summary>
        public GeolocationController()
        {
            this.geolocator = DependencyService.Get<IGeolocator>();
        }
        
        /// <summary>
        /// Asynchronously gets the current position
        /// Raises a PositionEvent on the NewPositionEvent handler
        /// </summary>
        public async void GetCurrentLocation()
        {
            try
            {
                if (this.geolocator == null)
                {
                    App.Log.Debug("GetNearbyLocations", "Geolocation null, check application has platform specific dependency injection and permissions");
                    return;
                }

                if (!this.geolocator.IsGeolocationAvailable)
                {
                    App.Log.Debug("GetNearbyLocations", "Geolocation not available");
                    return;
                }

                if (!this.geolocator.IsGeolocationEnabled)
                {
                    App.Log.Debug("GetNearbyLocations", "Geolocation not enabled");
                    return;
                }

                App.Log.Debug("GetNearbyLocations", "Geolocation is available, getting location");

                // Get position
                this.Position = await this.geolocator.GetPositionAsync(10000);

                // Logging
                App.Log.Debug("GetNearbyLocations", "Lat = " + this.Position.Latitude);
                App.Log.Debug("GetNearbyLocations", "Loc = " + this.Position.Longitude);

                // Raise new position event
                this.NewPositionEvent?.Invoke(this, new PositionEventArgs(this.Position));
            }
            catch (Exception e)
            {
                App.Log.Error("GetNearbyLocations", e.Message + e.StackTrace);
            }
        }
    }
}
