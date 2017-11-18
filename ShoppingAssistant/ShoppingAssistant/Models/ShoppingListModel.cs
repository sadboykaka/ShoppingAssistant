using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            newItems.ForEach(newItem => this.items.Add(newItem));
        }
    }
}
