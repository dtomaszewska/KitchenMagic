using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	class IsProperTypeToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			if (value?.GetType().Name == parameter?.ToString())
				return Visibility.Visible;

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
