using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Controllers;
using Xamarin.Forms;

namespace ShoppingAssistant.Models
{
    public class ModelManager
    {
        private readonly LoginApiHelper helper;

        private readonly ShoppingListController shoppingListController;
        public ShoppingListController ShoppingListController { get { return shoppingListController; } }

        private readonly LocationController locationController;

        public LocationController LocationController { get { return locationController; } }

        public LoginController LoginController { get; }

        /// <summary>
        /// Distinct collection of items (names)
        /// </summary>
        public SortedSet<string> Items { get; private set; }

        private const string BaseApiUrl = "https://rails-tutorial-benhudds.c9users.io/";

        private const string LocalDatabaseBaseName = "TodoSQLite1.db3";

        private readonly string localDatabaseName =
            DependencyService.Get<IFileHelper>().GetLocalFilePath(LocalDatabaseBaseName);

        public ModelManager()
        {
            helper = new LoginApiHelper(BaseApiUrl);

            this.Items = new SortedSet<string>();

            this.shoppingListController = new ShoppingListController(localDatabaseName, helper);
            this.locationController = new LocationController(localDatabaseName, BaseApiUrl, helper);
            this.LoginController = new LoginController(localDatabaseName, helper);
        }

        /// <summary>
        /// Method to add an item to the Items collection
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(string item)
        {
            Items.Add(item);
            //if (!Items.Contains(item)) Items.Add(item);
        }

        public async Task<LoginResponse> Login(UserModel user)
        {
            var response = await LoginController.Login(user);

            switch (response)
            {
                case LoginResponse.Success:
                    ShoppingListController.GetShoppingListModelsAsync();
                    break;
                case LoginResponse.InvalidCredentials:
                case LoginResponse.NoResponse:
                    break;
            }

            return response;
        }

        public async Task<LoginResponse> Register(UserModel user)
        {
            var response = await LoginController.Register(user);

            switch (response)
            {
                case LoginResponse.Success:
                    ShoppingListController.GetShoppingListModelsAsync();
                    break;
                case LoginResponse.InvalidCredentials:
                case LoginResponse.NoResponse:
                    break;
            }

            return response;
        }

        public void Logout()
        {
            this.ShoppingListController.ShoppingListModels.Clear();
            this.LocationController.LocationModels.Clear();
        }

    }
}
