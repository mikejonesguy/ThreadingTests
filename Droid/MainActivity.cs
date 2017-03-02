using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace ThreadingTests.Droid
{
	[Activity(Label = "ThreadingTests", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		MyViewModel _viewModel;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			var app = App.Current;
			_viewModel = new MyViewModel(new Logger(), app.Start);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { 
				_viewModel.Log("Button.Click()");
				button.Text = string.Format("{0} clicks!", count++); 
			};

			_viewModel.Log("OnCreate() Complete");
		}

		protected override void OnResume()
		{
			base.OnResume();
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


			_viewModel.Log("OnResume() Complete - Method Elapsed {0}ms", (MyViewModel.Now() - methodStartTime));
		}
	}
}

