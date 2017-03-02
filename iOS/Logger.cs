using System;
using static System.Diagnostics.Debug;

namespace ThreadingTests.iOS
{
	public class Logger : ILogger
	{
		const string MsgFormat = "{0}: {1}";
		const string TagDebug = "DEBUG";
		const string TagError = "ERROR";
		const string TagWarn = "WARN";

		public void Debug(string message, params object[] args)
		{
			WriteLine(string.Format(MsgFormat, TagDebug, message), args);
		}

		public void Error(Exception exception)
		{
			WriteLine(string.Format(MsgFormat, TagError, exception.Message));
		}

		public void Error(string message, params object[] args)
		{
			WriteLine(string.Format(MsgFormat, TagError, message), args);
		}

		public void Error(Exception exception, string message, params object[] args)
		{
			WriteLine(string.Format(MsgFormat, TagError, message + "\n" + exception.Message + "\n" + exception.StackTrace), args);
		}

		public void Warn(string message, params object[] args)
		{
			WriteLine(string.Format(MsgFormat, TagWarn, message), args);
		}

		public int ThreadId()
		{
			return System.Threading.Thread.CurrentThread.ManagedThreadId;
		}
	}
}
