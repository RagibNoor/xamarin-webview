using System;
using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AppOAuthConfigurationService))]
namespace OfflineApp.iOS.Implementations
{
    public class AppOAuthConfigurationService: IAppOAuthConfiguration 
    {
        public string GetConfiguration()
        {
            return Base64Decode(NSBundle.MainBundle.ObjectForInfoDictionary("PMIE2ESettings").ToString());
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}