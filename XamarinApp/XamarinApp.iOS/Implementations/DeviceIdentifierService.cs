using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceIdentifierService))]
namespace OfflineApp.iOS.Implementations
{
    public class DeviceIdentifierService: IDeviceIdentifier
    {
        public string GetDeviceId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
        }
    }
}
