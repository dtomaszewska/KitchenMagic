using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

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

		public ICommand ChangeLoginStateCommand => new MvxCommand(ChangeLoginStateCommandAction);

		public ICommand AddCategoryCommand => new MvxCommand<CategoryPO>(AddCategoryCommandAction);

		public ICommand AddRecipeCommand => new MvxCommand<CategoryPO>(AddRecipeCommandAction);

		public ICommand EditCategoryNameCommand => new MvxCommand<CategoryPO>(EditCategoryNameCommandAction);

		public ICommand EditCategoryContentCommand => new MvxCommand<CategoryPO>(EditCategoryContentCommandAction);

		public ICommand RemoveCategoryCommand => new MvxCommand<CategoryPO>(RemoveCategoryCommandAction);

		public ICommand EditRecipeCommand => new MvxCommand<RecipePO>(EditRecipeCommandAction);

		public ICommand PrintRecipeCommand => new MvxCommand<RecipePO>(PrintRecipeCommandAction);

		public ICommand RemoveRecipeCommand => new MvxCommand<RecipePO>(RemoveRecipeCommandAction);

		public ICommand SendRecipeCommand => new MvxCommand<RecipePO>(SendRecipeCommandAction);

		public ICommand CreateShoppingListCommand => new MvxCommand(CreateShoppingListCommandAction);

		public ICommand CategorySelectedCommand => new MvxCommand<ITreeListElement>(CategorySelectedCommandAction);

		public ICommand RecipeSelectedCommand => new MvxCommand<ITreeListElement>(RecipeSelectedCommandAction);

		public string WindowTitle => Assembly.GetExecutingAssembly().GetName().Name.Split('.').FirstOrDefault();

		public override async void Start()
		{
			base.Start();
			IsUserLoggedIn = await Mvx.Resolve<IGoogleLoginService>().Login();
		}

		public bool IsUserLoggedIn
		{
			get => _isUserLoggedIn;
			set
			{
				SetProperty(ref _isUserLoggedIn, value);
				RefreshRecipeList();
				if (!_isUserLoggedIn)
					CurrentStateViewModel = new UserNotLoggedInViewModel();
				else
					CurrentStateViewModel = new RecipeListViewModel(Guid.Empty);
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

		private void ChangeLoginStateCommandAction()
		{
			if (IsUserLoggedIn)
				LogoutCommandAction();
			else
				LoginCommandAction();
		}

		private async void LoginCommandAction()
		{
			if (await Mvx.Resolve<IGoogleLoginService>().Login())
				IsUserLoggedIn = true;
			else
				MessageBox.Show("Something goes wrong :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private async void LogoutCommandAction()
		{
			if (await Mvx.Resolve<IGoogleLoginService>().Logout())
				IsUserLoggedIn = false;
			else
				MessageBox.Show("Something goes wrong :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private async Task RefreshRecipeList()
		{
			if (!IsUserLoggedIn)
				TreeViewElements.Clear();
			else
				TreeViewElements = await GetTreeElements();
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

		private void CategorySelectedCommandAction(ITreeListElement element)
		{
			if (element.Id != Guid.Empty)
				CurrentStateViewModel = new RecipeListViewModel(element.Id);
		}

		private void RecipeSelectedCommandAction(ITreeListElement element)
		{
			if (element.Id != Guid.Empty)
				CurrentStateViewModel = new RecipeViewModel(element.Id);
		}

		private void AddCategoryCommandAction(CategoryPO category) {}

		private void AddRecipeCommandAction(CategoryPO category)
		{
			if (category.Id != Guid.Empty)
				CurrentStateViewModel = new RecipeAddEditViewModel(null);
		}

		private void EditCategoryNameCommandAction(CategoryPO category) {}

		private void EditCategoryContentCommandAction(CategoryPO category) {}

		private void RemoveCategoryCommandAction(CategoryPO category) {}

		private void EditRecipeCommandAction(RecipePO recipePO)
		{
			if (recipePO.Id != Guid.Empty)
				CurrentStateViewModel = new RecipeAddEditViewModel(recipePO.Id);
		}

		private void PrintRecipeCommandAction(RecipePO recipePO) {}

		private void RemoveRecipeCommandAction(RecipePO recipePO) {}

		private void SendRecipeCommandAction(RecipePO recipePO) {}

		private void CreateShoppingListCommandAction() {}
	}
}
