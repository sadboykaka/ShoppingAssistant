using System;
using System.IO;
using Windows.Storage;
using ShoppingAssistant.UWP.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace ShoppingAssistant.UWP.Dependencies
{
    /// <summary>
    /// UWP FileHelper implementation
    /// </summary>
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// Return the file path for the given file name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
