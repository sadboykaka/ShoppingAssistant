using System.Linq;
using System.Threading.Tasks;
using ShoppingAssistant.APIClasses;
using ShoppingAssistant.DatabaseClasses;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.Controllers
{
    /// <summary>
    /// Login Controller class
    /// </summary>
    public class LoginController
    {
        /// <summary>
        /// API helper
        /// </summary>
        private readonly LoginAPIHelper apiHelper;

        /// <summary>
        /// Database helper
        /// </summary>
        private readonly DatabaseHelper dbHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="apiHelper"></param>
        public LoginController(string dbName, LoginAPIHelper apiHelper)
        {
            this.apiHelper = apiHelper;
            dbHelper = new DatabaseHelper(dbName);
        }

        /// <summary>
        /// Method to login as a user on the API.
        /// Saves the login info to the local database if successful
        /// Logs in using local database credentials if no response from API
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(UserModel user)
        {
            // Attempt to log in to the API
            var apiResponse = await apiHelper.Login(user);

            switch (apiResponse)
            {
                // If success then save the login to the database
                case LoginResponse.Success:
                    user.Password = Crypt.Crypt.Encrypt(user.Password);
                    App.Log.Debug("Login", "Saving credentials for user " + user.Email);
                    await dbHelper.SaveItemsAsync(user);
                    break;
                // If invalid, return
                case LoginResponse.InvalidCredentials:
                    break;
                // If we cannot connect to the API then we try to log in from the credentials stored in the database
                case LoginResponse.NoResponse:
                    var localUsers = await dbHelper.GetItemsAsync<UserModel>();
                    if (localUsers.Any(dbUser => Crypt.Crypt.Encrypt(user.Password) == dbUser.Password))
                    {
                        App.Log.Debug("Login", "Logged user " + user.Email + " in from the local database");
                        return LoginResponse.Success;
                    }
                    break;
            }

            return apiResponse;
        }

        /// <summary>
        /// Method to register the user on the API then save the details to the local database is successful
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Register(UserModel user)
        {
            // Attempt to register on the API
            var apiResponse = await apiHelper.Register(user);

            switch (apiResponse)
            {
                // Save the login details to the local database
                case LoginResponse.Success:
                    App.Log.Debug("Register", "Registered user " + user.Email + " on the API, saving to local database");
                    user.Password = Crypt.Crypt.Encrypt(user.Password);
                    dbHelper.SaveItemsAsync(user);
                    break;
                case LoginResponse.InvalidCredentials:
                case LoginResponse.NoResponse:
                    break;
            }

            return apiResponse;
        }
    }
}
