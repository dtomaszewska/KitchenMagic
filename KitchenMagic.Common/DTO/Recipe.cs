using System;
using System.Collections.Generic;

namespace KitchenMagic.Common.DTO
{
	public class Recipe
	{
		public Guid Id;
		public string Title;
		public List<Ingredient> Ingredients;
		public string Directions;
		public string Image;
		public long Calories;
		public TimeSpan CookTime;
		public TimeSpan ReadyIn;
		public List<Category> Categories;
	}
}