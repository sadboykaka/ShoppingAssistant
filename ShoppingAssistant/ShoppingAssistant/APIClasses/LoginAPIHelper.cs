using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.APIClasses
{
    public class LoginAPIHelper : APIHelper
    {
        public readonly string BaseUrl;
        private bool loggedIn = false;

        public LoginAPIHelper(string baseUrl) : base()
        {
            this.BaseUrl = baseUrl;
        }

        public async Task<LoginResponse> Login(UserModel user)
        {
            var response = await Login(user, BaseUrl + "auth/login");
            if (response == LoginResponse.Success)
            {
                loggedIn = true;
            }

            return response;
        }

        public async Task<LoginResponse> Register(UserModel user)
        {
            var response = await Register(user, BaseUrl + "signup");
            if (response == LoginResponse.Success)
            {
                loggedIn = true;
            }

            return response;
        }

        public new async Task<List<T>> RefreshDataAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> urlparams)
        {
            while(!loggedIn)
            {
                await Task.Delay(2000);
            }

            return await base.RefreshDataAsync<T>(BaseUrl + "/" + url, urlparams);
        }

        public new async Task<List<T>> RefreshDataAsync<T>(string url)
        {
            while (!loggedIn)
            {
                await Task.Delay(2000);
            }
            
            return await base.RefreshDataAsync<T>(BaseUrl + "/" + url);
        }
    }
}
