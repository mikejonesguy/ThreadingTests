using System;
using System.Net.Http;
using System.Threading.Tasks;
using PCLStorage;

namespace ThreadingTests
{
	public class MyViewModel
	{
		ILogger _logger;
		long _appStart;

		public MyViewModel(ILogger logger, long appStart)
		{
			_logger = logger;
			_appStart = appStart;
		}

		public static long Now()
		{
			return DateTime.Now.Ticks / 10000;
		}

		public void Log(string message, params object[] args)
		{
			message = args == null ? message : string.Format(message, args);
			_logger.Debug("Elapsed={0}ms; Thread={1}; {2}", (Now() - _appStart), _logger.ThreadId(), message);
		}

		public async Task Start(bool configureAwait)
		{
			await FileTest().ConfigureAwait(configureAwait);
			//await NetworkTest().ConfigureAwait(configureAwait);
			//await SleepTest().ConfigureAwait(configureAwait);
		}

		async Task FileTest()
		{
			Log("FileTest Begin");

			byte[] data = new byte[128 * 1024 * 1024];
			Random rng = new Random();
			rng.NextBytes(data);

			Log("FileTest Data Ready To Write");

			try
			{
				var folder = FileSystem.Current.LocalStorage;
				IFile file = await folder.CreateFileAsync("data.txt", CreationCollisionOption.ReplaceExisting);
				using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
				{
					await fileHandler.WriteAsync(data, 0, data.Length);
				}
				await file.DeleteAsync();
			}
			catch (Exception e)
			{
				Log("FileTest Error: " + e);
			}

			Log("FileTest Complete");
		}

		async Task NetworkTest()
		{
			Log("NetworkTest Begin");

			var folder = FileSystem.Current.LocalStorage;
			IFile file = await folder.CreateFileAsync("network.txt", CreationCollisionOption.ReplaceExisting);

			try
			{
				string random = Guid.NewGuid().ToString();
				Uri url = new Uri("https://dl.dropboxusercontent.com/u/4053974/teamviewer_linux_x64.deb?foo=" + random);
				var client = new HttpClient();

				using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
				{
					var httpResponse = await client.GetAsync(url);
					byte[] dataBuffer = await httpResponse.Content.ReadAsByteArrayAsync();
					await fileHandler.WriteAsync(dataBuffer, 0, dataBuffer.Length);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				await file.DeleteAsync();
			}

			Log("NetworkTest Complete");
		}

		async Task SleepTest()
		{
			Log("SleepTest Begin");
			await Task.Delay(TimeSpan.FromMilliseconds(3000L));
			Log("SleepTest Complete");
		}
	}
}
