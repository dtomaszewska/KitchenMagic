using KitchenMagic.Common.DTO;
using System;
using System.Collections.Generic;

namespace KitchenMagic.Common.PO
{
	public class RecipePO : BasePO
	{
		public RecipePO()
		{
			Ingredients = new List<Ingredient>();
			Categories = new List<CategoryPO>();
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public List<Ingredient> Ingredients { get; set; }
		public string Directions { get; set; }
		public string Image { get; set; }
		public long Calories { get; set; }
		public TimeSpan CookTime { get; set; }
		public TimeSpan ReadyIn { get; set; }
		public List<CategoryPO> Categories { get; set; }

		public void AddCategory(CategoryPO category)
		{
			Categories.Add(category);
		}
	}
}