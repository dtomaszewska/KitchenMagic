using KitchenMagic.Common.PO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	class IsCategoryToFontWeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			if (value is CategoryPO)
				return FontWeights.Bold;

			return FontWeights.Normal;
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
