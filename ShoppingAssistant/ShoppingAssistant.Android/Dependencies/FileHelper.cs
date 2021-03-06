﻿using System;
using System.IO;
using ShoppingAssistant.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace ShoppingAssistant.Droid.Dependencies
{
    /// <summary>
    /// Android FileHelper implementation
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
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}