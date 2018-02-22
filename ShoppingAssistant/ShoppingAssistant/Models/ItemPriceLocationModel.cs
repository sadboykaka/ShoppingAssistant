using Newtonsoft.Json;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Item Price Location Model
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemPriceLocationModel : Model
    {
        /// <summary>
        /// API suffix
        /// </summary>
        public const string UrlSuffix = "ipls";

        /// <summary>
        /// Name of the Item
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("item")]
        public string Name { get; set; }

        /// <summary>
        /// Remote database location identifier
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("location_id")]
        public int RemoteDbLocationId { get; set; }

        /// <summary>
        /// Price of the item
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("price")]
        public float Price { get; set; }
       
        /// <summary>
        /// Quantity attribute
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        /// <summary>
        /// Measurement attribute
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("measure")]
        public string Measure { get; set; }
            
        /// <summary>
        /// Local database location id
        /// Stored in the local database
        /// </summary>
        public int LocalDbLocationId { get; set; }

        /// <summary>
        /// Image url
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}
