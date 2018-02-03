using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingAssistant.DependencyInterfaces
{
    public interface INotificationHelper
    {
        void CreateNotification(string title, string text);
    }
}
