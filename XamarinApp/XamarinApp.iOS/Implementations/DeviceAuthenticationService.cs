using LocalAuthentication;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceAuthenticationService))]
namespace OfflineApp.iOS.Implementations
{
    public class DeviceAuthenticationService: IDeviceAuthenticationService
    {
        public bool CheckIfAuthenticationEnabled()
        {
            using (var context = new LAContext())
            {
                return context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out _)
                   || context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out _);
            }
        }
    }
}
