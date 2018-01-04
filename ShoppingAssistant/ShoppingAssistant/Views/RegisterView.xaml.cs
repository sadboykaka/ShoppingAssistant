using System;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
    /// <inheritdoc />
    /// <summary>
    /// Register View
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView
    {
        /// <summary>
        /// Binding Property
        /// </summary>
        public string Name { get; set; }

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
        public RegisterView()
        {
            Title = "Shopping Assistant - Register";
            BindingContext = this;
            InitializeComponent();

            // Set event handler
            BtnRegister.Clicked += Register;
        }

        /// <summary>
        /// Method to disable the interactive UI elements (while processing is being done)
        /// </summary>
        private void DisableUi()
        {
            BtnRegister.IsEnabled = false;
            EntryEmail.IsEnabled = false;
            EntryPassword.IsEnabled = false;
            EntryName.IsEnabled = false;
        }

        /// <summary>
        /// Method to enable the interactive UI elements (after processing has finished)
        /// </summary>
        private void EnableUi()
        {
            BtnRegister.IsEnabled = true;
            EntryEmail.IsEnabled = true;
            EntryPassword.IsEnabled = true;
            EntryName.IsEnabled = true;
        }

        /// <summary>
        /// Method to check the user input
        /// </summary>
        /// <returns>True if valid, false if not</returns>
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(Name))
            {
                LabelError.Text = "Name cannot be blank";
                return false;
            }

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
        /// Method to register a user
        /// Asynchronous
        /// Disables UI while running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Register(object sender, EventArgs args)
        {
            // Return if input invalid
            if (!CheckInput())
            {
                return;
            }

            // Disable UI
            DisableUi();

            // Get response from the main controller
            var response = await App.MasterController.Register(new UserModel
            {
                Name = Name,
                Email = Email.Trim(),
                Password = Password
            });

            switch (response)
            {
                case LoginResponse.InvalidCredentials:
                    LabelError.Text = "User already registered with this email";
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
    }
}