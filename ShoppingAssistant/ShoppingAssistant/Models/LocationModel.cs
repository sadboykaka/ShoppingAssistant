using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    class LocationModel : Model
    {
        public const string UrlSuffix = "locations";

        /// <summary>
        /// Name of the location
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// General area descriptor. Typically address line
        /// </summary>
        public string Vicinity { get; set; }

        /// <summary>
        /// Private latitiude value between -90 and 90 degrees
        /// </summary>
        private float latitude;
        
        /// <summary>
        /// Latitude value between -90 and 90 degrees
        /// </summary>
        public float Latitude
        {
            get => latitude;
            set => latitude = value < 90 | value > -90 ? value : latitude;
        }

        /// <summary>
        /// Private longitude value between -180 and 180 degrees
        /// </summary>
        private float longitude;

        /// <summary>
        /// Longitude value between -180 and 180 degrees
        /// </summary>
        public float Longitude
        {
            get => longitude;
            set => longitude = value < 180 | value > -180 ? value : longitude;
        }
    }
}
