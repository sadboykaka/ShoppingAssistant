using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs;
using XLabs.Platform.Services.Geolocation;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NearbyLocationsView : ContentPage
	{
        /// <summary>
        /// Reference to LocationModelManager
        /// </summary>
	    private LocationModelManager locationModelManager;

        /// <summary>
        /// Reference to master location model collection
        /// </summary>
	    private ObservableCollection<LocationModel> locationsMaster;

        /// <summary>
        /// Private binding property - mutable so user can filter it
        /// </summary>
	    private ObservableCollection<LocationModel> locationsMutable;

        /// <summary>
        /// Binding property
        /// </summary>
        public ObservableCollection<LocationModel> Locations => this.locationsMutable;

	    /// <summary>
	    /// Binding property
	    /// </summary>
	    public string LocationFilterText { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
		public NearbyLocationsView ()
		{
			InitializeComponent ();

            // Add the filter text changed eventhandler
		    this.LocationFilterTextEntry.TextChanged += OnFilterTextChanged;

            // Set references
		    this.locationModelManager = App.ModelManager.LocationModelManager;
		    this.locationsMaster = this.locationModelManager.LocationModels;
            
		    // Add collection changed event
		    this.locationsMaster.CollectionChanged += NewLocationModel;

            // Create new mutable collection so we can filter the shown locations
		    this.locationsMutable = new ObservableCollection<LocationModel>();
		    foreach (var location in this.locationsMaster)
		    {
		        this.AddLocationModel(location);
		    }

            // Set binding context
            BindingContext = this;

            // Set refreshing symbol if there is no data
            if (this.locationsMutable.Count == 0)
		    {
		        this.FindByName<ListView>("LocationsListView").BeginRefresh();
		    }
		}

        /// <summary>
        /// Event raised when a new LocationModel is added by the LocationModelManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
	    public void NewLocationModel(object sender, NotifyCollectionChangedEventArgs args)
	    {
            // Add the new LocationModels to the mutable collection
            // They master collection is just a reference to the collection invoking this method
            // so no need to add it again
	        foreach (var model in args.NewItems)
	        {
	            this.AddLocationModel((LocationModel) model);
	        }

            // Remove the refreshing symbol if there is data
	        if (locationsMutable.Count != 0)
	        {
	            this.FindByName<ListView>("LocationsListView").EndRefresh();
	        }
        }

	    public void AddLocationModel(LocationModel model)
	    {
	        for (int i = 0; i <= this.locationsMutable.Count; i++)
	        {
	            if (i == this.locationsMutable.Count)
	            {
	                this.locationsMutable.Insert(i, model);
	                break;
	            }

	            if (this.locationsMutable.ElementAt(i).Distance > model.Distance)
	            {
	                this.locationsMutable.Insert(i, model);
	                break;
	            }
	        }
	    }

	    /// <summary>
        /// Handle the tapping of a LocationModel in the ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
	    public void Handle_ItemTapped(object sender, EventArgs args)
        {
            var listView = (ListView) sender;
            var location = (LocationModel) listView.SelectedItem;

            listView.SelectedItem = null;
            Navigation.PushAsync(new LocationView(location));
	    }

        /// <summary>
        /// Filter the mutable collection based on the given input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
	    public void OnFilterTextChanged(object sender, EventArgs args)
	    {
            // Create temp collection for the given filter text
	        var temp = this.locationsMaster.Where(location =>
                location.Name.ToLower().Contains(this.LocationFilterText.ToLower())).ToList();

            // Clear mutable collection and populate with new items
            this.locationsMutable.Clear();
            temp.ForEach(location => this.locationsMutable.Add(location));
        }
    }
}