using EmbedIO;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using EmbedIO.WebApi;
using Xamarin.Essentials;
using Xamarin.Forms;
using EmbedIO.Files;

namespace XamarinApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage(string resourcePath)
		{
           
            InitializeComponent();
            var webserverUrl = new UrlWebViewSource
            {
                Url = "http://localhost:44200/"
            };

            WebView.Source = webserverUrl;
            //string htmlIndexFilePath = "dist/index.html";
            //var source = new UrlWebViewSource
            //{
            //    Url = Path.Combine(resourcePath, htmlIndexFilePath)
            //};
            //WebViewCustom.Source = source;
            this.RunServer("http://localhost:44100/", resourcePath);
            Authenticate();
        }

        private void Authenticate()
        {
            DisplayWebView();
        }


        private void DisplayWebView()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
               WebView.IsVisible = true;
               WebView.Reload();
            });
        }

        private void DisplayCustomWebView()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WebViewCustom.IsVisible = true;
                WebViewCustom.Reload();
            });
        }
        private void RunServer(string url,  string location)
        {
            location = Path.Combine(location, "dist");
            Console.WriteLine("From main page server server");
            Console.WriteLine(location);
            Task.Factory.StartNew(async () =>
            {
                using (var server = new WebServer(HttpListenerMode.EmbedIO, ""))
                {
                    Assembly assembly = typeof(App).Assembly;
                    server.WithLocalSessionManager();
                    server.WithStaticFolder("/", location, true, m => m
                        .WithContentCaching(true));
                    await server.RunAsync();

                    var source = new UrlWebViewSource
                    {
                        Url = url
                    };
                    WebViewCustom.Source = source;
                    DisplayCustomWebView();
                }
            });
        }

        void Handle_OnNavigationStarted(object sender, WebNavigatingEventArgs e)
        {
            bool uri = Uri.TryCreate(e.Url, UriKind.Absolute, out var url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == "about" || url.Scheme == Uri.UriSchemeHttps);

            if (!uri)
            {
            }
        }

        private void DisplayExternalAppNotInstalledMessage()
        {
            DisplayAlert("Info", "External app is not installed", "Cancel");
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
          //  WebView.IsVisible = false;
           // WebView.Source = webLIink.Text;
            this.DisplayWebView();
        }
    }
}
