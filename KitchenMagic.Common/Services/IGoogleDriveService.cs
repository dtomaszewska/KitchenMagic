using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace KitchenMagic.Common.Services
{
	public interface IGoogleDriveService
	{
		Task<bool> CreateDirectories(UserCredential credentials);

		Task<string> UpdateImage(string fileName, string recipeName);
	}
}
