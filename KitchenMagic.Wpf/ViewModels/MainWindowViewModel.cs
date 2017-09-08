using System;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using KitchenMagic.Wpf.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace KitchenMagic.Wpf.ViewModels
{
	internal class MainWindowViewModel : MvxViewModel
	{
		private bool _isUserLoggedIn;
		private readonly ICategoryService _categoryService;
		private MvxObservableCollection<CategoryPO> _categories;
		private MvxViewModel _currentStateViewModel;

		public MainWindowViewModel()
		{
			_categoryService = Mvx.Resolve<ICategoryService>();
		}

		public override async void Start()
		{
			base.Start();
			Categories = new MvxObservableCollection<CategoryPO>(await _categoryService.GetAll());
			IsUserLoggedIn = IsUserLoggedIn;
		}

		public ICommand AddCategoryCommand => new RelayCommand(AddCategoryCommandAction);

		public ICommand AddRecipeCommand => new RelayCommand(AddRecipeCommandAction);

		public ICommand ChangeLoginStateCommand => new RelayCommand(ChangeLoginStateCommandAction);

		public ICommand EditCategoriesCommand => new RelayCommand(EditCategoriesCommandAction);

		public ICommand EditRecipeCommand => new RelayCommand(EditRecipeCommandAction);

		public ICommand LoginCommand => new RelayCommand(LoginCommandAction);

		public ICommand PrintRecipeCommand => new RelayCommand(PrintRecipeCommandAction);

		public ICommand RemoveRecipeCommand => new RelayCommand(RemoveRecipeCommandAction);

		public ICommand SendRecipeCommand => new RelayCommand(SendRecipeCommandAction);

		public ICommand CategorySelectedCommand => new MvxCommand<Guid>(CategorySelectedCommandAction);

		public string WindowTitle => Assembly.GetExecutingAssembly().GetName().Name.Split('.').FirstOrDefault();

		public bool IsUserLoggedIn
		{
			get => _isUserLoggedIn;
			set
			{
				SetProperty(ref _isUserLoggedIn, value);
				if (!_isUserLoggedIn)
					CurrentStateViewModel = new UserNotLoggedInViewModel();
			}
		}

		public MvxObservableCollection<CategoryPO> Categories
		{
			get => _categories;
			set => SetProperty(ref _categories, value);
		}

		public MvxViewModel CurrentStateViewModel
		{
			get => _currentStateViewModel;
			set => SetProperty(ref _currentStateViewModel, value);
		}

		private void CategorySelectedCommandAction(Guid categoryId)
		{
			if (categoryId != Guid.Empty)
				CurrentStateViewModel = new RecipeListViewModel(categoryId);
		}

		private void AddCategoryCommandAction() {}

		private void AddRecipeCommandAction() {}

		private void ChangeLoginStateCommandAction()
		{
			if (IsUserLoggedIn)
				LogoutCommandAction();
			else
				LoginCommandAction();
		}

		private void EditCategoriesCommandAction() {}

		private void EditRecipeCommandAction() {}

		private async void LoginCommandAction()
		{
			if (await Mvx.Resolve<IGoogleLoginService>().Login())
				IsUserLoggedIn = true;
		}

		private async void LogoutCommandAction()
		{
			if (await Mvx.Resolve<IGoogleLoginService>().Logout())
				IsUserLoggedIn = false;
		}

		private void PrintRecipeCommandAction() {}

		private void RemoveRecipeCommandAction() {}

		private void SendRecipeCommandAction() {}
	}
}
