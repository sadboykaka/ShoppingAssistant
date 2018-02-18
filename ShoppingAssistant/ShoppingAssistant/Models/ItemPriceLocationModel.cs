using Newtonsoft.Json;

namespace ShoppingAssistant.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemPriceLocationModel : ItemIdModel
    {
        public const string UrlSuffix = "ipls";

        [JsonProperty("item")]
        public string Name { get; set; }
        [JsonProperty("location_id")]
        public int RemoteDbLocationId { get; set; }
        [JsonProperty("price")]
        public float Price { get; set; }
       
        /// <summary>
        /// Quantity attribute
        /// </summary>
        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("measure")]
        public string Measure { get; set; }
            
        public int LocalDbLocationId { get; set; }

        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}
