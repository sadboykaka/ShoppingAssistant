using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Platform.Services.Geolocation;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NearbyLocationsView : ContentPage
	{
	    private LocationModelManager locationModelManager;

	    public string LocationFilterText { get; set; }

	    private ObservableCollection<LocationModel> locations;

        public ObservableCollection<LocationModel> Locations { get { return locations; } }

		public NearbyLocationsView ()
		{
			InitializeComponent ();

		    this.locationModelManager = App.ModelManager.LocationModelManager;
		    this.locations = this.locationModelManager.LocationModels;
            BindingContext = this;

            if (this.locations.Count == 0)
		    {
		        this.FindByName<ListView>("LocationsListView").BeginRefresh();
		    }

            this.locations.CollectionChanged += NewLocationModel;
		}

	    public void NewLocationModel(object sender, NotifyCollectionChangedEventArgs args)
	    {
	        if (locations.Count != 0)
	        {
	            this.FindByName<ListView>("LocationsListView").EndRefresh();
            }
        }

	    public void Handle_ItemTapped(object sender, EventArgs args)
	    {
	        App.Log.Debug("t", "item tapped");
	    }
    }
}