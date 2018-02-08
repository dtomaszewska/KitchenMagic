using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using KitchenMagic.Common.Services;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.Services
{
	internal class GoogleLoginServiceWpf : IGoogleLoginService
	{
		private static string _credentialPath;
		private static readonly string[] Scopes = { DriveService.Scope.Drive };

		public Task<string> GetUserInfo()
		{
			return null;
		}

		public async Task<bool> Login()
		{
			UserCredential credential;

			using (var stream =
				new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
			{
				var credPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
				_credentialPath = new Uri(Path.Combine(credPath, ".credentials/google-drive.json")).LocalPath;

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
						GoogleClientSecrets.Load(stream).Secrets,
						Scopes,
						"user",
						CancellationToken.None,
						new FileDataStore(_credentialPath, true))
					.Result;
				Console.WriteLine(@"Credential file saved to: " + _credentialPath);
			}

			await Mvx.Resolve<IGoogleDriveService>().CreateDirectories(credential);

			return true;
		}

		public async Task<bool> Logout()
		{
			await Task.Delay(1);
			if (Directory.Exists(_credentialPath))
				try
				{
					foreach (var file in Directory.GetFiles(_credentialPath))
						File.Delete(file);

					Directory.Delete(_credentialPath);

					return true;
				}
				catch
				{
					return false;
				}

			return false;
		}
	}
}
