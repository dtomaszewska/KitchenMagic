﻿using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Common.PO
{
	public class CategoryPO : BasePO, ITreeListElement
	{
		private CategoryPO _parentCategory;

		public Guid Id { get; set; }

		public string Name { get; set; }

		public MvxObservableCollection<CategoryPO> ChildCategories { get; }

		public CategoryPO()
		{
			ChildCategories = new MvxObservableCollection<CategoryPO>();
		}

		public CategoryPO ParentCategory
		{
			get => _parentCategory;
			set
			{
				if (value == _parentCategory)
					return;

				_parentCategory = value;
				_parentCategory.AddChild(this);
			}
		}

		public MvxObservableCollection<ITreeListElement> ChildList { get; set; }

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

	public static class CategoryPOExtensions
	{
		public static CategoryPO FirstOrDefault(this List<CategoryPO> node, Func<CategoryPO, bool> predicate)
		{
			return node?.Select(child => FirstOrDefault(child, predicate)).FirstOrDefault(found => found != null);
		}

		public static CategoryPO FirstOrDefault(this CategoryPO node, Func<CategoryPO, bool> predicate)
		{
			if (node == null)
				return null;

			if (predicate.Invoke(node))
				return node;

			foreach (var child in node.ChildCategories)
			{
				var found = FirstOrDefault(child, predicate);
				if (found != null)
					return found;
			}

			return null;
		}
	}
}
