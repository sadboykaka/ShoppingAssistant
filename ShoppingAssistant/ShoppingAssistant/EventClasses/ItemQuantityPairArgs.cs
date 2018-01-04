using System;
using System.Collections.Generic;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ItemQuantityPairEventHandler(object sender, ItemQuantityPairArgs args);

    public class ItemQuantityPairArgs : EventArgs
    {
        public List<ItemQuantityPairModel>  ItemQuantityPairModels { get; set; }

        public ItemQuantityPairArgs(List<ItemQuantityPairModel>  iqps)
        {
            ItemQuantityPairModels = iqps;
        }
    }
}
