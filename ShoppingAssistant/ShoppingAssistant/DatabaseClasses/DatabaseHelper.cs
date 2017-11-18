using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using Xamarin.Forms;
using SQLite;
using Xamarin.Forms.Internals;

namespace ShoppingAssistant.DatabaseClasses
{
    public class DatabaseHelper
    {
        protected readonly SQLiteAsyncConnection DatabaseAsyncConnection;

        public DatabaseHelper(string dbPath)
        {
            DatabaseAsyncConnection = new SQLiteAsyncConnection(dbPath);
            
        }

        /// <summary>
        /// Generic method to return a list of the items stored in the table for that type
        /// </summary>
        /// <typeparam name="T">Table type</typeparam>
        /// <returns>List of T stored in the table for T</returns>
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return DatabaseAsyncConnection.Table<T>().ToListAsync();
        }

        public Task<int> SaveItemsAsync<T>(T item) where T : Model, new()
        {
            if (item.LocalDbId != null)
            {
                return DatabaseAsyncConnection.UpdateAsync(item);
            }

            return DatabaseAsyncConnection.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
           return await DatabaseAsyncConnection.DeleteAsync(item); 
        }
    }


}
