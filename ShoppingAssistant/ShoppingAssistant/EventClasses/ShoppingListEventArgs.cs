using System;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    /// <summary>
    /// An event handler delegate taking ShoppingListEventArgs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ShoppingListEventHandler(object sender, ShoppingListEventArgs args);

    /// <summary>
    /// ShoppingList EventArgs
    /// </summary>
    public class ShoppingListEventArgs : EventArgs
    {
        /// <summary>
        /// ShoppingListModel
        /// </summary>
        public ShoppingListModel ShoppingList { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list"></param>
        public ShoppingListEventArgs(ShoppingListModel list)
        {
            ShoppingList = list;
        }
    }
}