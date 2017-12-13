using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;

namespace ShoppingAssistant.Models
{
    public class ItemQuantityPairModel : ItemIdModel
    {
        public const string UrlSuffix = "iqps";
        
        [JsonProperty("item")]
        public string Name { get; set; }
        [JsonProperty("slist_id")]
        public int LocalDbShoppingListId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
