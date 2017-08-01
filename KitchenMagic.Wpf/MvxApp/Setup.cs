using KitchenMagic.Common.Services;
using KitchenMagic.Wpf.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;
using System.Windows.Threading;

namespace KitchenMagic.Wpf.MvxApp
{
	public class Setup: MvxWpfSetup
	{
		public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) : base(uiThreadDispatcher, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Wpf.MvxApp.MvxApp();
		}

		public override void Initialize()
		{
			base.Initialize();
			RegisterTypes();
	}
		private void RegisterTypes()
		{
			Mvx.RegisterType<IGoogleLoginService, GoogleLoginServiceWpf>();
		}
	}
}
