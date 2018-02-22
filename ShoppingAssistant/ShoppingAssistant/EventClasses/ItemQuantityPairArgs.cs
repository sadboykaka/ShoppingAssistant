using System;
using System.Collections.Generic;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.EventClasses
{
    /// <summary>
    /// An event handler delegate taking ItemQuantityPairArgs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ItemQuantityPairEventHandler(object sender, ItemQuantityPairArgs args);

    /// <inheritdoc />
    /// <summary>
    /// ItemQuantityPair EventArgs
    /// Holds a List of ItemQuantityPairModels
    /// </summary>
    public class ItemQuantityPairArgs : EventArgs
    {
        /// <summary>
        /// A List of ItemQuantityPairModels
        /// </summary>
        public List<ItemQuantityPairModel>  ItemQuantityPairModels { get; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iqps"></param>
        public ItemQuantityPairArgs(List<ItemQuantityPairModel>  iqps)
        {
            ItemQuantityPairModels = iqps;
        }
    }
}
