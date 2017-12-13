using System;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
	/// <inheritdoc />
	/// <summary>
	/// Login View
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView
	{
		/// <summary>
		/// Binding Property
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Binding Property
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public LoginView ()
		{
			Title = "Shopping Assistant - Login";
			BindingContext = this;
			InitializeComponent ();
			
			// Set event handlers
			BtnLogin.Clicked += Login;
			BtnRegister.Clicked += Register;
		}
		
		/// <summary>
		/// Method to disable the interactive UI elements (while processing is being done)
		/// </summary>
		private void DisableUi()
		{
			BtnLogin.IsEnabled = false;
			BtnRegister.IsEnabled = false;
			EntryEmail.IsEnabled = false;
			EntryPassword.IsEnabled = false;
		}

		/// <summary>
		/// Method to enable the interactive UI elements (after processing is done)
		/// </summary>
		private void EnableUi()
		{
			BtnLogin.IsEnabled = true;
			BtnRegister.IsEnabled = true;
			EntryEmail.IsEnabled = true;
			EntryPassword.IsEnabled = true;
		}

		/// <summary>
		/// Method to check the user input
		/// </summary>
		/// <returns>True if valid, false if not</returns>
		private bool CheckInput()
		{
			if (string.IsNullOrEmpty(Email))
			{
				LabelError.Text = "Email cannot be blank";
				return false;
			}

			if (string.IsNullOrEmpty(Password))
			{
				LabelError.Text = "Password cannot be blank";
				return false;
			}

			LabelError.Text = "";
			return true;
		}

		/// <summary>
		/// Method to log user in
		/// Asynchronous
		/// Disables UI while running
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private async void Login(object sender, EventArgs args)
		{
			// Return if input invalid
			if (!CheckInput())
			{
				return;
			}

			// Disable UI
			DisableUi();

			// Get response from the main controller
			var response = await App.ModelManager.Login(new UserModel
			{
				// Replace any spaces in the email (auto correct options typically add one at the end)
				Email = Email.Replace(" ", string.Empty),
				Password = Password
			});
			
			switch (response)
			{
				case LoginResponse.InvalidCredentials:
					LabelError.Text = "Invalid email or password combination";
					break;
				case LoginResponse.NoResponse:
					LabelError.Text = "Cannot connect to server";
					break;
				case LoginResponse.Success:
					LabelError.Text = "";
					
					// Open the main page
					App.Md = new MDP();
					Application.Current.MainPage = App.Md;
					break;
			}
			
			// Enable the UI
			EnableUi();
		}

		/// <summary>
		/// Method to open the Register View
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void Register(object sender, EventArgs args)
		{
			Navigation.PushAsync(new RegisterView());
		}
	}
}