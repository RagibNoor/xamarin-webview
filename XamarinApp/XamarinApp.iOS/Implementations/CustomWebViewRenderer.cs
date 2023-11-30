using OfflineApp.iOS.Implementations;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(FormsWebView), typeof(CustomWebViewRenderer))]
namespace OfflineApp.iOS.Implementations
{
    class CustomWebViewRenderer :FormsWebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FormsWebView> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.ScrollView.Bounces = false;
            }
        }
    }
}