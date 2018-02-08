using System;
using System.Collections.Generic;
using System.Linq;
using KitchenMagic.Common.DTO;

namespace KitchenMagic.Common.Services
{
	public class RecipeEditionService : IRecipeEditionService
	{
		public List<Unit> GetUnits()
		{
			return Enum.GetValues(typeof(Unit)).Cast<Unit>().ToList();
		}
	}
}
