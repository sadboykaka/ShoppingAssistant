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
	public partial class ShoppingListsView : ContentPage
	{
		public NavigationPage navPage = new NavigationPage();

		private ObservableCollection<ShoppingListModel> shoppingLists = App.MasterController.ShoppingListController.ShoppingListModels;
		public ObservableCollection<ShoppingListModel> ShoppingLists { get { return shoppingLists; } }

		public ShoppingListsView()
		{
			InitializeComponent();

			//System.Net.ServicePointManager.ServerCertificateValidationCallback += CertificateValidationCallBack;
		    Title = "Shopping Lists";
			// Set the button event listeners
			this.SetBtnListeners();

			BindingContext = this;

		}

		private void SetBtnListeners()
		{
			var shoppingListBtn = this.FindByName<Button>("BtnNewShoppingList");
			shoppingListBtn.Clicked += delegate { OnShoppingListBtnClick(); };
		}


		public void OnDelete(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			//DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");

			//ListView listView = this.FindByName<ListView>("ItemListView");
			//ShoppingList selectedList = (ShoppingList)listView.SelectedItem;

			//listView.SelectedItem = null;
			var model = (ShoppingListModel)mi.CommandParameter;

			App.MasterController.ShoppingListController.DeleteShoppingListAsync(model);

			this.shoppingLists.Remove((ShoppingListModel)mi.CommandParameter);
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
			var listView = (ListView)sender;
			var list = (ShoppingListModel)listView.SelectedItem;

			listView.SelectedItem = null;
			Navigation.PushAsync(new ShoppingListView(list));
		}

		private async void AddShoppingListEvent(object sender, ShoppingListEventArgs args)
		{
			this.shoppingLists.Add(args.ShoppingList);
			await Navigation.PopAsync();
		}

		async void OnShoppingListBtnClick()
		{
			await Navigation.PushAsync(new AddShoppingListView(AddShoppingListEvent));
		}
	}
}