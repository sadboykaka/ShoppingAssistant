using System;
using ShoppingAssistant.Crypt;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public LoginView ()
		{
			this.Title = "Shopping Assistant - Login";
			BindingContext = this;
			InitializeComponent ();
			
			this.BtnLogin.Clicked += this.Login;
			this.BtnRegister.Clicked += this.Register;
		}
		
		private void DisableUI()
		{
			this.BtnLogin.IsEnabled = false;
			this.BtnRegister.IsEnabled = false;
			this.EntryEmail.IsEnabled = false;
			this.EntryPassword.IsEnabled = false;
		}

		private void EnableUI()
		{
			this.BtnLogin.IsEnabled = true;
			this.BtnRegister.IsEnabled = true;
			this.EntryEmail.IsEnabled = true;
			this.EntryPassword.IsEnabled = true;
		}

		private bool CheckInput()
		{
			if (string.IsNullOrEmpty(this.Email))
			{
				this.LabelError.Text = "Email cannot be blank";
				return false;
			}

			if (string.IsNullOrEmpty(this.Password))
			{
				this.LabelError.Text = "Password cannot be blank";
				return false;
			}

			this.LabelError.Text = "";
			return true;
		}

		private async void Login(object sender, EventArgs args)
		{
			if (!this.CheckInput())
			{
				return;
			}

			this.DisableUI();

			var response = await App.ModelManager.Login(new UserModel
			{
				Email = this.Email.Replace(" ", string.Empty),
				Password = this.Password
			});
			
			switch (response)
			{
				case LoginResponse.InvalidCredentials:
					this.LabelError.Text = "Invalid email or password combination";
					break;
				case LoginResponse.NoResponse:
					this.LabelError.Text = "Cannot connect to server";
					break;
				case LoginResponse.Success:
					this.LabelError.Text = "";

					// Save to the database
					

					// Open the main page
					App.MD = new MDP();
					Application.Current.MainPage = App.MD;

					break;
			}
			
			this.EnableUI();
		}

		private void Register(object sender, EventArgs args)
		{
			Navigation.PushAsync(new RegisterView());
		}
	}
}