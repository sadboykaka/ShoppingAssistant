using System;
using System.Collections.Generic;
using System.Text;
using ShoppingAssistant.DataClasses;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ItemQuantityPairEventHandler(object sender, ItemQuantityPairArgs args);

    public class ItemQuantityPairArgs : EventArgs
    {
        public ItemQuantityPair ItemQuantityPair { get; set; }

        public ItemQuantityPairArgs(ItemQuantityPair iqp)
        {
            this.ItemQuantityPair = iqp;
        }
    }
}
