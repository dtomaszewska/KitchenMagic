using System;
using System.Globalization;
using System.Windows.Data;
using KitchenMagic.Common.DTO;
using KitchenMagic.Common.PO;

namespace KitchenMagic.Wpf.Converters
{
	internal class IngredientsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IngredientPO ingredient)
			{
				if (ingredient.IsTitle)
					return ingredient.Name;

				return $"{ingredient.Name} - {ingredient.Count} {(ingredient.Unit == Unit.item ? string.Empty : ingredient.Unit.ToString())}";
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return false;

			return !(bool)value;
		}
	}
}
