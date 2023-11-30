using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(IosAppVersionService))]
namespace OfflineApp.iOS.Implementations
{
    class IosAppVersionService: IAppVersion
    {
        public string GetAppVersion()
        {
            return GetVersion()+ GetVersionAndBuildSeparator()+ GetBuild();
        }
        private string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        private string GetBuild()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
        private string GetVersionAndBuildSeparator()
        {
            return ".";
        }
    }
}