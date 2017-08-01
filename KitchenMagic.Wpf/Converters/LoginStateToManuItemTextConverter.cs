using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KitchenMagic.Wpf.Converters
{
	internal class LoginStateToManuItemTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							object parameter, CultureInfo culture)
		{
			bool state = value != null && (bool)value;
			return state ? Application.Current.Resources["Logout"] : Application.Current.Resources["Login"];
		}

		public object ConvertBack(object value, Type targetType,
								object parameter, CultureInfo culture)
		{
			return true;
		}
	}
}
