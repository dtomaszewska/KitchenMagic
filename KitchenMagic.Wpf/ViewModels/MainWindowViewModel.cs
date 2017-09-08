using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using KitchenMagic.Wpf.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenMagic.Wpf.ViewModels
{
	internal class MainWindowViewModel : MvxViewModel
	{
		private bool _isUserLoggedIn;
		private readonly ICategoryService _categoryService;
		private MvxObservableCollection<ITreeListElement> _treeViewElements;
		private MvxViewModel _currentStateViewModel;
		private readonly IRecipeService _recipeService;

		public MainWindowViewModel()
		{
			_categoryService = Mvx.Resolve<ICategoryService>();
			_recipeService = Mvx.Resolve<IRecipeService>();
		}

		public override async void Start()
		{
			base.Start();
			TreeViewElements = await GetTreeElements();
			IsUserLoggedIn = IsUserLoggedIn;
		}

		private async Task<MvxObservableCollection<ITreeListElement>> GetTreeElements()
		{
			var treeElements = new MvxObservableCollection<ITreeListElement>();
			var categories = await _categoryService.GetAll();
			treeElements.AddRange(categories);
			foreach (var treeElement in treeElements)
			{
				treeElement.ChildList = new MvxObservableCollection<ITreeListElement>();
				treeElement.ChildList.AddRange((treeElement as CategoryPO)?.ChildCategories);
				var recipe = await _recipeService.GetAll(treeElement.Id);
				treeElement.ChildList.AddRange(recipe);
			}

			return treeElements;
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

		public ICommand CategorySelectedCommand => new MvxCommand<ITreeListElement>(CategorySelectedCommandAction);

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

		public MvxObservableCollection<ITreeListElement> TreeViewElements
		{
			get => _treeViewElements;
			set => SetProperty(ref _treeViewElements, value);
		}

		public MvxViewModel CurrentStateViewModel
		{
			get => _currentStateViewModel;
			set => SetProperty(ref _currentStateViewModel, value);
		}

		private void CategorySelectedCommandAction(ITreeListElement element)
		{
			if (element is CategoryPO cat)
			{
				if (cat.Id != Guid.Empty)
					CurrentStateViewModel = new RecipeListViewModel(cat.Id);
			}
			else if (element is RecipePO rec)
			{
				if (rec.Id != Guid.Empty)
					CurrentStateViewModel = new RecipeViewModel(rec.Id);
			}
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
