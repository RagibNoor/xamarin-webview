using System;
using UIKit;
using System.Timers;
using Xamarin.Forms;
using Timer = System.Timers.Timer;

namespace OfflineApp.iOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public ViewController()
        {
        }
        public override bool ShouldAutorotate()
        {
            return true;
        }
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create new image view and add it to the view.
            //var logoImage = new UIImageView(UIImage.FromBundle("logo_iqvia_big.png"));


            var activity = new UIActivityIndicatorView();
            activity.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            activity.StartAnimating();

            View.AddSubview(activity);
            View.BringSubviewToFront(activity);
            //View.AddSubview(logoImage);

            // logo position
            //logoImage.TranslatesAutoresizingMaskIntoConstraints = false;
            //logoImage.CenterXAnchor.ConstraintEqualTo(this.View.CenterXAnchor).Active = true;
            //logoImage.CenterYAnchor.ConstraintEqualTo(this.View.CenterYAnchor).Active = true;
            //logoImage.WidthAnchor.ConstraintEqualTo(260).Active = true;
            //logoImage.HeightAnchor.ConstraintEqualTo(108).Active = true;

            //activity indicator position
            activity.TranslatesAutoresizingMaskIntoConstraints = false;
            activity.CenterXAnchor.ConstraintEqualTo(this.View.CenterXAnchor).Active = true;
            activity.CenterYAnchor.ConstraintEqualTo(this.View.CenterYAnchor).Active = true;
            activity.WidthAnchor.ConstraintEqualTo(102).Active = true;
            activity.HeightAnchor.ConstraintEqualTo(50).Active = true;

            // timer 
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.AutoReset = false;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InvokeOnMainThread(delegate
            {
                MessagingCenter.Send<object, object>(this, "ShowMainScreen", null);
            });

        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }


}
