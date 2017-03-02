using Android.Util;
using System;

namespace ThreadingTests.Droid
{
	public class Logger : ILogger
	{
		const string Tag = "TEST";

		public void Debug(string message, params object[] args)
		{
			Log.Debug(Tag, message, args);
		}

		public void Error(Exception exception)
		{
			Log.Error(Tag, exception.Message);
		}

		public void Error(string message, params object[] args)
		{
			Log.Error(Tag, message, args);
		}

		public void Error(Exception exception, string message, params object[] args)
		{
			Log.Error(Tag, string.Format(message, args) + "\n" + exception.Message + "\n" + exception.StackTrace);
		}

		public void Warn(string message, params object[] args)
		{
			Log.Warn(Tag, message, args);
		}

		public int ThreadId()
		{
			return System.Threading.Thread.CurrentThread.ManagedThreadId;
		}
	}
}
