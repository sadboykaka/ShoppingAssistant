using ShoppingAssistant.DependencyInterfaces;
using ShoppingAssistant.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationHelper))]
namespace ShoppingAssistant.Droid.Dependencies
{
    /// <summary>
    /// Android NotificationHelper implementation
    /// </summary>
    public class NotificationHelper : INotificationHelper
    {

        /// <summary>
        /// Method to create a notification with the given text and title
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        public void CreateNotification(string title, string text)
        {
            ((MainActivity) Forms.Context).CreateNotification(title, text);
        }
    }
}