using System;
using Foundation;
using WebKit;

namespace XamarinApp.iOS
{
	 public class CustomWebViewDelegate : WKNavigationDelegate
        {
            public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
            {
                // Handle navigation errors here
                Console.WriteLine($"WebView navigation failed: {error.LocalizedDescription}");
            }

            public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
            {
                // Handle provisional navigation errors here
                Console.WriteLine($"WebView provisional navigation failed: {error.LocalizedDescription}");
            }
        }
    
}

