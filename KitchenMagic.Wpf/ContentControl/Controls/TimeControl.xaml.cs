using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KitchenMagic.Wpf.ContentControl.Controls
{
	/// <summary>
	/// Interaction logic for IngredientsListControl.xaml
	/// </summary>
	public partial class TimeControl : UserControl
	{
		public TimeControl()
		{
			InitializeComponent();
			Hours.TextChanged += HoursOnTextChanged;
			Minutes.TextChanged += MinutesOnTextChanged;
		}

		private void MinutesOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
		{
			OnTextChanged(sender as TextBoxControl);
		}

		private void HoursOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
		{
			OnTextChanged(sender as TextBoxControl);
		}

		private void OnTextChanged(TextBoxControl sender)
		{
			if (uint.TryParse(sender.Text, out var time))
			{
				Foreground = sender.BaseForegroundColor;
				TryUpdateTime();
			}
			else
			{
				sender.Foreground = Brushes.Firebrick;
			}
		}

		private void TryUpdateTime()
		{
			if (uint.TryParse(Hours.Text, out var hours) && uint.TryParse(Minutes.Text, out var minutes))
				Time = new TimeSpan((int)hours, (int)minutes, 0);
		}

		public TimeSpan Time
		{
			get => (TimeSpan)GetValue(TimeProperty);
			set => SetValue(TimeProperty, value);
		}

		public static readonly DependencyProperty TimeProperty =
			DependencyProperty.Register(
				nameof(Time),
				typeof(TimeSpan),
				typeof(TimeControl),
				new PropertyMetadata(
					new TimeSpan(),
					(o, args) =>
					{
						var time = (TimeSpan)args.NewValue;
						(o as TimeControl).Hours.Text = time.Hours.ToString();
						(o as TimeControl).Minutes.Text = time.Minutes.ToString();
					}));

		public Brush BaseForegroundColor
		{
			get => (Brush)GetValue(BaseForegroundColorProperty);
			set => SetValue(BaseForegroundColorProperty, value);
		}

		public static readonly DependencyProperty BaseForegroundColorProperty =
			DependencyProperty.Register(nameof(BaseForegroundColor), typeof(Brush), typeof(TimeControl));
	}
}
