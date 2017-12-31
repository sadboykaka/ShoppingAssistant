using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant
{
    /// <summary>
    /// Add Item Quantity View
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemView
	{
        /// <summary>
        /// EventHandler for completion callback
        /// </summary>
		private readonly ItemQuantityPairEventHandler callBack;

        /// <summary>
        /// Reference colleciton of potential items
        /// </summary>
	    private readonly IEnumerable<string> itemsCollection;

        /// <summary>
        /// Binding Property
        /// </summary>
        public string ItemFilterText { get; set; }

        /// <summary>
        /// Binding Property
        /// </summary>
	    public string Quantity { get; set; }

        /// <summary>
        /// Binding Property
        /// </summary>
	    public ObservableCollection<string> Items { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="callBack">Event handler for completion callback</param>
        /// <param name="itemsCollection">Reference collection of potential items</param>
		public AddItemView (ItemQuantityPairEventHandler callBack, IEnumerable<string> itemsCollection)
		{
			InitializeComponent ();

			this.callBack = callBack;
			this.itemsCollection = itemsCollection;
			
            // Populate the binding property
			Items = new ObservableCollection<string>(itemsCollection.OrderBy(item => item));
			
			BindingContext = this;
		    
            // Set event handlers
			ItemFilterTextEntry.TextChanged += TextChanged;
			BtnAddItem.Clicked += delegate { RaiseNewItemQuantityPairEvent(); };
		}

		/// <summary>
		/// Handle the tapping of a LocationModel in the ListView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void Handle_ItemTapped(object sender, EventArgs args)
		{
			var item = (string)ItemsListView.SelectedItem;
			ItemFilterText = item;

		    ItemsListView.SelectedItem = null;
		    OnPropertyChanged("ItemFilterText");
        }

        /// <summary>
        /// Method called when ItemFilterTextEntry is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
		private void TextChanged(object sender, EventArgs args)
		{
			var temp = itemsCollection.Where(item => item.Contains(ItemFilterText)).OrderBy(item => item);
			Items.Clear();
		    temp.ForEach(Items.Add);
		}

        /// <summary>
        /// Method to create a new ItemQuantityPair and raise the relevant event
        /// </summary>
		private void RaiseNewItemQuantityPairEvent()
		{
			// Add the new item to the collection
			App.ModelManager.AddItem(ItemFilterText);

			var iqp = new ItemQuantityPairModel()
			{
				Name = ItemFilterText,
				Quantity = Int32.Parse(Quantity)
			};

			callBack?.Invoke(this, new ItemQuantityPairArgs(iqp));
		}
	}
}