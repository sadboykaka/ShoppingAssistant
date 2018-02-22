using System;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    /// <summary>
    /// An event handler delegate taking ItemPriceLocationEventArgs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ItemPriceLocationEventHandler(object sender, ItemPriceLocationEventArgs args);

    /// <inheritdoc />
    /// <summary>
    /// ItemPriceLocation EventArgs
    /// </summary>
    public class ItemPriceLocationEventArgs : EventArgs
    {
        /// <summary>
        /// ItemPriceLocationModel
        /// </summary>
        public ItemPriceLocationModel ItemPriceLocationModel { get; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ipl"></param>
        public ItemPriceLocationEventArgs(ItemPriceLocationModel ipl)
        {
            ItemPriceLocationModel = ipl;
        }
    }
}
