using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.APIClasses
{
    public class LocationModelAPIHelper
    {
        private readonly LoginAPIHelper helper;
        private readonly string baseUrl;

        public LocationModelAPIHelper(LoginAPIHelper helper)
        {
            this.helper = helper;
            this.baseUrl = baseUrl;
        }

        public async Task<List<LocationModel>> GetLocationModelsAsync(double lat, double lng)
        {
            var locations = await helper.RefreshDataAsync<LocationModel>(
                baseUrl + LocationModel.UrlSuffix,
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("lat", lat.ToString()),
                    new KeyValuePair<string, string>("lng", lng.ToString())
                }
            );

            locations.ForEach(GetItemPriceLocationModelsAsync);

            return locations;
        }

        public async void GetItemPriceLocationModelsAsync(LocationModel location)
        {
            var url = baseUrl + LocationModel.UrlSuffix + "/" + location.RemoteDbId + "/" +
                      ItemPriceLocationModel.UrlSuffix;

            var ipls = await helper.RefreshDataAsync<ItemPriceLocationModel>(url);

            location.AddItems(ipls);
        }

        public async void SaveLocationModelAsync(LocationModel location)
        {
            var url = baseUrl + LocationModel.UrlSuffix;

            await helper.SaveItemAsync(location, url);

            url += "/" + location.RemoteDbId + "/" + ItemPriceLocationModel.UrlSuffix;

            location.ItemPriceLocations.ForEach(item => helper.SaveItemAsync(item, url));
        }

        public async Task<bool> DeleteLocationModelAsync(LocationModel location)
        {
            return await helper.DeleteItemAsync<LocationModel>(baseUrl + "/" + location.UrlSuffixProperty + "/" + location.RemoteDbId);
        }
    }
}
