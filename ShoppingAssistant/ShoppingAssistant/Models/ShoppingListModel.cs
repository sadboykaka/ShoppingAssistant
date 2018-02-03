using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShoppingListModel : Model
    {
        public static string UrlSuffix = "slists";

        [JsonProperty("name")]
        public string Name { get; set; }

        public string DateCreated { get; set; }
        
        private ObservableCollection<ItemQuantityPairModel> items = new ObservableCollection<ItemQuantityPairModel>();
        
        public ObservableCollection<ItemQuantityPairModel> Items => this.items;

        public ShoppingListModel()
        {
            UrlSuffixProperty = UrlSuffix;
        }

        public void AddItem(ItemQuantityPairModel newItem)
        {
            this.items.Add(newItem);
        }

        public void AddItems(IEnumerable<ItemQuantityPairModel> newItems)
        {
            newItems?.ForEach(newItem => this.items.Add(newItem));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ShoppingListModel slist))
            {
                return false;
            }

            if (RemoteDbId != slist.RemoteDbId)
            {
                return false;
            }

            if (Name != slist.Name)
            {
                return false;
            }
            
            return Items.Count == slist.Items.Count && Items.All(item => slist.Items.Any(i => i.Name == item.Name && i.Quantity == item.Quantity && i.Measure == item.Measure));
        }
    }
}
