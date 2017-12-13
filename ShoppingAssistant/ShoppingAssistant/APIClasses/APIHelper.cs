using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using ModernHttpClient;
using Newtonsoft.Json;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.APIClasses
{
    public enum LoginResponse
    {
        Success,
        InvalidCredentials,
        NoResponse
    }
    
    public class APIHelper
    {
        public static APIHelper Helper = new APIHelper();

        protected HttpClient client;

        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {NullValueHandling = NullValueHandling.Ignore};

        public APIHelper()
        {
            client = new HttpClient(new NativeMessageHandler());
        }

        public async Task<List<T>> RefreshDataAsync<T>(T model, string baseurl) where T : Model
        {
            return await this.RefreshDataAsync<T>(baseurl + "/" + model.UrlSuffixProperty + "/" + model.RemoteDbId);
        }

        /// <summary>
        /// Method to perform a PUT request at the given url with the given url parameters
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlparams"></param>
        /// <returns></returns>
        public async Task<bool> PutItemAsync(string url, IEnumerable<KeyValuePair<string, string>> urlparams)
        {
            UriBuilder builder = new UriBuilder(url);

            // Create query string using urlparams
            StringBuilder sb = new StringBuilder();
            foreach (var param in urlparams)
            {
                sb.Append(param.Key + "=" + param.Value + "&");
            }

            // Remove the last ampersand
            sb.Remove(sb.Length - 1, 1);

            builder.Query = sb.ToString();

            // Get response
            // Null content as we only want to send the email parameter, not an entire object
            try
            {
                var response = await client.PutAsync(builder.Uri, new StringContent(JsonConvert.Null, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                App.Log.Error("PutItemAsync", "Uri = " + builder.Uri + "\n" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        protected async Task<List<T>> RefreshDataAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> urlparams)
        {
            UriBuilder builder = new UriBuilder(url);

            // Create query string using urlparams
            StringBuilder sb = new StringBuilder();
            foreach (var param in urlparams)
            {
                sb.Append(param.Key + "=" + param.Value + "&");
            }

            // Remove the last ampersand
            sb.Remove(sb.Length - 1, 1);

            builder.Query = sb.ToString();
            
            // Get response
            var response = await client.GetAsync(builder.Uri);
            
            // Return null if not successful
            if (!response.IsSuccessStatusCode) return null;

            // Convert json in http response to a List of T items
            var content = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<T>>(content);
            return items;   
        }

        protected async Task<List<T>> RefreshDataAsync<T>(string url)
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

        public async Task<bool> DeleteItemAsync<T>(T model, string baseUrl) where T : Model
        {
            return await DeleteItemAsync<T>(baseUrl + "/" + model.UrlSuffixProperty + "/" + model.RemoteDbId);
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

        protected async Task<LoginResponse> Register(UserModel login, string url)
        {
            try
            {
                // Post new user info
                var uri = new Uri(url);
                var payload = JsonConvert.SerializeObject(login, serializerSettings);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    App.Log.Info("Register", "Successfully registered account for " + login.Email);

                    // Convert json in http response to an auth token
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var authToken = stringResponse.Remove(0, 56);
                    authToken = authToken.Replace("\"", string.Empty);
                    authToken = authToken.Replace("}", string.Empty);
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Authorization", authToken);

                    return LoginResponse.Success;
                }
                else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    App.Log.Info("Register", "Could not connect to API server");
                    return LoginResponse.NoResponse;
                }
                else
                {
                    App.Log.Info("Register", "Could not register user " + login.Email);
                    return LoginResponse.InvalidCredentials;
                }
            }
            catch (Exception e)
            {
                App.Log.Error("Register", e.Message + " " + e.GetBaseException().Message);
                return LoginResponse.NoResponse;
            }
        }

        protected async Task<LoginResponse> Login(UserModel login, string url)
        {
            try
            {
                // Post login info
                var uri = new Uri(url);
                var json = JsonConvert.SerializeObject(login, serializerSettings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    // Logging
                    App.Log.Info("Login", "Successfully logged in user " + login.Email);

                    // Convert json in http response to a List of T items
                    var authToken = await response.Content.ReadAsStringAsync();
                    authToken = authToken.Substring(15);
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Authorization", authToken);
                    return LoginResponse.Success;
                }
                else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    App.Log.Info("Login", "Could not connect to API server");
                    return LoginResponse.NoResponse;
                }
                else
                {
                    // Logging
                    App.Log.Info("Login", "Could not log in user " + login.Email + ". Invalid credentials");
                    return LoginResponse.InvalidCredentials;
                }
            }
            catch (Exception e)
            {
                App.Log.Error("Login", e.Message + " " + e.GetBaseException().Message);
                return LoginResponse.NoResponse;
            }
        }
    }
}
