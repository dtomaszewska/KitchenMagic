using System;
using System.Threading.Tasks;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.ViewModels
{
	internal class RecipeViewModel : MvxViewModel
	{
		private readonly IRecipeService _recipeService;
		private Guid _recipeId;

		public RecipeViewModel(Guid id)
		{
			_recipeService = Mvx.Resolve<IRecipeService>();
			this.RecipeId = id;
		}

		public Guid RecipeId
		{
			get => _recipeId;
			set
			{
				SetProperty(ref _recipeId, value);
				RefreshRecipe();
			}
		}

		private RecipePO _recipe;

		public RecipePO Recipe
		{
			get => _recipe;
			set => SetProperty(ref _recipe, value);
		}

		private async Task RefreshRecipe()
		{
			Recipe = await _recipeService.Get(RecipeId);
		}
	}
}