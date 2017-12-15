using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;

namespace ShoppingAssistant.Controllers
{
    /// <summary>
    /// ShoppingList Controller class
    /// </summary>
    public class ShoppingListController
    {
        /// <summary>
        /// Static reference to ShoppingList specific database helper class
        /// </summary>
        private static ShoppingListDatabaseHelper databaseHelper;

        /// <summary>
        /// Static reference to ShoppingList specific api helper class
        /// </summary>
        private static ShoppingListApiHelper apiHelper;

        public ObservableCollection<ShoppingListModel> ShoppingListModels { get; }
        
        private string localDatabaseName;

        private string baseApiUrl;

        public ShoppingListController(string localDatabaseName, LoginApiHelper helper)
        {
            this.localDatabaseName = localDatabaseName;
            this.baseApiUrl = baseApiUrl;

            databaseHelper = new ShoppingListDatabaseHelper(this.localDatabaseName, true);
            apiHelper = new ShoppingListApiHelper(helper);

            this.ShoppingListModels = new ObservableCollection<ShoppingListModel>();
        }

        public async void DeleteShoppingListAsync(ShoppingListModel list)
        {
            this.ShoppingListModels.Remove(list);

            list.Deleted = true;
            foreach (var item in list.Items)
            {
                item.Deleted = true;
            }
            
            // Save this item to the database as deleted so that it can be deleted if the api call fails
            databaseHelper.SaveShoppingListAsync(list);

            bool deleted = await apiHelper.DeleteShoppingListModelAsync(list);

            if (deleted) databaseHelper.DeleteShoppingListAsync(list);
        }

        public void DeleteShoppingListAsync(int index)
        {
            this.DeleteShoppingListAsync(this.ShoppingListModels[index]);
        }

        public async void GetShoppingListModelsAsync()
        {
            System.Diagnostics.Debug.WriteLine("Getting shopping list models");
            try
            {

                this.AddShoppingLists(databaseHelper.GetShoppingLists());

                var apiShoppingListModels = await apiHelper.GetShoppingListModelsAsync();
                apiShoppingListModels.ForEach(SaveShoppingListToDatabase);

                this.AddShoppingLists(apiShoppingListModels);
            }
            catch (Exception e)
            {
                App.Log.Error("GetShoppingListModels()", "Message - " + e.Message + " " + "Source - " + e.Source + e.GetBaseException().Message);
            }
        }
        
        public void AddShoppingLists(IEnumerable<ShoppingListModel> lists)
        {
            if (lists != null)
            {
                foreach (var list in lists)
                {
                    if (list.Deleted)
                    {
                        this.DeleteShoppingListAsync(list);
                        break;
                    }

                    var oldList = this.ShoppingListModels.FirstOrDefault(l => l.RemoteDbId == list.RemoteDbId);
                    if (oldList == null)
                    {
                        // Insert the list as no list with the same RemoteDbId could be found
                        this.ShoppingListModels.Add(list);
                    }
                    else if (RubyDateParser.Compare(oldList.LastUpdated, list.LastUpdated) < 0)
                    {
                        // Replace the old list with the new if it was last updated more recently
                        oldList = list;
                    }
                }
            }
        }

        public void SaveShoppingListModel(ShoppingListModel list)
        {
            SaveShoppingListToDatabase(list);
            apiHelper.SaveShoppingListModelAsync(list);
        }

        /// <summary>
        /// Method to save a ShoppingList to the database
        /// </summary>
        /// <param name="list"></param>
        private void SaveShoppingListToDatabase(ShoppingListModel list)
        {
            databaseHelper.SaveShoppingListAsync(list);
        }

        /// <summary>
        /// Method to add a new owner to the given ShoppingList
        /// </summary>
        /// <param name="list"></param>
        /// <param name="newUserEmail"></param>
        public async Task<bool> AddOwnerAsync(ShoppingListModel list, string newUserEmail)
        {
            var apiResponse = await apiHelper.AddShoppingListModelOwnerAsync(list, newUserEmail);

            // Create a ListOwnerModel in the local database if the api responds with success
            if (apiResponse)
            {
                databaseHelper.SaveItemsAsync<ListOwnerModel>(new ListOwnerModel()
                {
                    ShoppingListModelId = list.LocalDbId.Value,
                    UserEmail = newUserEmail
                });   
            }

            return apiResponse;
        }
    }
}
