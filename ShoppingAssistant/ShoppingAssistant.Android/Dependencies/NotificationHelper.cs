using ShoppingAssistant.DependencyInterfaces;
using ShoppingAssistant.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationHelper))]
namespace ShoppingAssistant.Droid.Dependencies
{
    internal class NotificationHelper : INotificationHelper
    {

        public void CreateNotification(string text, string title)
        {
            ((MainActivity) Forms.Context).CreateNotification(text, title);
        }
    }
}