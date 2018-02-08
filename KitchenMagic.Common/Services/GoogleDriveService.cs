using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using MimeTypes;
using File = Google.Apis.Drive.v3.Data.File;

namespace KitchenMagic.Common.Services
{
	public class GoogleDriveService : IGoogleDriveService
	{
		private File _dbFile;

		private DriveService _service;
		private string _imagesDirId;

		public Task<bool> CreateDirectories(UserCredential credentials)
		{
			// Create Drive API service.
			_service = new DriveService(
				new BaseClientService.Initializer
				{
					HttpClientInitializer = credentials,
					ApplicationName = Configuration.ApplicationName
				});

			// Define parameters of request.
			var dirRequest = _service.Files.List();
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
					var request = _service.Files.Create(body);
					request.Fields = "id";
					appDir = request.Execute();
				}
				catch (Exception e)
				{
					Console.WriteLine(@"An error occurred: " + e.Message);
					return Task.FromResult(false);
				}
			}
			else
			{
				appDir = dirs.Files.FirstOrDefault(x => x.Name == Configuration.DriveAplicationDirectory);
			}

			// Prepare directory for the images on Drive
			if ((dirs == null) || dirs.Files.All(x => x.Name != Configuration.DriveImagesDirectory))
			{
				// Create metaData for a new Directory
				var body = new File
				{
					Name = Configuration.DriveImagesDirectory,
					Description = "Directory for images",
					MimeType = "application/vnd.google-apps.folder",
				};
				try
				{
					var request = _service.Files.Create(body);
					request.Fields = "id";
					var dir = request.Execute();
					_imagesDirId = dir.Id;
				}
				catch (Exception e)
				{
					Console.WriteLine(@"An error occurred: " + e.Message);
					return Task.FromResult(false);
				}
			}
			else
			{
				_imagesDirId = dirs.Files.FirstOrDefault(x => x.Name == Configuration.DriveImagesDirectory)?.Id;
			}

			var listRequest = _service.Files.List();
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
					var request = _service.Files.Create(body);
					request.Fields = "id";
					_dbFile = request.Execute();
				}
				catch (Exception e)
				{
					Console.WriteLine(@"An error occurred: " + e.Message);
					return Task.FromResult(false);
				}
			}
			else
			{
				_dbFile = files.FirstOrDefault(x => x.Name == Configuration.DriveAplicationDb);
			}

			return Task.FromResult(true);
		}

		public Task<string> UpdateImage(string fileName, string recipeName)
		{
			var ext = fileName.Split('.').LastOrDefault();
			var fileMetadata = new File()
			{
				Name = $"{fileName}.{ext}",
				Parents = new List<string> { _imagesDirId }
			};

			try
			{
				FilesResource.CreateMediaUpload request;
				using (var stream = new FileStream(
					fileName,
					FileMode.Open))
				{
					request = _service.Files.Create(
						fileMetadata,
						stream,
						MimeTypeMap.GetMimeType(ext));
					request.Fields = "id";
					request.Upload();
				}

				var file = request.ResponseBody;
				return Task.FromResult("https://drive.google.com/uc?id=" + file.Id);
			}
			catch (Exception e)
			{
				Console.WriteLine(@"An error occurred: " + e.Message);
				return Task.FromResult(string.Empty);
			}
		}
	}
}
