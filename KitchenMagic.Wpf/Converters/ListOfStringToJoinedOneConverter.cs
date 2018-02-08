using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using KitchenMagic.Common.PO;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Wpf.Converters
{
	internal class ListOfStringToJoinedOneConverter : IValueConverter
	{
		public object Convert(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			if (value is MvxObservableCollection<CategoryPO> elements)
				if (elements.Any())
					return elements.Select(x => x.Name).Aggregate((x, y) => x + ", " + y);

			return string.Empty;
		}

		public object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			if (value == null)
				return false;

			return !(bool)value;
		}
	}
}
