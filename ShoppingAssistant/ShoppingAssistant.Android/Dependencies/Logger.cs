using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShoppingAssistant.Droid;
using ShoppingAssistant.Logging;
using Xamarin.Forms;


[assembly: Dependency(typeof(ShoppingAssistant.Droid.Dependencies.Log))]
namespace ShoppingAssistant.Droid.Dependencies
{
    public class Log : ILog
    {
        public void Error(string tag, string message)
        {
            Android.Util.Log.Error(tag, message);
        }

        public void Warning(string tag, string message)
        {
            Android.Util.Log.Warn(tag, message);
        }

        public void Debug(string tag, string message)
        {
            Android.Util.Log.Debug(tag, message);
        }

        public void Info(string tag, string message)
        {
            Android.Util.Log.Info(tag, message);
        }
    }
}