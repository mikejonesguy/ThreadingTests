using System;
using Android.App;

namespace ThreadingTests.Droid
{
	[Application]
	public class App : Application
	{
		public static App Current { get; private set; }

		public App(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
			: base(handle, transfer)
		{
			Current = this;
		}

		long _start = MyViewModel.Now();

		public long Start => _start;

		public override void OnCreate()
		{
			base.OnCreate();
		}
	}
}
