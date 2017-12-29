using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationView : ContentPage
	{
		private object selectedItem = null;
		private LocationModel locationModel;
		private bool requiresUpdate = false;

		public event ItemPriceLocationEventHandler IplEvent;

		private ObservableCollection<ItemPriceLocationModel> iplsMaster;

		private ObservableCollection<ItemPriceLocationModel> iplsMutable;

		public ObservableCollection<ItemPriceLocationModel> Ipls { get { return iplsMutable; } }

		public string ItemFilterText { get; set; }
	

		public LocationView (LocationModel location)
		{
			InitializeComponent ();
			
			locationModel = location;
			iplsMaster = locationModel.ItemPriceLocations;
			iplsMutable = new ObservableCollection<ItemPriceLocationModel>(iplsMaster);



			ItemFilterTextEntry.TextChanged += OnFilterTextChanged;

			btnAddpl.Clicked += delegate { OnAddItemClick(); };

			BindingContext = this;
		}

		private async void OnAddItemClick()
		{
			await Navigation.PushAsync(new AddItemPriceLocationView(AddIplEvent));
		}

		private async void AddIplEvent(object sender, ItemPriceLocationEventArgs args)
		{
			requiresUpdate = true;
			args.ItemPriceLocationModel.LocalDbLocationId = locationModel.LocalDbId ?? 0;
			locationModel.ItemPriceLocations.Add(args.ItemPriceLocationModel);
			iplsMutable.Add(args.ItemPriceLocationModel);

			await Navigation.PopAsync();
		}

		protected override void OnDisappearing()
		{
			if (requiresUpdate)
			{
				App.ModelManager.LocationController.SaveLocationModel(locationModel);
			}
			base.OnDisappearing();
		}

		public void OnFilterTextChanged(object sender, EventArgs args)
		{
			// Create temp collection for the given filter text
			var temp = iplsMaster.Where(location =>
				location.Name.ToLower().Contains(ItemFilterText.ToLower())).ToList();

			// Clear mutable collection and populate with new items
			iplsMutable.Clear();
			temp.ForEach(location => iplsMutable.Add(location));
		}
	}
}