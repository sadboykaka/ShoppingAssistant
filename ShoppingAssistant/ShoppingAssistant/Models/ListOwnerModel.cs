using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// List Owner Model
    /// Relates a shopping list to an owner
    /// </summary>
    internal class ListOwnerModel : Model
    {
        /// <summary>
        /// API suffix
        /// </summary>
        public const string UrlSuffix = "share";

        /// <summary>
        /// Shopping List Model ID
        /// Stored in the local database
        /// </summary>
        public int ShoppingListModelId { get; set; }

        /// <summary>
        /// User Email
        /// Stored in the local database
        /// </summary>
        public string UserEmail { get; set; } 
    }
}
