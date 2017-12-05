using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAssistant.Models;
using SQLite;

namespace ShoppingAssistant.DatabaseClasses
{
    /// <summary>
    /// Database helper class
    /// Deals with generic grud operations
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        /// Database connection object
        /// </summary>
        protected readonly SQLiteAsyncConnection DatabaseAsyncConnection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPath"></param>
        public DatabaseHelper(string dbPath)
        {
            DatabaseAsyncConnection = new SQLiteAsyncConnection(dbPath);

            // Create the user table (if it does not already exist)
            DatabaseAsyncConnection.CreateTableAsync<UserModel>(CreateFlags.ImplicitPK).Wait();
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

        /// <summary>
        /// Generic method to save a given item to the database
        /// Requires the table to have already been created
        /// Objects must be of Model type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> SaveItemsAsync<T>(T item) where T : Model, new()
        {
            if (item.LocalDbId != null)
            {
                return DatabaseAsyncConnection.UpdateAsync(item);
            }

            return DatabaseAsyncConnection.InsertAsync(item);
        }

        /// <summary>
        /// Method to save a given user to the database
        /// Required as the email is the primary key
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> SaveItemsAsync(UserModel user)
        {
            var check = GetItemsAsync<UserModel>().Result.Any(dbUser => dbUser.Email == user.Email);
            if (check)
            {
                return DatabaseAsyncConnection.UpdateAsync(user);
            }

            return DatabaseAsyncConnection.InsertAsync(user);
        }

        /// <summary>
        /// Generic method to delete items from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
           return await DatabaseAsyncConnection.DeleteAsync(item); 
        }
    }


}
