using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    public class UserModel : Model
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        [Unique]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
