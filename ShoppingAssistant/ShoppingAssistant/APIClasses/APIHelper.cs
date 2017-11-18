using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ModernHttpClient;
using Newtonsoft.Json;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.APIClasses
{
    class APIHelper
    {
        public static APIHelper Helper = new APIHelper();

        HttpClient client;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {NullValueHandling = NullValueHandling.Ignore};

        private APIHelper()
        {
            client = new HttpClient(new NativeMessageHandler());
        }

        public async Task<List<T>> RefreshDataAsync<T>(string url)
        {
            // Get http response
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);
            
            // Return null if not successful
            if (!response.IsSuccessStatusCode) return null;

            // Convert json in http response to a List of T items
            var content = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<T>>(content);
            return items;
        }

        public async Task<bool> DeleteItemAsync<T>(string url) where T : Model
        {
            try
            {
                var uri = new Uri(url);
                var response = await client.DeleteAsync(uri);
                
                return response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound;
            }
            catch (Exception e)
            {
                App.Log.Error("DeleteItemAsync<T>", e.Message + e.GetBaseException().Message);
                return false;
            }
        }

        public async Task SaveItemAsync<T>(T item, string url) where T : Model
        {
            try
            {
                // TODO logging
                if (item.RemoteDbId == null)
                {
                    // Post a new item
                    var uri = new Uri(url);
                    var json = JsonConvert.SerializeObject(item, serializerSettings);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, content);
                
                    if (response.IsSuccessStatusCode)
                    {
                        // Logging
                        App.Log.Info("SaveItemAsync<T>", "Successfully posted item with id " + item.RemoteDbId + " on url " + url);
                    }
                    else
                    {
                        // Logging
                    }
                }
                else
                {
                    // Put/Update the previously existing item
                    url += "/" + item.RemoteDbId;
                    var uri = new Uri(url);
                    var json = JsonConvert.SerializeObject(item, serializerSettings);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Logging
                        App.Log.Info("SaveItemAsync<T>", "Successfully patched item with id " + item.RemoteDbId + " on url " + url);
                    }
                    else
                    {
                        // Logging
                    }
                }

            }
            catch (Exception e)
            {
                App.Log.Error("SaveItemAsync<T>", e.Message + " " + e.GetBaseException().Message);
            }
        }
    }
}
