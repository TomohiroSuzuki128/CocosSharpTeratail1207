using Foundation;
using UIKit;

namespace CocosSharpFormsTeratail1207.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : 
	global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate 
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new CocosSharpFormsTeratail1207.App ());  

			return base.FinishedLaunching (app, options);
		}
	}
}