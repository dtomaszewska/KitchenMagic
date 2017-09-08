using KitchenMagic.Common.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenMagic.Common.Services.Stubs
{
	public class RecipeServiceStub : IRecipeService
	{
		List<RecipePO> _recipes;
		public RecipeServiceStub()
		{
			_recipes = new List<RecipePO>
			{
				new RecipePO { Id = System.Guid.NewGuid(), Title = "Muffinki", Image = "http://apps.reimaginesoft.com/bus/storage/images/10927/muffinki-snickersowe-3-jpg"},
				new RecipePO { Id = System.Guid.NewGuid(), Title = "Keks", Image = "https://www.domowe-wypieki.pl/images/content/475/Keks_3.jpg" },
				new RecipePO { Id = System.Guid.NewGuid(), Title = "Stek w sosie musztardowo-miodowym", Image = "https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/poledwiczki_w_sosie_musztardowym_01.jpg" },
			};
		}

		public async Task<List<RecipePO>> GetAll(Guid categoryId)
		{
			await Task.Delay(1);
			//var result = _recipes.Where(x => x.Categories.Any(y => y.Id == categoryId)).ToList();
			return _recipes.Take(new Random().Next(1, _recipes.Count)).ToList();
		}

		public async Task<RecipePO> Get(Guid recipeId)
		{
			await Task.Delay(1);
			return _recipes.FirstOrDefault(x => x.Id == recipeId);
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
			{
				_recipes.Remove(rec);
			}
		}
	}
}
