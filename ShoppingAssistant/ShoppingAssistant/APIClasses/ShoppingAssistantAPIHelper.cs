using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.APIClasses
{
    class ShoppingAssistantAPIHelper
    {
        private readonly LoginAPIHelper helper;
        

        public ShoppingAssistantAPIHelper(LoginAPIHelper helper)
        {
            this.helper = helper;
        }

        public async Task<List<ShoppingListModel>> GetShoppingListModelsAsync()
        {
            List<ShoppingListModel> lists = await helper.RefreshDataAsync<ShoppingListModel>(helper.BaseUrl + ShoppingListModel.UrlSuffix);

            lists.ForEach(GetItemQuantityPairModelsAsync);
            //lists.ForEach(list => list.AddItems(GetItemQuantityPairModelsAsync(list).Result));

            return lists;
        }

        public async void GetItemQuantityPairModelsAsync(ShoppingListModel list)
        {
            var url = helper.BaseUrl + ShoppingListModel.UrlSuffix + "/" + list.RemoteDbId + "/" +
                      ItemQuantityPairModel.UrlSuffix;

            var items = await helper.RefreshDataAsync<ItemQuantityPairModel>(url);

            list.AddItems(items);
            return;
            //return await helper.RefreshDataAsync<ItemQuantityPairModel>(url);
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
    }
}
