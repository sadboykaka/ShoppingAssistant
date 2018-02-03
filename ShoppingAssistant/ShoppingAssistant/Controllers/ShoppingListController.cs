using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;

using Xamarin.Forms.Internals;

namespace ShoppingAssistant.Controllers
{
    /// <summary>
    /// ShoppingList Controller class
    /// </summary>
    public class ShoppingListController
    {
        /// <summary>
        /// Const notification title for new shopping lists
        /// </summary>
        private const string NEW_SHOPPING_LIST_TITLE = "You have a new shopping list";

        /// <summary>
        /// Const notification text for new shopping lists
        /// Takes 1 parameter giving the shopping list name
        /// </summary>
        private const string NEW_SHOPPING_LIST_TEXT = "The shopping list {0} has been added to your account";

        /// <summary>
        /// Const notification title for updated shopping lists
        /// </summary>
        private const string UPDATED_SHOPPING_LIST_TITLE = "A shopping list has been updated";

        /// <summary>
        /// Const notification text for updated shopping lists
        /// Takes 1 parameter giving the shopping list name
        /// </summary>
        private const string UPDATED_SHOPPING_LIST_TEXT = "The shopping list {0} has been updated";

        /// <summary>
        /// Static reference to ShoppingList specific database helper class
        /// </summary>
        private static ShoppingListDatabaseHelper databaseHelper;

        /// <summary>
        /// Static reference to ShoppingList specific api helper class
        /// </summary>
        private static ShoppingListApiHelper apiHelper;
        
        /// <summary>
        /// Base of API url to be used
        /// </summary>
        private readonly string baseApiUrl;

        /// <summary>
        /// Observable colleciton of shopping lists
        /// </summary>
        public ObservableCollection<ShoppingListModel> ShoppingListModels { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localDatabaseName">Local database name</param>
        /// <param name="helper">Login API helper object</param>
        public ShoppingListController(string localDatabaseName, LoginApiHelper helper)
        {
            // Create the database and API helper objects
            databaseHelper = new ShoppingListDatabaseHelper(localDatabaseName, true);
            apiHelper = new ShoppingListApiHelper(helper);

            ShoppingListModels = new ObservableCollection<ShoppingListModel>();
        }

        /// <summary>
        /// Asynchronous method that deletes a shopping list
        /// Sets the deleted flag and saves the model locally
        /// Then attempts to delete the model on the API
        /// Deletes from the local database if it is successfully deleted on the API
        /// </summary>
        /// <param name="list"></param>
        public async void DeleteShoppingListAsync(ShoppingListModel list)
        {
            ShoppingListModels.Remove(list);

            list.Deleted = true;
            foreach (var item in list.Items)
            {
                item.Deleted = true;
            }
            
            // Save this item to the database as deleted so that it can be deleted if the api call fails
            databaseHelper.SaveShoppingListAsync(list);

            var deleted = await apiHelper.DeleteShoppingListModelAsync(list);

            if (deleted)
            {
                databaseHelper.DeleteShoppingListAsync(list);
            }
        }
        
        /// <summary>
        /// Asynchronous method to get the shopping lists from both the local database and the API
        /// </summary>
        public async void GetShoppingListModelsAsync()
        {
            try
            {
                OnDatabaseRetrieval(databaseHelper.GetShoppingLists());
                OnApiRetrieval(await apiHelper.GetShoppingListModelsAsync()); 
            }
            catch (Exception e)
            {
                App.Log.Error("GetShoppingListModelsAsync", "Message - " + e.Message + " " + "Source - " + e.Source + e.GetBaseException().Message + "\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// Method to deal with data retrieved form the API
        /// </summary>
        /// <param name="lists"></param>
        private void OnApiRetrieval(IEnumerable<ShoppingListModel> lists)
        {
            foreach (var list in lists)
            {
                var oldList = this.ShoppingListModels.FirstOrDefault(l => l.RemoteDbId == list.RemoteDbId);

                if (oldList == null)
                {
                    // Add the list to the list view
                    this.ShoppingListModels.Add(list);

                    // Save the list to the database asynchronously
                    databaseHelper.SaveShoppingListAsync(list);

                    App.Log.Debug("OnApiRetrieval", "New shopping list retrieved from API");
                    App.NotificationHelper.CreateNotification(NEW_SHOPPING_LIST_TITLE, string.Format(NEW_SHOPPING_LIST_TEXT, list.Name));
                }
                else if (RubyDateParser.Compare(oldList.LastUpdated, list.LastUpdated) <= 0)
                {
                    // Replace the old list with the stored list
                    var index = ShoppingListModels.IndexOf(oldList);
                    ShoppingListModels[index] = list;

                    databaseHelper.DeleteShoppingListAsync(oldList);
                    databaseHelper.SaveShoppingListAsync(list);

                    App.Log.Debug("OnApiRetrieval", $"Found newer version of shopping list {list.Name} on API");
                    if (!oldList.Equals(list))
                    {
                        App.NotificationHelper.CreateNotification(UPDATED_SHOPPING_LIST_TITLE,
                            string.Format(UPDATED_SHOPPING_LIST_TEXT, list.Name));
                    }
                }

                list.Items.Select(item => item.Name).ForEach(App.MasterController.AddItem);
            }

            PushLocalLists();
        }

        /// <summary>
        /// Method to save the local lists on the API
        /// </summary>
        private void PushLocalLists()
        {
            foreach (var list in ShoppingListModels.Where(slist => slist.RemoteDbId == null))
            {
                apiHelper.SaveShoppingListModelAsync(list);
            }
        }

        /// <summary>
        /// Method to deal with data retrieved from the local database
        /// </summary>
        /// <param name="lists"></param>
        private void OnDatabaseRetrieval(IEnumerable<ShoppingListModel> lists)
        {
            foreach (var list in lists)
            {
                if (list.Deleted)
                {
                    DeleteShoppingListAsync(list);
                }

                var oldList = this.ShoppingListModels.FirstOrDefault(l => l.LocalDbId == list.LocalDbId);

                if (oldList == null)
                {
                    // Insert the new list
                    ShoppingListModels.Add(list);
                    list.Items.Select(item => item.Name).ForEach(App.MasterController.AddItem);
                }
                else if (RubyDateParser.Compare(oldList.LastUpdated, list.LastUpdated) < 0)
                {
                    // Replace the old list with the stored list    
                    var index = ShoppingListModels.IndexOf(oldList);
                    ShoppingListModels[index] = list;
                }

                list.Items.Select(item => item.Name).ForEach(App.MasterController.AddItem);
            }
        }
        
        public async void SaveShoppingListModel(ShoppingListModel list)
        {
            SaveShoppingListToDatabase(list);

            await apiHelper.SaveShoppingListModelAsync(list);

            // Resave with remote db id
            SaveShoppingListToDatabase(list);
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
