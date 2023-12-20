using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace XamarinApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage(string resourcePath)
		{
           
            InitializeComponent();
            var webserverUrl = new UrlWebViewSource
            {
                Url = "http://localhost:4200/"
            };

            WebView.Source = webserverUrl;
            string htmlIndexFilePath = "dist/index.html"; 
            var source = new UrlWebViewSource
            {
                Url = Path.Combine(resourcePath, htmlIndexFilePath)
            };
            WebViewCustom.Source = source;
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
        private void DisplayAuthenticationError()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Authentication Failed", "Could not authenticate the user. Please try later.", "Ok");
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
