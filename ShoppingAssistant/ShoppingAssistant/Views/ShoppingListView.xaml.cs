﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.DataClasses;
using ShoppingAssistant.EventClasses;

namespace ShoppingAssistant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListView : ContentPage
    {
        // Object representing the currently selected item in the ListView
        private object selectedItem = null;

        public event ItemQuantityPairEventHandler ItemQuantityPairEvent;

        private ShoppingList shoppingList;
        
        // ObservableCollection getter for the ListView items with property for data binding
        public ObservableCollection<ItemQuantityPair> Items { get { return this.shoppingList.Items; } }

        // Constructor
        public ShoppingListView(ShoppingList list)
        {
            InitializeComponent();

            this.shoppingList = list;

            this.ItemQuantityPairEvent += new ItemQuantityPairEventHandler(this.AddItemEvent);

            Button btnAddItem = this.FindByName<Button>("btnAddItem");
            btnAddItem.Clicked += delegate { OnAddItemClick(); };

            BindingContext = this;            
        }

        async private void AddItemEvent(object sender, ItemQuantityPairArgs args)
        {
            this.shoppingList.AddItem(args.ItemQuantityPair);
            await Navigation.PopAsync();
        }

        // Method to add the delete button to the toolbar
        private void AddToolbarItems()
        {
            ToolbarItems.Add(new ToolbarItem("Delete", "filter.png", async () => { var page = new ContentPage(); DeleteSelectedItem(); }));
        }

        /// <summary>
        /// Method to delete the ItemQuantityPair currently selected in the ListView
        /// </summary>
        private void DeleteSelectedItem()
        {
            // Find the selected item in the list view and get the associated ItemQuantityPair
            ListView listView = this.FindByName<ListView>("ItemListView");
            ItemQuantityPair iqp = (ItemQuantityPair)listView.SelectedItem;

            // Deselect the selected item and change the stored value
            listView.SelectedItem = null;
            this.selectedItem = null;

            // Remove the item from the list and the delete button from the toolbar
            this.shoppingList.RemoveItem(iqp);
            this.RemoveToolbarItems();
        }

        // Method to remove the delete button from the toolbar
        private void RemoveToolbarItems()
        {
            ToolbarItems.Clear();
        }

        // Back navigation action
        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void OnAddItemClick()
        {
            // Display the new window
            await Navigation.PushAsync(new AddItemView(AddItemEvent));
        }

        // Method to handle the clicking of items in the ListView
        void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            // Error handling
            if (e.SelectedItem == null)
            {
                return;
            }

            if (selectedItem == null)
            {
                // Select the item if previously selected is null
                selectedItem = ((ListView)sender).SelectedItem;
                AddToolbarItems();
            }
            else
            {
                // Deselect the item if the same item is tapped
                if (selectedItem == ((ListView)sender).SelectedItem)
                {
                    //Deselect Item
                    ((ListView)sender).SelectedItem = null;

                    selectedItem = null;
                    RemoveToolbarItems();
                }
                else
                {
                    // Update the stored selected item
                    selectedItem = ((ListView)sender).SelectedItem;
                }
            }
        }
    }
}