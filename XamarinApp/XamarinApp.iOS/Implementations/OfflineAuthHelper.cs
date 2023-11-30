using System;
using System.Threading.Tasks;
using LocalAuthentication;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(OfflineAuthHelper))]
namespace OfflineApp.iOS.Implementations
{
    public class OfflineAuthHelper : IOfflineAuthHelper
    {
        private readonly string AuthDisplayMessage = "Please Authenticate to proceed";

        public async Task AuthenticateAsync(Action onSuccess, Action onFailure)
        {
            var context = new LAContext();
            var result = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthentication, AuthDisplayMessage);
            if(result.Item1)
            {
                onSuccess?.Invoke();
            }
            else
            {
                onFailure?.Invoke();
            }
           
        }
    }

}
