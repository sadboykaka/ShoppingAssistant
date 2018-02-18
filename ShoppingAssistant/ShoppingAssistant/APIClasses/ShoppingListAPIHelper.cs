using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.APIClasses
{
    /// <summary>
    /// ShoppingList specific helper class for the API
    /// </summary>
    class ShoppingListApiHelper
    {

        private readonly LoginApiHelper helper;
        
        public ShoppingListApiHelper(LoginApiHelper helper)
        {
            this.helper = helper;
        }
        
        public async Task<List<ShoppingListModel>> GetShoppingListModelsAsync()
        {
            List<ShoppingListModel> lists = await helper.RefreshDataAsync<ShoppingListModel>(helper.BaseUrl + ShoppingListModel.UrlSuffix);

            foreach (var list in lists)
            {
                await GetItemQuantityPairModelsAsync(list);
            }

            return lists;
        }

        public async Task GetItemQuantityPairModelsAsync(ShoppingListModel list)
        {
            try
            {
                var url = helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" + list.RemoteDbId + "/" +
                          ItemQuantityPairModel.UrlSuffix;

                var items = await helper.RefreshDataAsync<ItemQuantityPairModel>(url);

                list.AddItems(items);
            }
            catch (Exception e)
            {
                App.Log.Error("GetItemQuantityPairModelsAsync", e.StackTrace);
            }
        }

        public async Task SaveShoppingListModelAsync(ShoppingListModel list)
        {
            try
            {
                var url = helper.BaseUrl + ShoppingListModel.UrlSuffix;

                var slistResponse = await helper.SaveItemAsync(list, url);

                list.RemoteDbId = list.RemoteDbId ?? slistResponse?.RemoteDbId;

                url += "/" + list.RemoteDbId + "/" + ItemQuantityPairModel.UrlSuffix;

                foreach (var iqp in list.Items)
                {
                    var iqpResponse = await helper.SaveItemAsync(iqp, url);
                    if (iqp.RemoteDbId == null)
                    {
                        iqp.RemoteDbId = iqpResponse?.RemoteDbId;
                    }
                }
            }
            catch (Exception ex)
            {
                App.Log.Error("SaveShoppingListModelAsync", ex.Message + "\n" + ex.StackTrace);
            }
        }

        public async Task<bool> DeleteItemQuantityPairModelAsync(ItemQuantityPairModel item)
        {
            if (item.RemoteDbId == null) return true;
            return await helper.DeleteItemAsync(helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" +
                                                ItemQuantityPairModel.UrlSuffix + "/" + item.RemoteDbId);
        }

        public async Task<bool> DeleteShoppingListModelAsync<T>(T item) where T : Model
        {
            if (item.RemoteDbId == null) return false;
            return await helper.DeleteItemAsync(helper.BaseUrl + item.UrlSuffixProperty + "/" + item.RemoteDbId);
        }

        /// <summary>
        /// Method to add a new owner with the given email to the given shopping list on the API
        /// </summary>
        /// <param name="list"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> AddShoppingListModelOwnerAsync(ShoppingListModel list, string email)
        {
            var url = helper.BaseUrl + ListOwnerModel.UrlSuffix;
            return await helper.SaveItemAsync(url, new List<KeyValuePair<string, string>>(1)
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("slist_id", list.RemoteDbId.ToString())
            });

            //var url = helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" + list.RemoteDbId;
            //return await helper.PutItemAsync(url, new List<KeyValuePair<string, string>>(1)
            //{
            //    new KeyValuePair<string, string>("email", email)
            //});
        }
    }
}
