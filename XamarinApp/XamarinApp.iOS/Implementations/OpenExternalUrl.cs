using System;
using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using UIKit;
using Xamarin.Essentials;
[assembly: Xamarin.Forms.Dependency(typeof(OpenExternalUrl))]
namespace OfflineApp.iOS.Implementations
{
    public class OpenExternalUrl: IOpenExternalUrl
    {
      
        public bool Lunch(string url)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Code to run on the main thread
                    NSUrl request = new NSUrl(url);
                    UIApplication.SharedApplication.OpenUrl(request);
                    //Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred).GetAwaiter();
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return true;
            }
            
           
        }
    }
}
