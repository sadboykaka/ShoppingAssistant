using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.DatabaseClasses
{
    class ShoppingAssistantDatabaseHelper : DatabaseHelper
    {
        public ShoppingAssistantDatabaseHelper(string dbPath, bool createTables) : base(dbPath)
        {
            if (createTables)
            {
                this.CreateDatabases();
            }
        }

        private void CreateDatabases()
        {
            //DatabaseAsyncConnection.DropTableAsync<ItemQuantityPairModel>();
            //DatabaseAsyncConnection.DropTableAsync<ShoppingListModel>();



            DatabaseAsyncConnection.CreateTableAsync<LocationModel>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK).Wait();
            DatabaseAsyncConnection.CreateTableAsync<ShoppingListModel>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK).Wait();
            DatabaseAsyncConnection.CreateTableAsync<ItemQuantityPairModel>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK).Wait();
            DatabaseAsyncConnection
                .CreateTableAsync<ListOwnerModel>(SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK).Wait();
        }

        public void DeleteShoppingListAsync(ShoppingListModel list)
        {
            try
            {
                list.Items.ForEach(item => DeleteItemAsync(item));

                DeleteItemAsync(list);
            }
            catch (Exception e)
            {
                App.Log.Error("DeleteShoppingListAsync", e.Message + e.GetBaseException().Message);
            }
        }

        /// <summary>
        /// Method to return a list of shopping lists created from data stored across tables in the database
        /// </summary>
        /// <returns>List of shopping lists stored in the database</returns>
        public List<ShoppingListModel> GetShoppingLists()
        {
            // Get the ListOwnerModels and ShoppingListModels
            var listowners = GetItemsAsync<ListOwnerModel>().Result;
            var lists = GetItemsAsync<ShoppingListModel>().Result;
               
            // Select the ListOwnerModels relevant to the current user
            listowners = listowners.Where(listowner =>
                listowner.UserModelId == App.ModelManager.LoginController.CurrentUser.LocalDbId).ToList();

            // Select the lists that belong to the current owner
            lists = lists.Where(slist => listowners.Any(listowner => listowner.ShoppingListModelId == slist.LocalDbId))
                .ToList();

            // Get the ItemQuantityPairModels
            var items = GetItemsAsync<ItemQuantityPairModel>().Result;
            
            // Select the ItemQuantityPairModels and attach them to the relevant ShoppingListModels
            items.ForEach(i => lists.FirstOrDefault(l => l.RemoteDbId == i.RemoteDbShoppingListId)?.Items.Add(i));
            
            // Return the ShoppingListModels
            return lists;
        }


        public async Task SaveShoppingListAsync(ShoppingListModel list)
        {
            var user = App.ModelManager.LoginController.CurrentUser;

            // Save the item quantity pairs
            list.Items.ForEach(item => SaveItemsAsync(item));

            // Return and save the shopping list model object
            await SaveItemsAsync(list);

            // Create the ListOwnerModel if required
            var listOwnerModel = new ListOwnerModel()
            {
                ShoppingListModelId = list.LocalDbId.Value,
                UserModelId = user.LocalDbId.Value
            };

            SaveItemsAsync(listOwnerModel);
        }
    }
}
