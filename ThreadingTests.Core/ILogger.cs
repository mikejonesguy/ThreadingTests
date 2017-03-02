using System;

namespace ThreadingTests
{
	/// <summary>
	/// An interface to allow messages to be written to the device log.
	/// Allows three different log levels: Debug, Warn, and Error (if supported by the platform specific logger).
	/// </summary>
	public interface ILogger
	{
		void Debug(string message, params object[] args);

		void Warn(string message, params object[] args);

		void Error(string message, params object[] args);

		void Error(Exception exception);

		void Error(Exception exception, string message, params object[] args);

		int ThreadId();
	}
}
