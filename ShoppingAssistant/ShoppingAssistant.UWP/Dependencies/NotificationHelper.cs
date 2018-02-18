using ShoppingAssistant.DependencyInterfaces;
using ShoppingAssistant.UWP.Dependencies;
using Xamarin.Forms;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

[assembly: Dependency(typeof(NotificationHelper))]
namespace ShoppingAssistant.UWP.Dependencies
{
    /// <summary>
    /// UWP NotificationHelper implementation
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
            CreateTile(title, text);
        }

        /// <summary>
        /// Method to create the tile with notification information
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        private void CreateTile(string title, string text)
        {
            // Construct the tile content
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title
                                },

                                new AdaptiveText()
                                {
                                    Text = text,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                },
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = title,
                                    HintStyle = AdaptiveTextStyle.Subtitle
                                },

                                new AdaptiveText()
                                {
                                    Text = text,
                                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                                }
                            }
                        }
                    }
                }
            };

            // Create the tile notification
            var notification = new TileNotification(content.GetXml());
            
            // Send the notification to the primary tile
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
