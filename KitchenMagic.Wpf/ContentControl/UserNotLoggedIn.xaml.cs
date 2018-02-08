using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KitchenMagic.Wpf.ContentControl
{
	/// <summary>
	/// Interaction logic for UserNotLoggedIn.xaml
	/// </summary>
	public partial class UserNotLoggedIn : UserControl
	{
		public UserNotLoggedIn()
		{
			InitializeComponent();
		}

		public ICommand GoogleButtonCommand
		{
			get => (ICommand)GetValue(GoogleButtonCommandProperty);
			set => SetValue(GoogleButtonCommandProperty, value);
		}

		public static readonly DependencyProperty GoogleButtonCommandProperty =
			DependencyProperty.Register(nameof(GoogleButtonCommand), typeof(ICommand), typeof(UserNotLoggedIn));
	}
}
