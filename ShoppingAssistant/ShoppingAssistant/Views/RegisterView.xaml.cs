using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingAssistant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public RegisterView()
        {
            Title = "Shopping Assistant - Register";
            BindingContext = this;
            InitializeComponent();

            this.BtnRegister.Clicked += this.Register;
        }
        private void DisableUI()
        {
            this.BtnRegister.IsEnabled = false;
            this.EntryEmail.IsEnabled = false;
            this.EntryPassword.IsEnabled = false;
            this.EntryName.IsEnabled = false;
        }

        private void EnableUI()
        {
            this.BtnRegister.IsEnabled = true;
            this.EntryEmail.IsEnabled = true;
            this.EntryPassword.IsEnabled = true;
            this.EntryName.IsEnabled = true;
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                this.LabelError.Text = "Name cannot be blank";
                return false;
            }

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

        private async void Register(object sender, EventArgs args)
        {
            if (!this.CheckInput())
            {
                return;
            }

            this.DisableUI();

            var response = await App.ModelManager.Register(new UserModel
            {
                Name = this.Name,
                Email = this.Email.Replace(" ", string.Empty),
                Password = this.Password
            });

            switch (response)
            {
                case LoginResponse.InvalidCredentials:
                    this.LabelError.Text = "User already registered with this email";
                    break;
                case LoginResponse.NoResponse:
                    this.LabelError.Text = "Cannot connect to server";
                    break;
                case LoginResponse.Success:
                    this.LabelError.Text = "";

                    App.MD = new MDP();
                    Application.Current.MainPage = App.MD;

                    break;
            }

            this.EnableUI();
        }
    }
}