using KitchenMagic.Common.PO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	class IsCategoryToFontStyleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			if (value is CategoryPO)
				return (Style)Application.Current.Resources["BoldLabelStyle"];

			return (Style)Application.Current.Resources["LabelStyle"];
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
