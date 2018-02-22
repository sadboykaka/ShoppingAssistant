using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Location Model
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class LocationModel : Model
    {
        /// <summary>
        /// Private latitiude value between -90 and 90 degrees
        /// </summary>
        private float latitude;

        /// <summary>
        /// Private longitude value between -180 and 180 degrees
        /// </summary>
        private float longitude;

        /// <summary>
        /// API suffix
        /// </summary>
        public const string UrlSuffix = "locations";

        /// <summary>
        /// Name of the location
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// General area descriptor. Typically address line
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
        

        /// <summary>
        /// Latitude value between -90 and 90 degrees
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("lat")]
        public float Latitude
        {
            get => latitude;
            set => latitude = value < 90 | value > -90 ? value : latitude;
        }


        /// <summary>
        /// Longitude value between -180 and 180 degrees
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("lng")]
        public float Longitude
        {
            get => longitude;
            set => longitude = value < 180 | value > -180 ? value : longitude;
        }

        /// <summary>
        /// Google location id
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("googleid")]
        public string GoogleId { get; set; }

        /// <summary>
        /// Distance value show on user interface
        /// Ignored by local DB and API serializers
        /// </summary>
        [Ignore]
        public double Distance { get; set; }

        /// <summary>
        /// Collection of associated ItemPriceLocationModels
        /// </summary>
        public ObservableCollection<ItemPriceLocationModel> ItemPriceLocations { get; } = new ObservableCollection<ItemPriceLocationModel>();

        /// <summary>
        /// Method to add an ItemPriceLocationModel to this location
        /// </summary>
        /// <param name="newIpl"></param>
        public void AddItem(ItemPriceLocationModel newIpl)
        {
            ItemPriceLocations.Add(newIpl);
        }

        /// <summary>
        /// Method to add a collection of ItemPriceLocationModels to this location
        /// </summary>
        /// <param name="newIpls"></param>
        public void AddItems(IEnumerable<ItemPriceLocationModel> newIpls)
        {
            foreach (var ipl in newIpls)
            {
                AddItem(ipl);
            }
        }
    }
}
