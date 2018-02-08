using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KitchenMagic.Wpf.ContentControl.Controls
{
	/// <summary>
	/// Interaction logic for IngredientsListControl.xaml
	/// </summary>
	public class TextBoxControl : TextBox
	{
		private bool _changeTriggerdInside;
		public Regex validation;

		public TextBoxControl()
		{
			TextChanged += TextBox_TextChanged;
			PreviewKeyDown += OnPreviewKeyDown;
		}

		private void OnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			if (Text == Placeholder)
			{
				_changeTriggerdInside = true;
				Text = string.Empty;
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (_changeTriggerdInside)
			{
				_changeTriggerdInside = false;
				return;
			}

			if (string.IsNullOrWhiteSpace(Text) && null != Placeholder)
			{
				_changeTriggerdInside = true;
				Text = Placeholder;
				Foreground = Brushes.LightGray;
			}
			else if (null != validation)
			{
				Foreground = validation.IsMatch(Text)
					? BaseForegroundColor
					: Brushes.Firebrick;
			}
			else
			{
				Foreground = BaseForegroundColor;
			}
		}

		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		public static readonly DependencyProperty PlaceholderProperty =
			DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBoxControl), new PropertyMetadata(null));

		public string ValidationPattern
		{
			get => (string)GetValue(ValidationPatternProperty);
			set => SetValue(ValidationPatternProperty, value);
		}

		public static readonly DependencyProperty ValidationPatternProperty =
			DependencyProperty.Register(
				nameof(ValidationPattern),
				typeof(string),
				typeof(TextBoxControl),
				new PropertyMetadata(
					string.Empty,
					(o, args) =>
					{
						if (!string.IsNullOrWhiteSpace(args.NewValue.ToString()))
							(o as TextBoxControl).validation = new Regex(args.NewValue.ToString());
					}));

		public Brush BaseForegroundColor
		{
			get => (Brush)GetValue(BaseForegroundColorProperty);
			set => SetValue(BaseForegroundColorProperty, value);
		}

		public static readonly DependencyProperty BaseForegroundColorProperty =
			DependencyProperty.Register(
				nameof(BaseForegroundColor),
				typeof(Brush),
				typeof(TextBoxControl),
				new PropertyMetadata(new PropertyChangedCallback((a, o) => { (a as TextBoxControl).Foreground = o.NewValue as Brush; })));
	}
}
