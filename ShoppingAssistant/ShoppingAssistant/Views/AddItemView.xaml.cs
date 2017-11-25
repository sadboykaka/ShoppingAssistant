using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.DatabaseClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemView : ContentPage
	{
        private ItemQuantityPairEventHandler callBack;
        public string Name { get; set; }
        public string Quantity { get; set; }

		public AddItemView (ItemQuantityPairEventHandler callBack)
		{
            BindingContext = this;
			InitializeComponent ();
            this.callBack = callBack;


            Button btnAddItem = this.FindByName<Button>("btnAddItem");
            btnAddItem.Clicked += delegate { RaiseNewItemQuantityPairEvent(); };
        }

        private void OnAddItemClick()
        {
            this.RaiseNewItemQuantityPairEvent();
        }

        private void RaiseNewItemQuantityPairEvent()
        {
            // TODO error handling for non-int values
            callBack?.Invoke((object)this, new ItemQuantityPairArgs(new ItemQuantityPairModel()
            {
                Name = this.Name,
                Quantity = Int32.Parse(this.Quantity)
            }));
        }
	}
}