using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant
{
    /// <summary>
    /// View for single shopping list
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListView : ContentPage
    {
        /// <summary>
        /// Currently selected item in the ItemQuantityPair list view
        /// </summary>
        private object selectedItem;

        /// <summary>
        /// Raised when a new ItemQuantityPair is to be added to the ShoppingList
        /// </summary>
        public event ItemQuantityPairEventHandler ItemQuantityPairEvent;

        /// <summary>
        /// The currently represented ShoppingList
        /// </summary>
        private readonly ShoppingListModel shoppingList;

        /// <summary>
        /// Does the ShoppingList need to be updated in the DB/API
        /// </summary>
        private bool requiresUpdate = false;

        /// <summary>
        /// Binding property
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Bindable property to access ItemQuantityPairs
        /// </summary>
        public ObservableCollection<ItemQuantityPairModel > Items => this.shoppingList.Items;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list"></param>
        public ShoppingListView(ShoppingListModel list)
        {
            InitializeComponent();

            this.shoppingList = list;

            // Set event delegates
            this.ItemQuantityPairEvent += AddItemEvent;
            btnAddItem.Clicked += delegate { OnAddItemClick(); };
            btnShare.Clicked += delegate { OnShareClick(); };

            BindingContext = this;            
        }

        /// <summary>
        /// New ItemQuantityiPair event handler method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void AddItemEvent(object sender, ItemQuantityPairArgs args)
        {
            this.requiresUpdate = true;
            this.shoppingList.Items.Add(args.ItemQuantityPairModel);
            App.ModelManager.ShoppingListController.SaveShoppingListModel(this.shoppingList);

            await Navigation.PopAsync();
        }

        // Method to add the delete button to the toolbar
        private void AddToolbarItems()
        {
            // TODO filter.png??
            ToolbarItems.Add(new ToolbarItem("Delete", "filter.png", async () => { var page = new ContentPage(); DeleteSelectedItem(); }));
        }

        /// <summary>
        /// Method to delete the ItemQuantityPairModel currently selected in the ListView
        /// </summary>
        private void DeleteSelectedItem()
        {
            // Find the selected item in the list view and get the associated ItemQuantityPair
            var listView = this.FindByName<ListView>("ItemListView");
            var iqp = (ItemQuantityPairModel)listView.SelectedItem;

            // Deselect the selected item and change the stored value
            listView.SelectedItem = null;
            this.selectedItem = null;

            // Remove the item from the list and the delete button from the toolbar
            this.shoppingList.Items.Remove(iqp);
            this.RemoveToolbarItems();

            this.requiresUpdate = true;
        }

        /// <summary>
        /// Method to remove the delete button from the toolbar
        /// </summary>
        private void RemoveToolbarItems()
        {
            ToolbarItems.Clear();
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Called when this page is removed from the NavigationStack
        /// </summary>
        protected override void OnDisappearing()
        {
            // Save the shopping list in the local database
            if (this.requiresUpdate)
                App.ModelManager.ShoppingListController.SaveShoppingListModel(this.shoppingList);
            base.OnDisappearing();
        }

        /// <summary>
        /// Method called when AddItem button is clicked
        /// </summary>
        private async void OnAddItemClick()
        {
            // Display the new window
            await Navigation.PushAsync(new AddItemView(AddItemEvent));
        }
        
        /// <summary>
        /// Method called when Share button is clicked
        /// </summary>
        private async void OnShareClick()
        {
            var response = await App.ModelManager.ShoppingListController.AddOwnerAsync(shoppingList, Email);

            this.lblShareResult.IsVisible = true;
            this.lblShareResult.Text = response ? "Shared with user" : "Could not share with user";
            this.lblShareResult.TextColor = response ? Color.Green : Color.Red;
        }
    }
}