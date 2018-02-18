using System;
using Windows.System.Profile;
using ShoppingAssistant.UWP.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(Identifier))]
namespace ShoppingAssistant.UWP.Dependencies
{
    /// <summary>
    /// UWP Identifier implementation
    /// </summary>
    public class Identifier : IIdentifier
    {
        /// <summary>
        /// Get a device identifier
        /// </summary>
        /// <returns></returns>
        public string GetIdentifier()
        {
            return GetId();
        }

        /// <summary>
        /// Method to get a windows ID
        /// </summary>
        /// <returns></returns>
        private static string GetId()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
            {
                var token = HardwareIdentification.GetPackageSpecificToken(null);
                var hardwareId = token.Id;
                var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                byte[] bytes = new byte[hardwareId.Length];
                dataReader.ReadBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "");
            }

            ShoppingAssistant.App.Log.Error("GetIdentifier", "No api for device id present");
            return string.Empty;
        }
    }
}
