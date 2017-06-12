using KitchenMagic.Helpers;
using KitchenMagic.Interfaces;
using KitchenMagic.Services;
using KitchenMagic.Model;

namespace KitchenMagic
{
	public partial class App
	{
		public App()
		{
		}

		public static void Initialize()
		{
			ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
		}
	}
}