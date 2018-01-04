using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemQuantityPairModel : ItemIdModel
    {
        /// <summary>
        /// API suffix
        /// </summary>
        public const string UrlSuffix = "iqps";
        
        /// <summary>
        /// Name of the item
        /// </summary>
        [JsonProperty("item")]
        public string Name { get; set; }

        /// <summary>
        /// Shopping list id for the item
        /// </summary>
        [JsonProperty("slist_id")]
        public int LocalDbShoppingListId { get; set; }

        /// <summary>
        /// Quantity required
        /// </summary>
        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        /// <summary>
        /// Measurement of the quantity
        /// </summary>
        [JsonProperty("measure")]
        public string Measure { get; set; }
        
        /// <summary>
        /// String concatenation of the quantity and measurement
        /// </summary>
        [Ignore]
        public string QuantityMeasure => Quantity + " " + Measure;
    }
}
