using System;
using System.IO;
using Foundation;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using UIKit;
using WebKit;

[assembly: Xamarin.Forms.Dependency(typeof(CustomCacheManager))]
namespace OfflineApp.iOS.Implementations
{
    public class CustomCacheManager: ICacheManager
    {

        public void Clear()
        {
            DeleteCachedFiles();

        }
        private void DeleteCachedFiles()
        {

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                NSHttpCookieStorage.SharedStorage.RemoveCookiesSinceDate(NSDate.DistantPast);
                var websiteDataTypes = new NSSet<NSString>(new[]
                                {
                                    WKWebsiteDataType.Cookies,
                                    WKWebsiteDataType.DiskCache,
                                    WKWebsiteDataType.IndexedDBDatabases,
                                    WKWebsiteDataType.MemoryCache,
                                    WKWebsiteDataType.OfflineWebApplicationCache,
                                    WKWebsiteDataType.SessionStorage,
                                    WKWebsiteDataType.WebSQLDatabases
                                });
                WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(websiteDataTypes, (NSArray records) =>
                {
                    for (nuint i = 0; i < records.Count; i++)
                    {
                        var record = records.GetItem<WKWebsiteDataRecord>(i);

                        WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes,
                            new[] { record }, () => { Console.Write($@"deleted: {record.DisplayName}"); });
                    }
                });

                NSUrlCache.SharedCache.RemoveAllCachedResponses();
            }
            else
            {

                NSUrlCache.SharedCache.RemoveAllCachedResponses();
                var cookies = NSHttpCookieStorage.SharedStorage.Cookies;

                foreach (var c in cookies)
                {
                    NSHttpCookieStorage.SharedStorage.DeleteCookie(c);
                }
            }


            try
            {
                DeleteLibraryFolderContents("Caches");

            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void DeleteLibraryFolderContents(string folderName)
        {
            var manager = NSFileManager.DefaultManager;
            var library = manager.GetUrls(NSSearchPathDirectory.LibraryDirectory, NSSearchPathDomain.User)[0];
            var dir = Path.Combine(library.Path, folderName);
            var contents = manager.GetDirectoryContent(dir, out NSError error);

            foreach (var c in contents)
            {
                try
                {
                    manager.Remove(Path.Combine(dir, c), out NSError errorRemove);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }
    }
}
