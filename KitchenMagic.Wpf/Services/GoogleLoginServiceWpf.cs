using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using KitchenMagic.Common;
using KitchenMagic.Common.Services;
using File = Google.Apis.Drive.v3.Data.File;

namespace KitchenMagic.Wpf.Services
{
	internal class GoogleLoginServiceWpf : IGoogleLoginService
	{
		private static string _credentialPath;
		private static readonly string[] Scopes = { DriveService.Scope.Drive };
		private File _dbFile;

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

			// Create Drive API service.
			var service = new DriveService(
				new BaseClientService.Initializer
				{
					HttpClientInitializer = credential,
					ApplicationName = Configuration.ApplicationName
				});

			// Define parameters of request.
			var dirRequest = service.Files.List();
			dirRequest.Q = "mimeType='application/vnd.google-apps.folder' and trashed=false";
			var dirs = dirRequest.Execute();

			// Prepare app dir on Drive
			var appDir = new File();
			if ((dirs == null) || dirs.Files.All(x => x.Name != Configuration.DriveAplicationDirectory))
			{
				// Create metaData for a new Directory
				var body = new File
				{
					Name = Configuration.DriveAplicationDirectory,
					Description = "Directory for application data",
					MimeType = "application/vnd.google-apps.folder",
				};
				try
				{
					var request = service.Files.Create(body);
					request.Fields = "id";
					appDir = request.Execute();
				}
				catch (Exception e)
				{
					Console.WriteLine(@"An error occurred: " + e.Message);
					return false;
				}
			}
			else
			{
				appDir = dirs.Files.FirstOrDefault(x => x.Name == Configuration.DriveAplicationDirectory);
			}

			var listRequest = service.Files.List();
			var files = listRequest.Execute().Files;
			if ((files == null) || files.All(x => x.Name != Configuration.DriveAplicationDb))
			{
				var body = new File
				{
					Name = Configuration.DriveAplicationDb,
					Description = "File with all recipies for " + Configuration.ApplicationName,
					MimeType = "application/x-sqlite3",
					Parents = new List<string> { appDir.Id }
				};
				try
				{
					var request = service.Files.Create(body);
					request.Fields = "id";
					_dbFile = request.Execute();
				}
				catch (Exception e)
				{
					Console.WriteLine(@"An error occurred: " + e.Message);
					return false;
				}
			}
			else
			{
				_dbFile = files.FirstOrDefault(x => x.Name == Configuration.DriveAplicationDb);
			}

			return true;
		}

		public async Task<bool> Logout()
		{
			await Task.Delay(1);
			if (Directory.Exists(_credentialPath))
				try
				{
					foreach (var file in Directory.GetFiles(_credentialPath))
						System.IO.File.Delete(file);

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
