using System;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ItemQuantityPairEventHandler(object sender, ItemQuantityPairArgs args);

    public class ItemQuantityPairArgs : EventArgs
    {
        public ItemQuantityPairModel  ItemQuantityPairModel { get; set; }

        public ItemQuantityPairArgs(ItemQuantityPairModel  iqp)
        {
            this.ItemQuantityPairModel = iqp;
        }
    }
}
