using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.DataClasses;
using ShoppingAssistant.EventClasses;

namespace ShoppingAssistant
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemView : ContentPage
	{
        private ItemQuantityPairEventHandler callBack;
        public string Item { get; set; }
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
            if (this.callBack == null)
                return;

            this.callBack((object)this, new ItemQuantityPairArgs(new ItemQuantityPair(this.Item, Int32.Parse(this.Quantity))));
        }
	}
}