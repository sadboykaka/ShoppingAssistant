using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.Controllers;
using Xamarin.Forms;

namespace ShoppingAssistant.Models
{
    public class ModelManager
    {
        private readonly LoginAPIHelper helper;

        private readonly ShoppingListModelManager shoppingListModelManager;
        public ShoppingListModelManager ShoppingListModelManager { get { return shoppingListModelManager; } }

        private readonly LocationModelManager locationModelManager;

        public LocationModelManager LocationModelManager { get { return locationModelManager; } }

        public LoginController LoginController { get; }

        private const string BaseApiUrl = "https://rails-tutorial-benhudds.c9users.io/";

        private const string LocalDatabaseBaseName = "TodoSQLite1.db3";

        private readonly string localDatabaseName =
            DependencyService.Get<IFileHelper>().GetLocalFilePath(LocalDatabaseBaseName);

        public ModelManager()
        {
            helper = new LoginAPIHelper(BaseApiUrl);

            this.shoppingListModelManager = new ShoppingListModelManager(localDatabaseName, helper);
            this.locationModelManager = new LocationModelManager(localDatabaseName, helper);
            this.LoginController = new LoginController(localDatabaseName, helper);
        }

        public async Task<LoginResponse> Login(UserModel user)
        {
            var response = await LoginController.Login(user);

            switch (response)
            {
                case LoginResponse.Success:
                    ShoppingListModelManager.GetShoppingListModelsAsync();
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
                    ShoppingListModelManager.GetShoppingListModelsAsync();
                    break;
                case LoginResponse.InvalidCredentials:
                case LoginResponse.NoResponse:
                    break;
            }

            return response;
        }

    }
}
