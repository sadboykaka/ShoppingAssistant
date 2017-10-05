using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ShoppingAssistant.DataClasses;

namespace ShoppingAssistant
{
	public partial class MainPage : ContentPage
	{
        public NavigationPage navPage = new NavigationPage();

        private ObservableCollection<ShoppingList> shoppingLists = new ObservableCollection<ShoppingList>();
        public ObservableCollection<ShoppingList> ShoppingLists { get { return shoppingLists; } }

		public MainPage()
		{
			InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                ShoppingList list = new ShoppingList();
                list.AddItem("apples", 2);
                list.AddItem(new ItemQuantityPair("bananas", 2));
                list.AddItem(new ItemQuantityPair("oranges", 2));
                list.AddItem(new ItemQuantityPair("bread", 2));

                this.shoppingLists.Add(list);
            }

            Button shoppingListBtn = this.FindByName<Button>("btnShoppingList");
            shoppingListBtn.Clicked += delegate { OnShoppingListBtnClick(); };

            BindingContext = this;
		}

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");

            //ListView listView = this.FindByName<ListView>("ItemListView");
            //ShoppingList selectedList = (ShoppingList)listView.SelectedItem;

            //listView.SelectedItem = null;

            this.shoppingLists.Remove((ShoppingList)mi.CommandParameter);
        }

        /// <summary>
        /// Handle the tapping of a ShoppingList in the ListView
        /// When an item is tapped we want to open up the associated ShoppingList in a ShoppingListView
        /// On a long press we want to display a delete button in the toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void Handle_ItemTapped(object sender, EventArgs args)
        {
            ListView listView = (ListView)sender;
            ShoppingList list = (ShoppingList)listView.SelectedItem;

            listView.SelectedItem = null;
            Navigation.PushAsync(new ShoppingListView(list));
        }

        public void OnShoppingListEdited(ShoppingList list)
        {


            // Save changes to the database
        }

        public void OnShoppingListBtnClick()
        {

            Navigation.PushAsync(new ShoppingListView(this.shoppingLists.First()));
            this.BackgroundColor = Color.Black;
        }
	}
}
