using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddShoppingListView : ContentPage
	{
		private ShoppingListEventHandler callback;
		public string NameField{ get; set; }

		public AddShoppingListView (ShoppingListEventHandler callback)
		{
			InitializeComponent();
			this.callback = callback;

			this.NameField = DateTime.Now.ToString();

			BindingContext = this;

			var btnAddItem = this.FindByName<Button>("BtnAddList");
			btnAddItem.Clicked += delegate { RaiseNewShoppingListEvent(); };
		}
		
		private void RaiseNewShoppingListEvent()
		{
			var newShoppingListModel = new ShoppingListModel()
			{
				Name = this.NameField,
				DateCreated = DateTime.Now.ToString()
			};

			App.ModelManager.ShoppingListModelManager.SaveShoppingListModel(newShoppingListModel);

			callback?.Invoke(this, new ShoppingListEventArgs(newShoppingListModel));
		}
	}
}