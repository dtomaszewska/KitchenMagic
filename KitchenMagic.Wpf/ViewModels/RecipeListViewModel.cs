using System;
using System.Threading.Tasks;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.ViewModels
{
	public class RecipeListViewModel : MvxViewModel
	{
		private MvxObservableCollection<RecipePO> _recipeList;
		private Guid _categoryId;
		private readonly IRecipeService _recipeService;

		public RecipeListViewModel()
		{
			_recipeService = Mvx.Resolve<IRecipeService>();
			RecipeList = new MvxObservableCollection<RecipePO>();
		}

		public RecipeListViewModel(Guid categoryId) : this()
		{
			CategoryId = categoryId;
		}

		public Guid CategoryId
		{
			get => _categoryId;
			set
			{
				SetProperty(ref _categoryId, value);
				RefreshRecipeList();
			}
		}

		public MvxObservableCollection<RecipePO> RecipeList
		{
			get => _recipeList;
			set => SetProperty(ref _recipeList, value);
		}

		private async Task RefreshRecipeList()
		{
			RecipeList.Clear();
			foreach (var recipe in await _recipeService.GetAll(CategoryId))
				RecipeList.Add(recipe);
		}
	}
}
