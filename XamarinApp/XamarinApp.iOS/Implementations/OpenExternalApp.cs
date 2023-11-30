using System;
using System.Threading.Tasks;
using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(OpenExternalApp))]
namespace OfflineApp.iOS.Implementations
{
    public class OpenExternalApp: IOpenExternalApp
    {
        public bool CanLunch(string url)
        {
            NSUrl request = new NSUrl(url);
            return UIApplication.SharedApplication.CanOpenUrl(request);
        }

        public Task<bool> Lunch(string url)
        {
            NSUrl request = new NSUrl(url);
            try
            {
                return Task.FromResult(UIApplication.SharedApplication.OpenUrl(request));
            }
            catch (Exception e)
            {
                return Task.FromResult(false);
            }
        }
    }
}