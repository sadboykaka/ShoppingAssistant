using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Model
    {
        // Hook up to the static / const var in each class
        public string UrlSuffixProperty { get; protected set; }

        [PrimaryKey, AutoIncrement]
        public int? LocalDbId { get; set; }

        /// <summary>
        /// Remote database unique identifier
        /// </summary>
        [JsonProperty("id")]
        public int? RemoteDbId { get; set; }

        /// <summary>
        /// The time this model was last updated
        /// </summary>
        [JsonProperty("updated_at")]
        public string LastUpdated { get; set; }

        private bool deleted = false;

        /// <summary>
        /// Deleted flag marking this item as deleted
        /// </summary>
        public bool Deleted
        {
            get { return this.deleted; }
            set { this.deleted = value; }
        }
    }
}
