using System.Collections.ObjectModel;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.ViewModels
{
    /// <summary>
    /// Class that that bundles a total price with a given location for displaying in a list
    /// </summary>
    public class LocationPriceViewModel
    {
        /// <summary>
        /// The given LocationModel
        /// </summary>
        public LocationModel Location { get; set; }

        /// <summary>
        /// The price for an slist at the given location
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The number of items matched to give the price for the slist and location pair
        /// </summary>
        public int NumberOfItemsMatched { get; set; }

        /// <summary>
        /// Exposed LocationModel Name used for Binding in View
        /// </summary>
        public string Name => Location.Name;

        /// <summary>
        /// Exposed LocationModel distance used for Binding in View
        /// </summary>
        public double Distance => Location.Distance;

        /// <summary>
        /// Exposed LocationModel Vicinity used for Binding in View
        /// </summary>
        public string Vicinity => Location.Vicinity;

        /// <summary>
        /// ShoppingList for this LocationPriceViewModel
        /// </summary>
        public string ShoppingListName { get; set; }

        /// <summary>
        /// Item matches for the current model
        /// </summary>
        private ObservableCollection<ItemMatchViewModel> itemMatches = new ObservableCollection<ItemMatchViewModel>();

        /// <summary>
        /// Bindable ItemMatch collection
        /// </summary>
        public ObservableCollection<ItemMatchViewModel> ItemMatches => itemMatches;
    }
}
