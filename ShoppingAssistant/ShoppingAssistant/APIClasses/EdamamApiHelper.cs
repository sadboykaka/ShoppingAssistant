using System;
using System.Threading.Tasks;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.APIClasses
{
    /// <summary>
    /// Helper class for interacting with the Edamam API
    /// </summary>
    public class EdamamApiHelper : ApiHelper
    {
        /// <summary>
        /// Application id key string
        /// </summary>
        private const string AppIdKey = "app_id=";

        /// <summary>
        /// Application key key string
        /// </summary>
        private const string AppKeyKey = "app_key=";

        /// <summary>
        /// Query key string
        /// </summary>
        private const string QueryKey = "q=";

        /// <summary>
        /// From key string
        /// </summary>
        private const string FromKey = "from=";

        /// <summary>
        /// To key string
        /// </summary>
        private const string ToKey = "to=";

        /// <summary>
        /// NextParam string
        /// </summary>
        private const string NextParam = "&";

        /// <summary>
        /// Params string
        /// </summary>
        private const string Params = "?";

        /// <summary>
        /// Application id
        /// </summary>
        private const string AppId = "08c12cba";

        /// <summary>
        /// Application key
        /// </summary>
        private const string AppKey = "e8f06aed445afa549d8d3f7c88df4447";

        /// <summary>
        /// The base api url
        /// </summary>
        private readonly string baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl"></param>
        public EdamamApiHelper(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Method to query the api using the given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<EdamamResponse> QueryUrlAsync(string url)
        {
            try
            {
                // Get the json
                var queryResponse = await GetStringResponse(url);

                // Parse the json string to an object
                var content = EdamamResponse.FromJson(queryResponse);

                // Return the response object
                return content;
            }
            catch (Exception e)
            {
                App.Log.Error("EdamamQueryUrlAsync", "Could not query url " + url + "\n" + e.StackTrace + "\n" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Method to query the api with the given string
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<EdamamResponse> QueryAsync(string queryString)
        {
            // Create the url
            var url = baseUrl + Params + AppIdKey + AppId + NextParam + AppKeyKey + AppKey + NextParam + QueryKey + queryString;
            
            // Return the response object
            return await QueryUrlAsync(url);
        }

        /// <summary>
        /// Method to query the api with the given string, from, and to values
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public async Task<EdamamResponse> QueryAsync(string queryString, long from, long to)
        {
            // Create the url
            var url = baseUrl + Params + AppIdKey + AppId + NextParam + AppKeyKey + AppKey + NextParam + QueryKey +
                        queryString + NextParam + FromKey + from + NextParam + ToKey + to;
            
            // Return the response object
            return await QueryUrlAsync(url);
        }
    }
}
