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

        public ObservableCollection<ItemPriceLocationModel> Ipls { get { return this.iplsMutable; } }

	    public string ItemFilterText { get; set; }
	

		public LocationView (LocationModel location)
		{
			InitializeComponent ();
            
            this.locationModel = location;
		    this.iplsMaster = this.locationModel.ItemPriceLocations;
            this.iplsMutable = new ObservableCollection<ItemPriceLocationModel>(this.iplsMaster);



		    this.ItemFilterTextEntry.TextChanged += OnFilterTextChanged;

		    btnAddpl.Clicked += delegate { OnAddItemClick(); };

            BindingContext = this;
		}

	    private async void OnAddItemClick()
	    {
	        await Navigation.PushAsync(new AddItemPriceLocationView(AddIplEvent));
	    }

	    private async void AddIplEvent(object sender, ItemPriceLocationEventArgs args)
	    {
	        this.requiresUpdate = true;
	        args.ItemPriceLocationModel.LocalDbLocationId = locationModel.LocalDbId ?? 0;
	        this.locationModel.ItemPriceLocations.Add(args.ItemPriceLocationModel);
	        this.iplsMutable.Add(args.ItemPriceLocationModel);

            await Navigation.PopAsync();
        }

        protected override void OnDisappearing()
	    {
	        if (this.requiresUpdate)
	        {
	            App.ModelManager.LocationModelManager.SaveLocationModel(this.locationModel);
	        }
            base.OnDisappearing();
	    }

	    public void OnFilterTextChanged(object sender, EventArgs args)
	    {
	        // Create temp collection for the given filter text
	        var temp = this.iplsMaster.Where(location =>
	            location.Name.ToLower().Contains(this.ItemFilterText.ToLower())).ToList();

	        // Clear mutable collection and populate with new items
	        this.iplsMutable.Clear();
	        temp.ForEach(location => this.iplsMutable.Add(location));
        }
	}
}