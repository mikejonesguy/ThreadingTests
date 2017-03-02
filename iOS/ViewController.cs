using System;
using System.Threading.Tasks;
using UIKit;

namespace ThreadingTests.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;
		MyViewModel _viewModel;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_viewModel = new MyViewModel(new Logger(), AppDelegate.Current.Start);

			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate
			{
				_viewModel.Log("Button.TouchUpInside()");
				var title = string.Format("{0} clicks!", count++);
				Button.SetTitle(title, UIControlState.Normal);
			};

			_viewModel.Log("ViewDidLoad() Complete");
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			var configureAwait = false;
			var methodStartTime = MyViewModel.Now();


			//_viewModel.Log("Test - 'await Start();' pattern - ConfigureAwait({0})", configureAwait);
			//await _viewModel.Start(configureAwait);
			//_viewModel.Log("Test Complete()");


			//_viewModel.Log("Test - 'Start().ContinueWith();' pattern - ConfigureAwait({0})", configureAwait);
			//_viewModel.Start(configureAwait).ContinueWith(task =>
			//{
			//	_viewModel.Log("Test Complete()");
			//});


			_viewModel.Log("Test - 'Task.Run(async () => await Start());' pattern - ConfigureAwait({0})", configureAwait);
			Task.Run(async () =>
			{
				await _viewModel.Start(configureAwait);
				_viewModel.Log("Test Complete()");
			});


			_viewModel.Log("ViewDidAppear() Complete - Method Elapsed {0}ms", (MyViewModel.Now() - methodStartTime));
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
