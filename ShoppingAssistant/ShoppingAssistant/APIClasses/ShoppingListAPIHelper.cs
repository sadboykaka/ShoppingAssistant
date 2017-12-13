using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.APIClasses
{
    /// <summary>
    /// ShoppingList specific helper class for the API
    /// </summary>
    class ShoppingListApiHelper
    {

        private readonly LoginAPIHelper helper;
        
        public ShoppingListApiHelper(LoginAPIHelper helper)
        {
            this.helper = helper;
        }

        public async Task<List<ShoppingListModel>> GetShoppingListModelsAsync()
        {
            List<ShoppingListModel> lists = await helper.RefreshDataAsync<ShoppingListModel>(helper.BaseUrl + ShoppingListModel.UrlSuffix);

            lists.ForEach(GetItemQuantityPairModelsAsync);

            return lists;
        }

        public async void GetItemQuantityPairModelsAsync(ShoppingListModel list)
        {
            var url = helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" + list.RemoteDbId + "/" +
                      ItemQuantityPairModel.UrlSuffix;

            var items = await helper.RefreshDataAsync<ItemQuantityPairModel>(url);

            list.AddItems(items);
        }

        public async void SaveShoppingListModelAsync(ShoppingListModel list)
        {
            var url = helper.BaseUrl + ShoppingListModel.UrlSuffix;

            await helper.SaveItemAsync(list, url);

            url += "/" + list.RemoteDbId + "/" + ItemQuantityPairModel.UrlSuffix;

            list.Items.ForEach(item => helper.SaveItemAsync(item, url));
        }

        public async Task<bool> DeleteShoppingListModelAsync<T>(T item) where T : Model
        {
            return await helper.DeleteItemAsync<T>(helper.BaseUrl + "/" + item.UrlSuffixProperty + "/" + item.RemoteDbId);
        }

        /// <summary>
        /// Method to add a new owner with the given email to the given shopping list on the API
        /// </summary>
        /// <param name="list"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> AddShoppingListModelOwnerAsync(ShoppingListModel list, string email)
        {
            var url = helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" + list.RemoteDbId;
            return await helper.PutItemAsync(url, new List<KeyValuePair<string, string>>(1)
            {
                new KeyValuePair<string, string>("email", email)
            });
        }
    }
}
