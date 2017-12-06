using System;
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
            //DatabaseAsyncConnection.DropTableAsync<UserModel>();
            DatabaseAsyncConnection.CreateTableAsync<UserModel>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK).Wait();
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
        public async Task SaveItemsAsync<T>(T item) where T : Model, new()
        {
            //var val = DatabaseAsyncConnection.InsertOrReplaceAsync(item);

            //string sql = @"select last_insert_rowid()";
            //int lastId = await DatabaseAsyncConnection.ExecuteAsync(sql);
            //item.LocalDbId = lastId;

            //return lastId;

            Task<int> val;

            if (item.LocalDbId != null)
            {
                val = DatabaseAsyncConnection.UpdateAsync(item);
            }
            else
            {
                val = DatabaseAsyncConnection.InsertAsync(item);
            }

            try
            {
                var a = await DatabaseAsyncConnection.GetAsync<T>(dbitem => dbitem.LastUpdated == item.LastUpdated);
                var b = DatabaseAsyncConnection.GetConnection().ExecuteScalar<int>("select last_insert_rowid();");
                item.LocalDbId = a.LocalDbId;
            }
            catch (Exception e)
            {

                App.Log.Error("SaveItemsAsync", e.Message + e.StackTrace);
            }
            
        }



        /// <summary>
        /// Method to save a given user to the database
        /// Required as the email is the primary key
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SaveItemsAsync(UserModel user)
        {
            var check = GetItemsAsync<UserModel>().Result.Any(dbUser => dbUser.Email == user.Email);
            if (check)
            {
                await DatabaseAsyncConnection.UpdateAsync(user);
            }
            else
            {
                await DatabaseAsyncConnection.InsertAsync(user);
            }

            
            var newUser = await DatabaseAsyncConnection.GetAsync<UserModel>(dbUser => dbUser.Email == user.Email);
            user.LocalDbId = newUser.LocalDbId;
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
