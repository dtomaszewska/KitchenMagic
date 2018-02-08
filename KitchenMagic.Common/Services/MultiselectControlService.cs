using System;
using System.Collections.Generic;
using System.Linq;
using KitchenMagic.Common.PO;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Common.Services
{
	public class MultiselectControlService
	{
		private List<CategoryPO> _inputCategoriesList;
		private readonly List<MultiSelectObject> _flatCategoriesList;
		private MvxObservableCollection<CategoryPO> _selectedCategories;
		private int _indentSpaceCount = 3;

		public MultiselectControlService()
		{
			_flatCategoriesList = new List<MultiSelectObject>();
			_selectedCategories = new MvxObservableCollection<CategoryPO>();
		}

		public void ChangeAllCategoriesList(List<CategoryPO> allCategoriesList)
		{
			_inputCategoriesList = allCategoriesList?.ToList();
			_flatCategoriesList.Clear();
			if (allCategoriesList != null)
				foreach (var categoryPO in allCategoriesList)
				{
					_flatCategoriesList.Add(new MultiSelectObject { Id = categoryPO.Id, Name = categoryPO.Name });
					AddCategories(categoryPO, 1);
				}

			SetProperActiveState();
		}

		public void ChangeSelectedCategoriesList(MvxObservableCollection<CategoryPO> selectedCategoriesList)
		{
			_selectedCategories = selectedCategoriesList;
			SetProperActiveState();
		}

		public IEnumerable<MultiSelectObject> GetAutocompleteSugestions(string filtredText)
		{
			if (string.IsNullOrWhiteSpace(filtredText))
				return _flatCategoriesList;

			var filtred = _flatCategoriesList.Where(x => x.IsActive && x.Name.Trim().StartsWith(filtredText, StringComparison.OrdinalIgnoreCase)).ToList();
			filtred = filtred.Select(x => new MultiSelectObject() { Id = x.Id, Name = x.Name.TrimStart(' ') }).ToList();
			return filtred;
		}

		public void AddSelectedCategory(MultiSelectObject category)
		{
			if (category != null)
			{
				_selectedCategories.Add(_inputCategoriesList.FirstOrDefault(y => y.Id == category.Id));
				_flatCategoriesList.FirstOrDefault(x => x.Id == category.Id).IsActive = false;
			}
		}

		public void RemoveSelectedCategory(CategoryPO category)
		{
			if (category != null)
			{
				_selectedCategories.Remove(_inputCategoriesList.FirstOrDefault(x => x.Id == category.Id));
				_flatCategoriesList.FirstOrDefault(x => x.Id == category.Id).IsActive = true;
			}
		}

		private void AddCategories(CategoryPO category, int indentLevel)
		{
			var indent = "".PadLeft(indentLevel * _indentSpaceCount);
			if (category.ChildCategories != null)
				foreach (var c in category.ChildCategories)
				{
					_flatCategoriesList.Add(new MultiSelectObject() { Id = c.Id, Name = indent + c.Name, ParentCategory = category });
					AddCategories(c, indentLevel + 1);
				}
		}

		private void SetProperActiveState()
		{
			if (_selectedCategories.Any() && _flatCategoriesList.Any())
				foreach (var category in _selectedCategories)
				{
					var c = _flatCategoriesList.FirstOrDefault(x => x.Id == category.Id);
					if (c != null)
						c.IsActive = false;
				}
		}

		public MvxObservableCollection<CategoryPO> GetSelectedCategories()
		{
			return _selectedCategories;
		}
	}

	public class MultiSelectObject : CategoryPO
	{
		private bool _isActive = true;

		public bool IsActive
		{
			get => _isActive;
			set
			{
				_isActive = value;
				OnPropertyChanged();
			}
		}
	}
}
