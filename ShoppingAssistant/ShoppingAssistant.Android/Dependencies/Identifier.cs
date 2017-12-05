using Xamarin.Forms;
using ShoppingAssistant.Droid;

[assembly: Dependency(typeof(Identifier))]
namespace ShoppingAssistant.Droid
{
    public class Identifier : IIdentifier
    {
        public string GetIdentifier()
        {
            return Android.OS.Build.Serial;
        }
    }
}