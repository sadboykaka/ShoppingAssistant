using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingAssistant.Models
{
    public class ModelManager
    {
        private readonly ShoppingListModelManager shoppingListModelManager;
        public ShoppingListModelManager ShoppingListModelManager { get { return shoppingListModelManager; } }

        private readonly LocationModelManager locationModelManager;

        public LocationModelManager LocationModelManager { get { return locationModelManager; } }
        
        private const string BaseApiUrl = "https://rails-tutorial-benhudds.c9users.io/";

        private const string LocalDatabaseBaseName = "TodoSQLite.db3";

        private readonly string localDatabaseName =
            DependencyService.Get<IFileHelper>().GetLocalFilePath(LocalDatabaseBaseName);

        public ModelManager()
        {
            this.shoppingListModelManager = new ShoppingListModelManager(localDatabaseName, BaseApiUrl);
            this.locationModelManager = new LocationModelManager(localDatabaseName, BaseApiUrl);
        }
    }
}
