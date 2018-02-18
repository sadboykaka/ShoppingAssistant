using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAssistant.UWP.Services
{
    /// <summary>
    /// BackgroundTask that checks for shopping lists async and new locations
    /// </summary>
    public class BackgroundTask
    {
        public void GetData()
        {
            ShoppingAssistant.App.Log.Debug("BackgroundTask", "Woken in background");

            ShoppingAssistant.App.MasterController.ShoppingListController.GetShoppingListModelsAsync();
            ShoppingAssistant.App.MasterController.LocationController.GetNearbyLocations();

            ShoppingAssistant.App.Log.Debug("BackgroundTask", "Finished background processing");
        }
    }
}
