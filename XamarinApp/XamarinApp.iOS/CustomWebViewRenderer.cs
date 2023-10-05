using System;
using Foundation;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinApp;
using XamarinApp.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace XamarinApp.iOS
{
    public class CustomWebViewRenderer : WkWebViewRenderer
    {

        public override WKNavigation LoadRequest(NSUrlRequest request)
        {
            //var url = request.Url.ToString().Replace("html%3F", "html?");
            //var dotnetURI = new System.Uri(url);

            //var idn = new System.Globalization.IdnMapping();
            //NSUrl nsURL = new NSUrl(dotnetURI.Scheme, idn.GetAscii(dotnetURI.DnsSafeHost), dotnetURI.PathAndQuery);
            Console.WriteLine(request.Url);
            return base.LoadRequest(request);
        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            Console.WriteLine(e.ToString());
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                WebView webView = Element as WebView;
                webView.VerticalOptions = LayoutOptions.FillAndExpand;

                string jScript = @"var meta = document.createElement('meta'); " +
                    "meta.setAttribute('name', 'viewport');" +
                    "meta.setAttribute('content', 'width=device-width');" +
                    "document.getElementsByTagName('head')[0].appendChild(meta);";

                //WKUserScriptInjectionTime should be AtDocumentEnd
                var userScript = new WKUserScript((NSString)jScript, WKUserScriptInjectionTime.AtDocumentEnd, true);

                WKWebView wkWebView = this;
                wkWebView.NavigationDelegate = new CustomWebViewDelegate(); 
                WKWebViewConfiguration wkWebViewConfig = wkWebView.Configuration;
                wkWebViewConfig.UserContentController.AddUserScript(userScript);
            }
        }
    }
}

