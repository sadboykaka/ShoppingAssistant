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
            // Get list of ShoppingListModels and ItemQuantityPairModels
            var lists = GetItemsAsync<ShoppingListModel>().Result;
            var items = GetItemsAsync<ItemQuantityPairModel>().Result;

            // Create output list
            //var lists = new List<ShoppingListModel>();

            // Create the ShoppingList objects
            //models.ForEach(m => lists.Add(new ShoppingListModel(m)));

            //lists.AddRange(models);

            // Populate the item quantity pairs
            //foreach (var item in items)
            //{
            //    var list = lists.Where(l => l.LocalDbId == )
            //}
            items.ForEach(i => lists.FirstOrDefault(l => l.RemoteDbId == i.RemoteDbShoppingListId)?.Items.Add(i));
            //lists.ForEach(l => items.Where(i => i.ShoppingListID == l.LocalDbId).ForEach(l.Items.Add(i)));


            return lists;
        }


        public Task<int> SaveShoppingListAsync(ShoppingListModel list)
        {

            // Save the item quantity pairs
            list.Items.ForEach(item => SaveItemsAsync(item));

            // Return and save the shopping list model object
            return SaveItemsAsync(list);
        }
    }
}
