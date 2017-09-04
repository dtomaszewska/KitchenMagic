using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KitchenMagic.Common.PO
{
	public class CategoryPO : INotifyPropertyChanged
	{
		private CategoryPO _parentCategory;

		public Guid Id { get; set; }

		public string Name { get; set; }

		public List<CategoryPO> ChildCategories { get; }

		public event PropertyChangedEventHandler PropertyChanged;

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

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
