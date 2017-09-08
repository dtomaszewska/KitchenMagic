﻿using KitchenMagic.Common.PO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenMagic.Common.Services.Stubs
{
	public class CategoryServiceStub : ICategoryService
	{
		List<CategoryPO> _categories;
		public CategoryServiceStub()
		{
			_categories = new List<CategoryPO>
			{
				new CategoryPO { Id = System.Guid.NewGuid(), Name = "Mięso" },
				new CategoryPO { Id = System.Guid.NewGuid(), Name = "Ciasta" },
				new CategoryPO { Id = System.Guid.NewGuid(), Name = "Desery" },
			};
			_categories.LastOrDefault().AddChild(new CategoryPO
				{ Id = System.Guid.NewGuid(), Name = "Czekoladowe" });
			_categories.LastOrDefault().AddChild(new CategoryPO
			{ Id = System.Guid.NewGuid(), Name = "Z owocami" });
		}

		public async Task<List<CategoryPO>> GetAll()
		{
			await Task.Delay(1);
			return _categories;
		}
		public async Task Add(CategoryPO category)
		{
			await Task.Delay(1);
			_categories.Add(category);
		}
		public async Task Update(CategoryPO category)
		{
			await Task.Delay(1);
			var cat = _categories.FirstOrDefault(x => x.Id == category.Id);
			if (cat != null)
			{
				_categories.Remove(cat);
				_categories.Add(category);
			}
		}
		public async Task Delete(CategoryPO category)
		{
			await Task.Delay(1);
			var cat = _categories.FirstOrDefault(x => x.Id == category.Id);
			if (cat != null)
			{
				_categories.Remove(cat);
			}
		}
	}
}
