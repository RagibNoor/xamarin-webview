using OfflineApp.EmbedIO.LocalServer;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MauiAppShared
{
	public partial class App : Application
	{
        private  LoadLocalServer localServerService;
		public App ()
		{
			InitializeComponent();
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			var BundleResourcePath = DependencyService.Get<IResourcePath>().GetResourcePath();
            var outPutBasePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			localServerService = new LoadLocalServer(outPutBasePath, BundleResourcePath);
			var ct = new CancellationTokenSource();

            MainPage = new MainPage(outPutBasePath);
		}

		protected override void OnStart ()
		{
			Console.WriteLine("start");
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
           // localServerService.DisposeServer();
		}

		protected override void OnResume ()
		{
			//var outPutBasePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

			//localServerService = new LoadLocalServer(outPutBasePath, NSBundle.MainBundle.ResourcePath);
			//// Handle when your app resumes
			//localServerService.RunServer();
		}
	}
}
