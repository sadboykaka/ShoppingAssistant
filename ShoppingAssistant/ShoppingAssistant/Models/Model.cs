using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    /// <summary>
    /// Abstract Model Class
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Model
    {
        /// <summary>
        /// UrlSuffix
        /// Required by some generic methods instead of identifying the class
        /// </summary>
        [Ignore]
        public string UrlSuffixProperty { get; protected set; }

        /// <summary>
        /// Local database ID
        /// Stored in local database
        /// Primary key that auto-increments
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int? LocalDbId { get; set; }

        /// <summary>
        /// Remote database unique identifier
        /// Supplied by remote database
        /// Stored in local database
        /// </summary>
        [JsonProperty("id")]
        public int? RemoteDbId { get; set; }

        /// <summary>
        /// The time this model was last updated
        /// Supplied by remote database
        /// Stored in local database
        /// </summary>
        [JsonProperty("updated_at")]
        public string LastUpdated { get; set; }

        /// <summary>
        /// Deleted flag marking this item as deleted
        /// Required to ensure parity between local and remote
        /// Stored in local database
        /// </summary>
        public bool Deleted { get; set; }
    }
}
