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
        private List<ListOwnerModel> listOwners;

        public ShoppingAssistantDatabaseHelper(string dbPath, bool createTables) : base(dbPath)
        {
            if (createTables)
            {
                this.CreateDatabases();
            }
        }

        private void CreateDatabases()
        {
            //DatabaseAsyncConnection.DropTableAsync<ShoppingListModel>();
            //DatabaseAsyncConnection.DropTableAsync<ItemQuantityPairModel>();
            //DatabaseAsyncConnection.DropTableAsync<ListOwnerModel>();


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
            listOwners = GetItemsAsync<ListOwnerModel>().Result;
            var lists = GetItemsAsync<ShoppingListModel>().Result;

            // Select the ListOwnerModels relevant to the current user
            listOwners = listOwners.Where(listowner =>
                listowner.UserEmail == App.ModelManager.LoginController.CurrentUser.Email).ToList();

            // Select the lists that belong to the current owner
            lists = lists.Where(slist => listOwners.Any(listowner => listowner.ShoppingListModelId == slist.LocalDbId))
                .ToList();

            // Get the ItemQuantityPairModels
            var items = GetItemsAsync<ItemQuantityPairModel>().Result;
            
            // Select the ItemQuantityPairModels and attach them to the relevant ShoppingListModels
            items.ForEach(i => lists.FirstOrDefault(l => l.LocalDbId == i.LocalDbShoppingListId)?.Items.Add(i));
            
            // Return the ShoppingListModels
            return lists;
        }


        public async Task SaveShoppingListAsync(ShoppingListModel list)
        {
            var user = App.ModelManager.LoginController.CurrentUser;

            // Return and save the shopping list model object
            await SaveItemsAsync(list);
            
            // Save the item quantity pairs
            foreach (var item in list.Items)
            {
                item.LocalDbShoppingListId = list.LocalDbId.Value;
                SaveItemsAsync(item);
            }
            
            // Create the ListOwnerModel if required
            var listOwnerModel = new ListOwnerModel()
            {
                ShoppingListModelId = list.LocalDbId.Value,
                UserEmail = user.Email
            };

            if (!listOwners.Any(lo =>
                lo.UserEmail == listOwnerModel.UserEmail &&
                lo.ShoppingListModelId == listOwnerModel.ShoppingListModelId))
            {
                SaveItemsAsync(listOwnerModel);
            }
        }
    }
}
