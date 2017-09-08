using KitchenMagic.Common.PO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenMagic.Common.Services
{
	public interface IRecipeService
	{
		Task<List<RecipePO>> GetAll(Guid categoryId);
		Task<RecipePO> Get(Guid recipeId);
		Task Add(RecipePO recipe);
		Task Update(RecipePO recipe);
		Task Delete(RecipePO recipe);
	}
}
