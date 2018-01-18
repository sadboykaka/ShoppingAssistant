using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Models;
using Xamarin.Forms;

namespace ShoppingAssistant.Controllers
{
    /// <summary>
    /// Master Controller class
    /// </summary>
    public class MasterController
    {
        /// <summary>
        /// Shopping Assistant API URL
        /// </summary>
        private const string BaseApiUrl = "https://rails-tutorial-benhudds.c9users.io/";

        /// <summary>
        /// Edamam recipe API URL
        /// </summary>
        private const string BaseEdamamApiUrl = " https://api.edamam.com/search";

        /// <summary>
        /// Local database name
        /// </summary>
        private const string LocalDatabaseBaseName = "TodoSQLite1.db3";

        /// <summary>
        /// Local database path
        /// </summary>
        private readonly string localDatabasePath =
            DependencyService.Get<IFileHelper>().GetLocalFilePath(LocalDatabaseBaseName);

        /// <summary>
        /// Helper for API with login
        /// </summary>
        private readonly LoginApiHelper helper;

        /// <summary>
        /// Edamam API helper class
        /// </summary>
        public EdamamApiHelper EdamamApiHelper { get; }

        /// <summary>
        /// ShoppingList Controller class
        /// </summary>
        public ShoppingListController ShoppingListController { get; }

        /// <summary>
        /// Location Controller class
        /// </summary>
        public LocationController LocationController { get; }

        /// <summary>
        /// Login Controller class (for use with api and local database)
        /// </summary>
        public LoginController LoginController { get; }
        
        /// <summary>
        /// Distinct collection of items (names)
        /// </summary>
        public SortedSet<string> Items { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MasterController()
        {
            try
            {
                helper = new LoginApiHelper(BaseApiUrl);

                Items = new SortedSet<string>();

                ShoppingListController = new ShoppingListController(localDatabasePath, helper);
                LocationController = new LocationController(localDatabasePath, BaseApiUrl, helper);
                LoginController = new LoginController(localDatabasePath, helper);
                EdamamApiHelper = new EdamamApiHelper(BaseEdamamApiUrl);
            }
            catch (Exception e)
            {
                App.Log.Error("Constructor", e.StackTrace);
            }
        }

        /// <summary>
        /// Method to add an item to the Items collection
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(string item)
        {
            if (item != null)
            {
                Items.Add(item);
            }
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
