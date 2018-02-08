using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenMagic.Common.DTO;
using KitchenMagic.Common.PO;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Common.Services.Stubs
{
	public class RecipeServiceStub : IRecipeService
	{
		private readonly ICategoryService _categoryService;
		private List<RecipePO> _recipes;

		public RecipeServiceStub(ICategoryService categoryService)
		{
			_categoryService = categoryService;

			Task.Run(
				async () =>
				{
					var categories = await _categoryService.GetAll();

					_recipes = new List<RecipePO>
					{
						new RecipePO
						{
							Id = Guid.NewGuid(),
							Title = "Muffinki",
							Image = "https://drive.google.com/uc?id=1WS5Xv9iLTTFa5y284bXg6HD7M3B7x2w-",
							Categories = new MvxObservableCollection<CategoryPO>(categories.Where(x => x.Name == "Desery" || x.Name == "Ciasta").ToList())
						},
						new RecipePO
						{
							Id = Guid.NewGuid(),
							Title = "Keks",
							Image = "https://www.domowe-wypieki.pl/images/content/475/Keks_3.jpg",
							Categories = new MvxObservableCollection<CategoryPO>(categories.Where(x => x.Name == "Ciasta").ToList())
						},
						new RecipePO
						{
							Id = Guid.NewGuid(),
							Title = "Stek w sosie musztardowo-miodowym",
							Image = "https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/poledwiczki_w_sosie_musztardowym_01.jpg",
							Categories = new MvxObservableCollection<CategoryPO>(categories.Where(x => x.Name == "Mięso").ToList())
						},
					};
				});
		}

		public async Task<List<RecipePO>> GetAll(Guid categoryId)
		{
			await Task.Delay(1);
			return categoryId == Guid.Empty ? _recipes : _recipes.Where(x => x.Categories.Any(y => y.Id == categoryId)).ToList();
		}

		public async Task<RecipePO> Get(Guid recipeId)
		{
			var recipe = new RecipePO
			{
				Id = Guid.NewGuid(),
				Title = "Muffinki",
				Image = "http://apps.reimaginesoft.com/bus/storage/images/10927/muffinki-snickersowe-3-jpg",
				Calories = 1200,
				CookTime = new TimeSpan(1, 20, 0),
				ReadyIn = new TimeSpan(1, 20, 0),
				Directions =
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus finibus ligula laoreet magna laoreet, vel egestas dolor efficitur. In ultricies dolor eget dui fringilla, at faucibus diam eleifend. Phasellus id euismod magna. Phasellus vitae augue hendrerit, pulvinar libero at, cursus felis. Sed egestas diam semper nisi cursus, scelerisque suscipit ipsum elementum. Aenean tempus id eros sit amet convallis. In elit lacus, egestas ac massa sit amet, posuere semper odio. Aliquam quis tellus lacinia, sagittis dolor vulputate, semper dolor. Donec blandit semper scelerisque. Cras nec tincidunt elit. Aliquam dignissim varius leo eu tempor. Sed nibh quam, rutrum ac arcu sed, egestas convallis ante. Maecenas dignissim, sem vel congue vestibulum, nulla mauris pretium augue, nec dictum justo elit ac diam. Nunc sodales semper metus auctor fermentum. Quisque bibendum, sem ac porta bibendum, dolor erat ultricies risus, quis varius nunc neque eget purus.",
				Ingredients = new MvxObservableCollection<IngredientPO>()
				{
					new IngredientPO() { Count = 2, Name = "Jajka", Unit = Unit.item },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Name = "Tytuł1", IsTitle = true },
					new IngredientPO() { Count = 125, Name = "jfhfghgvhkfghgchkfyr", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
					new IngredientPO() { Count = 125, Name = "Mleko", Unit = Unit.ml },
				},
				Categories = new MvxObservableCollection<CategoryPO>(await _categoryService.GetAll())
			};

			recipe.Categories.RemoveAt(0);
			return recipe;
		}

		public async Task Add(RecipePO recipe)
		{
			await Task.Delay(1);
			_recipes.Add(recipe);
		}

		public async Task Update(RecipePO recipe)
		{
			await Task.Delay(1);
			var rec = _recipes.FirstOrDefault(x => x.Id == recipe.Id);
			if (rec != null)
			{
				_recipes.Remove(rec);
				_recipes.Add(recipe);
			}
		}

		public async Task Delete(RecipePO recipe)
		{
			await Task.Delay(1);
			var rec = _recipes.FirstOrDefault(x => x.Id == recipe.Id);
			if (rec != null)
				_recipes.Remove(rec);
		}
	}
}
