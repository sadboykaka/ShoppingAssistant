﻿using ShoppingAssistant.EventClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddItemPriceLocationView : ContentPage
	{
		/// <summary>
		/// ItemPriceLocationEventHandler on which we callback with new ItemPriceLocationModel
		/// </summary>1
		private readonly ItemPriceLocationEventHandler callBack;

		/// <summary>
		/// Binding Property
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Binding Property
		/// </summary>
		public string Price { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public AddItemPriceLocationView(ItemPriceLocationEventHandler callBack)
		{
			InitializeComponent ();
			BindingContext = this;

			this.callBack = callBack;

			BtnAddIpl.Clicked += delegate { RaiseNewItemPriceLocationEvent(); };
		}

		/// <summary>
		/// Raise new item price location event
		/// </summary>
		private void RaiseNewItemPriceLocationEvent()
		{
			// TODO error handling for non-float values
			callBack?.Invoke(this, new ItemPriceLocationEventArgs(new ItemPriceLocationModel()
			{
				Name = this.Name,
				Price = float.Parse(this.Price)
			}));
		}

	}
}