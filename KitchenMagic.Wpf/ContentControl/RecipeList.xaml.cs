using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KitchenMagic.Wpf.ContentControl
{
	/// <summary>
	/// Interaction logic for UserNotLoggedIn.xaml
	/// </summary>
	public partial class RecipeList : UserControl
	{
		public RecipeList()
		{
			InitializeComponent();
		}

		public ICommand ElementClickCommand
		{
			get => (ICommand)GetValue(ElementClickCommandProperty);
			set => SetValue(ElementClickCommandProperty, value);
		}

		public static readonly DependencyProperty ElementClickCommandProperty =
			DependencyProperty.Register("ElementClickCommand", typeof(ICommand), typeof(RecipeList));
	}
}
