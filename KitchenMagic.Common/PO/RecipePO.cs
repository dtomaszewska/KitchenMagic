using System;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Common.PO
{
	[Serializable]
	public class RecipePO : BasePO, ITreeListElement
	{
		public RecipePO()
		{
			Ingredients = new MvxObservableCollection<IngredientPO>();
			Categories = new MvxObservableCollection<CategoryPO>();
		}

		public Guid Id { get; set; }

		private string _title;

		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		private MvxObservableCollection<IngredientPO> _ingredients;

		public MvxObservableCollection<IngredientPO> Ingredients
		{
			get => _ingredients;
			set
			{
				_ingredients = value;
				OnPropertyChanged();
			}
		}

		private string _directions;

		public string Directions
		{
			get => _directions;
			set
			{
				_directions = value;
				OnPropertyChanged();
			}
		}

		private string _image;

		public string Image
		{
			get => _image;
			set
			{
				_image = value;
				OnPropertyChanged();
			}
		}

		private long _calories;

		public long Calories
		{
			get => _calories;
			set
			{
				_calories = value;
				OnPropertyChanged();
			}
		}

		private TimeSpan _cookTime;

		public TimeSpan CookTime
		{
			get => _cookTime;
			set
			{
				_cookTime = value;
				OnPropertyChanged();
			}
		}

		private TimeSpan _readyIn;

		public TimeSpan ReadyIn
		{
			get => _readyIn;
			set
			{
				_readyIn = value;
				OnPropertyChanged();
			}
		}

		private MvxObservableCollection<CategoryPO> _categories;

		public MvxObservableCollection<CategoryPO> Categories
		{
			get => _categories;
			set
			{
				_categories = value;
				OnPropertyChanged();
			}
		}

		public void AddCategory(CategoryPO category)
		{
			Categories.Add(category);
		}

		public string Name
		{
			get => Title;
			set => Title = value;
		}

		private MvxObservableCollection<ITreeListElement> _childList;

		public MvxObservableCollection<ITreeListElement> ChildList
		{
			get => _childList;
			set
			{
				_childList = value;
				OnPropertyChanged();
			}
		}
	}
}
