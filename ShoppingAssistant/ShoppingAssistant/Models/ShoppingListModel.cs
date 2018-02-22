using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Shopping List Model
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ShoppingListModel : Model
    {
        /// <summary>
        /// API suffix
        /// </summary>
        public const string UrlSuffix = "slists";

        /// <summary>
        /// Name of the shopping list
        /// Stored in local database
        /// Supplied by remote database
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Date the shopping list was created
        /// Stored in local database
        /// Supplied by remote database
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Observable Collection of Items
        /// </summary>
        public ObservableCollection<ItemQuantityPairModel> Items { get; } = new ObservableCollection<ItemQuantityPairModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingListModel()
        {
            UrlSuffixProperty = UrlSuffix;
        }

        /// <summary>
        /// Method to add a single item to the shopping list
        /// </summary>
        /// <param name="newItem"></param>
        public void AddItem(ItemQuantityPairModel newItem)
        {
            Items.Add(newItem);
        }

        /// <summary>
        /// Method to add a list of items to the shopping list
        /// </summary>
        /// <param name="newItems"></param>
        public void AddItems(IEnumerable<ItemQuantityPairModel> newItems)
        {
            newItems?.ForEach(newItem => Items.Add(newItem));
        }

        /// <summary>
        /// Override equals method
        /// Compares based on remote database ID, name, and item permutation
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Overriden get hash code method
        /// Supplied to supress warning
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
