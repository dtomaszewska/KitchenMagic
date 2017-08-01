using KitchenMagic.Wpf.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.MvxApp
{
	class MvxApp : MvxApplication
	{
		public MvxApp()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainWindowViewModel>());
		}
	}
}
