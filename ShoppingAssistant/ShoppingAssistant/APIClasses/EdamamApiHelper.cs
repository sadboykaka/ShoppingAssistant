using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.APIClasses
{
    public class EdamamApiHelper : ApiHelper
    {
        private readonly string baseUrl;

        public EdamamApiHelper(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<EdamamResponse> Query(string queryString)
        {
            // Create the url
            var url = baseUrl + "?q=" + queryString;

            // Get the json
            var queryResponse = await GetStringResponse(url);

            // Parse the json string to an object
            var content = EdamamResponse.FromJson(queryResponse);

            // Return the response object
            return content;
        }

        public async Task<EdamamResponse> Query(string queryString, long from, long to)
        {
            // Create the url
            var url = baseUrl + "?q=" + queryString + "&from=" + from + "&to=" + to;

            // Get the json
            var queryResponse = await GetStringResponse(url);

            // Parse the json string to an object
            var content = EdamamResponse.FromJson(queryResponse);

            // Return the response object
            return content;
        }
    }
}
