using System;
using KitchenMagic.Common.DTO;

namespace KitchenMagic.Common.PO
{
	public class IngredientPO : BasePO
	{
		public Guid Id { get; set; }

		private int _count;

		public int Count
		{
			get => _count;
			set
			{
				_count = value;
				OnPropertyChanged();
			}
		}

		private Unit _unit;

		public Unit Unit
		{
			get => _unit;
			set
			{
				_unit = value;
				OnPropertyChanged();
			}
		}

		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		private bool _isTitle;

		public bool IsTitle
		{
			get => _isTitle;
			set
			{
				_isTitle = value;
				OnPropertyChanged();
			}
		}
	}
}
