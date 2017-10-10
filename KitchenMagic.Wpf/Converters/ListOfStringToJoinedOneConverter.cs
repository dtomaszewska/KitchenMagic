using KitchenMagic.Common.PO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	class ListOfStringToJoinedOneConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			var elements = value as List<CategoryPO>;
			return elements?.Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, CultureInfo culture)
		{
			if (value == null)
				return false;

			return !(bool)value;
		}
	}
}
