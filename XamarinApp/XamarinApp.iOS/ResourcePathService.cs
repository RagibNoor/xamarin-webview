using Foundation;
using XamarinApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(ResourcePathService))]
namespace XamarinApp.iOS
{
    public class ResourcePathService: IResourcePath
    {
        public string GetResourcePath()
        {
            return NSBundle.MainBundle.ResourcePath;
        }
    }
}
