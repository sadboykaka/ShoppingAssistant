using System;
using ShoppingAssistant.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationPriceModelView : ContentPage
	{
        /// <summary>
        /// LocationPrice ViewModel
        /// </summary>
	    public LocationPriceViewModel LocationPriceViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
		public LocationPriceModelView (LocationPriceViewModel model)
		{
			InitializeComponent ();

		    LocationPriceViewModel = model;

		    Title = LocationPriceViewModel.ShoppingListName;

		    BindingContext = LocationPriceViewModel;
		}


        /// <summary>
        /// Item tapped event for the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
	    private void Handle_ItemTapped(object sender, EventArgs args)
	    {
            // Deselect the item
	        ((ListView) sender).SelectedItem = null;
	    }
	}
}