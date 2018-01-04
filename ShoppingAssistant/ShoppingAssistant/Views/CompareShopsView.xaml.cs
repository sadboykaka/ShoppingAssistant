using System.Collections.ObjectModel;
using ShoppingAssistant.Models;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	/// <inheritdoc />
	/// <summary>
	/// View to compare the prices for a given shopping list at nearby locations
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompareShopsView
	{
		/// <summary>
		/// Class that that bundles a total price with a given location for displaying in a list
		/// </summary>
		public class LocationPriceModel
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
		}

		/// <summary>
		/// The shopping list relevant to this view
		/// </summary>
		private readonly ShoppingListModel shoppingList;

		/// <summary>
		/// Collection of Location & Price Models
		/// </summary>
		private readonly ObservableCollection<LocationPriceModel> locationPriceModels = new ObservableCollection<LocationPriceModel>();

		/// <summary>
		/// Bindable Property
		/// </summary>
		public ObservableCollection<LocationPriceModel> LocationPriceModels => locationPriceModels;

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="list">The shopping list to be evaluated</param>
		public CompareShopsView(ShoppingListModel list)
		{
			InitializeComponent ();

			shoppingList = list;
			Title = shoppingList.Name;
			
			// Create the LocationPriceModels
			CompareShopPrices();


			BindingContext = this;
		}

		/// <summary>
		/// Method to compare the price for the member shopping list at each available location
		/// </summary>
		private void CompareShopPrices()
		{
			// Get the nearby locations
			var locations = App.MasterController.LocationController.LocationModels;
			foreach (var location in locations)
			{
				// Calculate a price for this location
				double total = 0;
				var numberOfItems = 0;

				foreach (var ipl in location.ItemPriceLocations)
				{
					foreach (var item in shoppingList.Items)
					{
						if (ipl.Name.Contains(item.Name))
						{
							total += item.Quantity * ipl.Price;
							numberOfItems++;
						}
					}
				}

				// Create a new LocationPriceModel
				locationPriceModels.Add(new LocationPriceModel()
				{
					Location = location,
					Price = total,
					NumberOfItemsMatched = numberOfItems
				});
			}
		}
	}
}