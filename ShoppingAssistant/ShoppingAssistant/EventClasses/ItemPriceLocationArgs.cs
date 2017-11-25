using System;
using System.Collections.Generic;
using System.Text;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    public delegate void ItemPriceLocationEventHandler(object sender, ItemPriceLocationEventArgs args);

    public class ItemPriceLocationEventArgs : EventArgs
    {
        public ItemPriceLocationModel ItemPriceLocationModel { get; set; }

        public ItemPriceLocationEventArgs(ItemPriceLocationModel ipl)
        {
            this.ItemPriceLocationModel = ipl;
        }
    }
}
