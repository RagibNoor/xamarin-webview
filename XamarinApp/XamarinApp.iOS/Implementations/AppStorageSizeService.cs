using System;
using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(AppStorageSizeService))]
namespace OfflineApp.iOS.Implementations
{
    public class AppStorageSizeService: IAppStorageSizeService
    {
        public double GetFreeSpace()
        {
            return NSFileManager.DefaultManager.GetFileSystemAttributes(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FreeSize;
        }
    }
}