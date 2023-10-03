using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
           
            InitializeComponent();
            WebView.Source = "http://localhost:4200/";
            webLIink.Text = "http://localhost:4200/";
            Authenticate();
        }

        private void LoadPortalApp(string url, string frontEndServerUrl)
        {
            //WebView.BaseUrl = $"{frontEndServerUrl}/";
          //  WebView.Source = $"{frontEndServerUrl}/portal/" + url;
        }
        private void LoadKpiDashboardApp(string url, string frontEndServerUrl)
        {
            //WebView.BaseUrl = $"{frontEndServerUrl}/";
          //  WebView.Source = $"{frontEndServerUrl}/kpidashboard/" + url;
        }
        private void Authenticate()
        {
           // DisplayWebView();
        }


        private void DisplayWebView()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
               //WebView.IsVisible = true;
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
            WebView.Source = webLIink.Text;
            this.DisplayWebView();
        }
    }
}
