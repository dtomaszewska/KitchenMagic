using System.Threading.Tasks;

namespace KitchenMagic.Common.Services
{
	public interface IGoogleLoginService
	{
		Task<bool> Login();
		Task<bool> Logout();
		Task<string> GetUserInfo();
	}
}
