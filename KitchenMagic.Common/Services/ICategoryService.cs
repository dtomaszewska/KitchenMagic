using KitchenMagic.Common.PO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KitchenMagic.Common.Services
{
	public interface ICategoryService
	{
		Task<List<CategoryPO>> GetAll();
		Task Add(CategoryPO category);
		Task Update(CategoryPO category);
		Task Delete(CategoryPO category);
	}
}
