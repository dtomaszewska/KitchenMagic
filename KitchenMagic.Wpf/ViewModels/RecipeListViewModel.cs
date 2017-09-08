using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Threading.Tasks;

namespace KitchenMagic.Wpf.ViewModels
{
	public class RecipeListViewModel : MvxViewModel
	{
		private MvxObservableCollection<RecipePO> _recipeList;
		private Guid _categoryId;
		private readonly IRecipeService _recipeService;

		public RecipeListViewModel(Guid categoryId)
		{
			_recipeService = Mvx.Resolve<IRecipeService>();
			RecipeList = new MvxObservableCollection<RecipePO>();

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
			RecipeList.ReplaceWith(await _recipeService.GetAll(CategoryId));
		}
	}
}
