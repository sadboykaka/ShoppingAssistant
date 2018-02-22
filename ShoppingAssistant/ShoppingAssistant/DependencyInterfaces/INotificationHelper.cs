namespace ShoppingAssistant.DependencyInterfaces
{
    /// <summary>
    /// Interface for the NotificationHelper dependencies
    /// </summary>
    public interface INotificationHelper
    {
        /// <summary>
        /// Method to create a notification on the device with the given title and text
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        void CreateNotification(string title, string text);
    }
}
