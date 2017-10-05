using System;
using System.Collections.Generic;
using System.Text;

using ShoppingAssistant.DataClasses;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ShoppingListEventHandler(object sender, ShoppingListEventArgs args);

    public class ShoppingListEventArgs : EventArgs
    {
        public ShoppingList ShoppingList { get; set; }

        public ShoppingListEventArgs(ShoppingList list)
        {
            this.ShoppingList = list;
        }
    }
}
