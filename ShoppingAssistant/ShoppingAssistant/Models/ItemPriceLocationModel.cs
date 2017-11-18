using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ShoppingAssistant.Models
{
    class ItemPriceLocationModel : ItemIdModel
    {
        public const string UrlSuffix = "ipls";
        [Ignore]
        public ItemModel Item { get; set; }

        public float Price { get; set; }

        public LocationModel Location { get; set; }
    }
}
