using System;
using System.Collections.Generic;
using System.Text;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ShoppingListEventHandler(object sender, ShoppingListEventArgs args);

    public class ShoppingListEventArgs : EventArgs
    {
        public ShoppingListModel ShoppingList { get; set; }

        public ShoppingListEventArgs(ShoppingListModel list)
        {
            this.ShoppingList = list;
        }
    }
}