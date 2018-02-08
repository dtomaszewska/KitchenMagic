using System.Windows.Threading;
using KitchenMagic.Common.Services;
using KitchenMagic.Common.Services.Stubs;
using KitchenMagic.Wpf.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;

namespace KitchenMagic.Wpf.MvxApp
{
	public class Setup : MvxWpfSetup
	{
		public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) : base(uiThreadDispatcher, presenter) {}

		protected override IMvxApplication CreateApp()
		{
			return new MvxApp();
		}

		public override void Initialize()
		{
			base.Initialize();
			RegisterTypes();
		}

		private void RegisterTypes()
		{
			Mvx.RegisterType<IGoogleLoginService, GoogleLoginServiceWpf>();
			Mvx.LazyConstructAndRegisterSingleton<IGoogleDriveService, GoogleDriveService>();
			Mvx.LazyConstructAndRegisterSingleton<ICategoryService, CategoryServiceStub>();
			Mvx.LazyConstructAndRegisterSingleton<IRecipeService, RecipeServiceStub>();
			Mvx.LazyConstructAndRegisterSingleton<IRecipeEditionService, RecipeEditionService>();
		}
	}
}
