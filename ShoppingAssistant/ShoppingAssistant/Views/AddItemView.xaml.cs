using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using XLabs.Forms.Mvvm;

namespace ShoppingAssistant
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemView
	{
		private ItemQuantityPairEventHandler callBack;
		public string Name { get; set; }
		public string Quantity { get; set; }

		public readonly ObservableCollection<string> ItemsCollection = new ObservableCollection<string>();
	    public ObservableCollection<string> Items;

        public AddItemView (ItemQuantityPairEventHandler callBack, ObservableCollection<string> itemsCollection)
		{
			InitializeComponent ();
			this.callBack = callBack;
		    this.ItemsCollection = itemsCollection;
            
            Items = new ObservableCollection<string>();

		    AutoCompleteView.TextChanged += TextChanged;


		    BindingContext = this;

            //Button btnAddItem = this.FindByName<Button>("btnAddItem");
            //btnAddItem.Clicked += delegate { RaiseNewItemQuantityPairEvent(); };
        }


		private void OnAddItemClick()
		{
			this.RaiseNewItemQuantityPairEvent();
		}

	    private void TextChanged(object sender, EventArgs args)
	    {
            Items = new ObservableCollection<string>(ItemsCollection.Where(item => item.Contains(AutoCompleteView.Text)));
	    }

		private void RaiseNewItemQuantityPairEvent()
		{
            ItemsCollection.Add(AutoCompleteView.Text);

            var iqp = new ItemQuantityPairModel()
            {
                Name = this.Name,
                Quantity = Int32.Parse(this.Quantity)
            };

            callBack?.Invoke((object)this, new ItemQuantityPairArgs(iqp));
        }
	}
}