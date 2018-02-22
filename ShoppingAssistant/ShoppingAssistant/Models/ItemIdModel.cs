using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Abstract ItemIdModel Class
    /// </summary>
    public abstract class ItemIdModel : Model
    {
        /// <summary>
        /// Remote 
        /// </summary>
        public int RemoteItemId { get; set; }
    }
}
