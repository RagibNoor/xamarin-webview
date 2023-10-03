using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(ResourcePathService))]
namespace OfflineApp.iOS.Implementations
{
    public class ResourcePathService: IResourcePath
    {
        public string GetResourcePath()
        {
            return NSBundle.MainBundle.ResourcePath;
        }
    }
}
