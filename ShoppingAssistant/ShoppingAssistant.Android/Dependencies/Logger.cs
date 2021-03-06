﻿using ShoppingAssistant.Logging;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShoppingAssistant.Droid.Dependencies.Log))]
namespace ShoppingAssistant.Droid.Dependencies
{
    /// <summary>
    /// Android Log implementation
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Error level message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public void Error(string tag, string message)
        {
            Android.Util.Log.Error(tag, message);
        }

        /// <summary>
        /// Warning level message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public void Warning(string tag, string message)
        {
            Android.Util.Log.Warn(tag, message);
        }

        /// <summary>
        /// Debug level message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public void Debug(string tag, string message)
        {
            Android.Util.Log.Debug(tag, message);
        }

        /// <summary>
        /// Info level message
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public void Info(string tag, string message)
        {
            Android.Util.Log.Info(tag, message);
        }
    }
}