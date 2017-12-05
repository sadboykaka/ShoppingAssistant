using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;

namespace ShoppingAssistant.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        [PrimaryKey]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
