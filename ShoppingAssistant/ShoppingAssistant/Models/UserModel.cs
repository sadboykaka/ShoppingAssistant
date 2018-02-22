using Newtonsoft.Json;
using SQLite;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel : Model
    {
        /// <summary>
        /// Name of the user
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Email address of the user
        /// Stored in the local database
        /// Unique
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("email")]
        [Unique]
        public string Email { get; set; }

        /// <summary>
        /// Password for the user
        /// Stored in the local database
        /// Supplied by the remote database
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
