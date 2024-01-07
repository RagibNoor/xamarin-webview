using Foundation;
using MauiAppShared;
using Microsoft.Maui.Controls;
using XamarinApp.iOSCore;
[assembly: Dependency(typeof(ResourcePathService))]
namespace XamarinApp.iOSCore
{
    public class ResourcePathService: IResourcePath
    {
        public string GetResourcePath()
        {
            return NSBundle.MainBundle.ResourcePath;
        }
    }
}
