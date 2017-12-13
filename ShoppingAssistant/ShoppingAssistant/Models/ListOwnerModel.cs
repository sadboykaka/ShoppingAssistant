using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    class ListOwnerModel : Model
    {
        public int ShoppingListModelId { get; set; }

        public string UserEmail { get; set; } 
    }
}
