using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.Models
{
    public class ModelManager
    {
        private readonly ShoppingListModelManager shoppingListModelManager;
        public ShoppingListModelManager ShoppingListModelManager { get { return shoppingListModelManager; } }
        
        private const string BaseApiUrl = "https://rails-tutorial-benhudds.c9users.io/";

        private const string LocalDatabaseName = "TodoSQLite.db3";

        public ModelManager()
        {
            this.shoppingListModelManager = new ShoppingListModelManager(LocalDatabaseName, BaseApiUrl);
        }
    }
}
