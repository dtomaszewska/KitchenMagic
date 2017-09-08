using System;
using System.Collections.Generic;
using System.Linq;

namespace KitchenMagic.Common.PO
{
	public class CategoryPO : BasePO
	{
		private CategoryPO _parentCategory;

		public Guid Id { get; set; }

		public string Name { get; set; }

		public List<CategoryPO> ChildCategories { get; }

		public CategoryPO()
		{
			ChildCategories = new List<CategoryPO>();
		}

		public CategoryPO ParentCategory
		{
			get { return _parentCategory; }
			set
			{
				if (value == _parentCategory)
					return;

				_parentCategory = value;
				_parentCategory.AddChild(this);
			}
		}

		public void AddChild(CategoryPO category)
		{
			if (ChildCategories.FirstOrDefault(x => x.Id == category.Id) == null)
			{
				ChildCategories.Add(category);
				OnPropertyChanged(nameof(ChildCategories));
			}
		}

		public void RemoveChild(CategoryPO category)
		{
			var cat = ChildCategories.FirstOrDefault(x => x.Id == category.Id);
			if (cat != null)
			{
				ChildCategories.Remove(cat);
				OnPropertyChanged(nameof(ChildCategories));
			}
		}
	}
}
