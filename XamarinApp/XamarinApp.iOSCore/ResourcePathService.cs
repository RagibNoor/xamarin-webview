using Foundation;
using MauiAppShared;
using Microsoft.Maui.Controls;

[assembly: Dependency(typeof(IResourcePath))]
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
