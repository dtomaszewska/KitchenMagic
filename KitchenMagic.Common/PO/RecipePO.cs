using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;

namespace KitchenMagic.Common.PO
{
	public class RecipePO : BasePO, ITreeListElement
	{
		public RecipePO()
		{
			Ingredients = new List<IngredientPO>();
			Categories = new List<CategoryPO>();
		}

		public Guid Id { get; set; }
		public string Title { get; set; }
		public List<IngredientPO> Ingredients { get; set; }
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

		public string Name
		{
			get => Title;
			set => Title = value;
		}

		public MvxObservableCollection<ITreeListElement> ChildList { get; set; }
	}
}