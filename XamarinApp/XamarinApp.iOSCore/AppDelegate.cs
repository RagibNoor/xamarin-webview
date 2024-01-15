using System.Globalization;
using System.Threading;
using CoreFoundation;
using Foundation;
using MauiAppShared;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using UIKit;

[assembly: Preserve(typeof(CFNotificationCenter), AllMembers = true)]
[assembly: Preserve(typeof(SQLitePCL.Batteries), AllMembers = true)]

namespace XamarinApp.iOSCore
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        MauiContext _mauiContext;

        public override UIWindow? Window
        {
            get;
            set;
        }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // SQLitePCL.Batteries.Init();
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var controller = new ViewController();
            controller.View.BackgroundColor = UIColor.White;
            Window.RootViewController = controller;

            Window.MakeKeyAndVisible();
            RegisterLockScreenAbort();
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            // Register the Window
            builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(UIWindow), Window));
            MauiApp mauiApp = builder.Build();
            _mauiContext = new MauiContext(mauiApp.Services);
            MessagingCenter.Subscribe<object, object>(this, "ShowMainScreen", (sender, args) =>
            {
                base.FinishedLaunching(app, options);
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            });


        //    return true;
        //}
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Microsoft.Maui.Controls.Application.Current is App && null != url)
            {
               // myApp.OnAppLinkRequestReceived(new Uri(url.AbsoluteString));
            }
            return true;
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
