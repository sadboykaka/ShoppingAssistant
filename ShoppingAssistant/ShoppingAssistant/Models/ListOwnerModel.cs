using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    class ListOwnerModel : Model
    {
        public const string UrlSuffix = "share";

        public int ShoppingListModelId { get; set; }

        public string UserEmail { get; set; } 
    }
}
