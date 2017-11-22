using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using Xamarin.Forms;

namespace ShoppingAssistant.Models
{
    public class ShoppingListModelManager
    {
        private static ShoppingAssistantDatabaseHelper databaseHelper;

        private static ShoppingAssistantAPIHelper apiHelper;

        public ObservableCollection<ShoppingListModel> ShoppingListModels { get; }

        private string localDatabaseName;

        private string baseApiUrl;

        public ShoppingListModelManager(string localDatabaseName, string baseApiUrl)
        {
            this.localDatabaseName = localDatabaseName;
            this.baseApiUrl = baseApiUrl;

            databaseHelper = new ShoppingAssistantDatabaseHelper(this.localDatabaseName, true);
            apiHelper = new ShoppingAssistantAPIHelper(baseApiUrl);

            this.ShoppingListModels = new ObservableCollection<ShoppingListModel>();
            this.GetShoppingListModelsAsync();
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
                this.AddShoppingLists(await apiHelper.GetShoppingListModelsAsync());
            }
            catch (Exception e)
            {
                App.Log.Error("GetShoppingListModels()", "Meesage - " + e.Message + " " + "Source - " + e.Source + e.GetBaseException().Message);
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
            databaseHelper.SaveShoppingListAsync(list);
            apiHelper.SaveShoppingListModelAsync(list);
        }
    }
}
