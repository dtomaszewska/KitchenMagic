using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using Microsoft.Win32;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.ViewModels
{
	public class RecipeAddEditViewModel : MvxViewModel
	{
		private readonly IRecipeService _recipeService;
		private ICategoryService _categoriesService;
		private RecipePO _recipe;
		private RecipePO _oldRecipe;
		private List<CategoryPO> _allCategories;
		private string _newImageFileName;

		public RecipeAddEditViewModel(Guid? recipeId)
		{
			_recipeService = Mvx.Resolve<IRecipeService>();
			_categoriesService = Mvx.Resolve<ICategoryService>();
			UploadFileCommand = new MvxCommand(UploadFileCommandAction);
			SaveCommand = new MvxCommand(SaveCommandAction);

			Task.Run(
				async () =>
				{
					if (recipeId != null && recipeId != Guid.Empty)
					{
						_oldRecipe = await _recipeService.Get(recipeId.Value);
						Recipe = _oldRecipe.Copy();
					}
					else
					{
						Recipe = new RecipePO { Id = Guid.NewGuid() };
					}
				});

			Task.Run(async () => { AllCategories = await _categoriesService.GetAll(); });
		}

		public RecipePO OldRecipe
		{
			get => _oldRecipe;
			set => SetProperty(ref _oldRecipe, value);
		}

		public RecipePO Recipe
		{
			get => _recipe;
			set => SetProperty(ref _recipe, value);
		}

		public List<CategoryPO> AllCategories
		{
			get => _allCategories;
			set => SetProperty(ref _allCategories, value);
		}

		public ICommand UploadFileCommand { get; set; }

		public ICommand SaveCommand { get; set; }

		private void UploadFileCommandAction()
		{
			var fileDialog = new OpenFileDialog { Filter = "Image files| *.jpg; *.jpeg; *.jpe; *.png; *.bmp" };

			var res = fileDialog.ShowDialog();
			if (res.HasValue && res.Value)
			{
				_newImageFileName = fileDialog.FileName;
				var a = new BitmapImage(new Uri(_newImageFileName, UriKind.Relative));
				Recipe.Image = new BitmapImage(new Uri(_newImageFileName, UriKind.Relative)).ToString();
			}
		}

		private void SaveCommandAction()
		{
			if (_oldRecipe == null)
				_recipeService.Add(Recipe);
			else
				_recipeService.Update(Recipe);
		}
	}
}
