using System;
using System.Globalization;
using System.Threading;
using CoreFoundation;
using Foundation;
using UIKit;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.iOS;
using XamarinApp;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

[assembly: Preserve(typeof(FormsWebView), AllMembers = true)]
[assembly: Preserve(typeof(FormsNavigationDelegate), AllMembers = true)]
[assembly: Preserve(typeof(FormsWebViewRenderer), AllMembers = true)]
[assembly: Preserve(typeof(CFNotificationCenter), AllMembers = true)]
[assembly: Preserve(typeof(SQLitePCL.Batteries), AllMembers = true)]

namespace OfflineApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Microsoft.Maui.MauiUIApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            SQLitePCL.Batteries.Init();
            FormsWebViewRenderer.Initialize();
            global::Xamarin.Forms.Forms.Init();
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var controller = new ViewController();
            controller.View.BackgroundColor = UIColor.White;
            Window.RootViewController = controller;

            Window.MakeKeyAndVisible();
            RegisterLockScreenAbort();
            MessagingCenter.Subscribe<object, object>(this, "ShowMainScreen", (sender, args) =>
            {
                LoadApplication(new App());
                base.FinishedLaunching(app, options);
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            });
           

            return true;
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Microsoft.Maui.Controls.Application.Current is App myApp && null != url)
            {
               // myApp.OnAppLinkRequestReceived(new Uri(url.AbsoluteString));
            }
            return true;
        }
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            return UIInterfaceOrientationMask.All;
        }

        private void RegisterLockScreenAbort()
        {
            // Start- Code to detect lock screen 
            const string notificationName = "com.apple.springboard.lockcomplete";
            var token = CFNotificationCenter.Darwin.AddObserver(
                name: notificationName,
                objectToObserve: null,
                notificationHandler: (name, userInfo) => {
                    Thread.CurrentThread.Abort();
                },
                suspensionBehavior: CFNotificationSuspensionBehavior.DeliverImmediately);
            // End- Code to cancel lock screen
        }

       

    }
}
