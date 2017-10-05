using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.DataClasses
{
    public class ItemQuantityPair
    {

        public string Name { set; get; }
        public int Quantity { set; get; }

        public ItemQuantityPair(string name, int quantity)
        { 
            this.Name = name;
            this.Quantity = quantity;
        }
    }
}
