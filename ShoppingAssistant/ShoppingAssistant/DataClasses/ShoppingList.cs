using System;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingAssistant.DataClasses
{
    public class ShoppingList
    {
        public string Name { get; private set; }

        private ObservableCollection<ItemQuantityPair> items;
        public ObservableCollection<ItemQuantityPair> Items { get { return this.items; } }

        public ShoppingList()
        {
            this.Name = DateTime.Today.ToString();
            this.items = new ObservableCollection<ItemQuantityPair>();
        }

        public void AddItem(ItemQuantityPair item)
        {
            this.items.Add(item);
        }

        public void AddItem(string name, int quantity)
        {
            this.items.Add(new ItemQuantityPair(name, quantity));
        }

        public void RemoveItem(ItemQuantityPair iqp)
        {
            this.items.Remove(iqp);
        }
    }
}
