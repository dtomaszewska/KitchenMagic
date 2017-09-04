using System;
using System.Collections.Generic;

namespace KitchenMagic.Common.DTO
{
	public class Category
	{
		public Guid Id;
		public string Name;
		public Category ParentCategory;
		public List<Category> ChildCategories;
	}
}
