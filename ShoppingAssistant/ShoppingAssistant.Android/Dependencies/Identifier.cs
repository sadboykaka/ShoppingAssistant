using Xamarin.Forms;
using ShoppingAssistant.Droid.Dependencies;

[assembly: Dependency(typeof(Identifier))]
namespace ShoppingAssistant.Droid.Dependencies
{
    /// <summary>
    /// Android Identifier implementation
    /// </summary>
    public class Identifier : IIdentifier
    {
        /// <summary>
        /// Get a device identifier
        /// </summary>
        /// <returns></returns>
        public string GetIdentifier()
        {
            return Android.OS.Build.Serial;
        }
    }
}