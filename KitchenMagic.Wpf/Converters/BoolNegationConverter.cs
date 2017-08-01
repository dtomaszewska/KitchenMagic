using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	class BoolNegationToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			bool state = value != null && (bool)value;
			return state ? Visibility.Hidden : Visibility.Visible;
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
