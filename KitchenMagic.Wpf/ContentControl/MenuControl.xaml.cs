using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KitchenMagic.Wpf.ContentControl
{
	/// <summary>
	/// Interaction logic for UserNotLoggedIn.xaml
	/// </summary>
	public partial class MenuControl : UserControl
	{
		public MenuControl()
		{
			InitializeComponent();
		}

		public ICommand IsUserLoggedIn
		{
			get => (ICommand)GetValue(IsUserLoggedInProperty);
			set => SetValue(IsUserLoggedInProperty, value);
		}

		public static readonly DependencyProperty IsUserLoggedInProperty =
			DependencyProperty.Register(nameof(IsUserLoggedIn), typeof(ICommand), typeof(MenuControl));

		public ICommand AddRecipeCommand
		{
			get => (ICommand)GetValue(AddRecipeCommandProperty);
			set => SetValue(AddRecipeCommandProperty, value);
		}

		public static readonly DependencyProperty AddRecipeCommandProperty =
			DependencyProperty.Register(nameof(AddRecipeCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand EditRecipeCommand
		{
			get => (ICommand)GetValue(EditRecipeCommandProperty);
			set => SetValue(EditRecipeCommandProperty, value);
		}

		public static readonly DependencyProperty EditRecipeCommandProperty =
			DependencyProperty.Register(nameof(EditRecipeCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand RemoveRecipeCommand
		{
			get => (ICommand)GetValue(RemoveRecipeCommandProperty);
			set => SetValue(RemoveRecipeCommandProperty, value);
		}

		public static readonly DependencyProperty RemoveRecipeCommandProperty =
			DependencyProperty.Register(nameof(RemoveRecipeCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand PrintRecipeCommand
		{
			get => (ICommand)GetValue(PrintRecipeCommandProperty);
			set => SetValue(PrintRecipeCommandProperty, value);
		}

		public static readonly DependencyProperty PrintRecipeCommandProperty =
			DependencyProperty.Register(nameof(PrintRecipeCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand SendRecipeCommand
		{
			get => (ICommand)GetValue(SendRecipeCommandProperty);
			set => SetValue(SendRecipeCommandProperty, value);
		}

		public static readonly DependencyProperty SendRecipeCommandProperty =
			DependencyProperty.Register(nameof(SendRecipeCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand CreateShoppingListCommand
		{
			get => (ICommand)GetValue(CreateShoppingListCommandProperty);
			set => SetValue(CreateShoppingListCommandProperty, value);
		}

		public static readonly DependencyProperty CreateShoppingListCommandProperty =
			DependencyProperty.Register(nameof(CreateShoppingListCommand), typeof(ICommand), typeof(MenuControl));

		public ICommand ChangeLoginStateCommand
		{
			get => (ICommand)GetValue(ChangeLoginStateCommandProperty);
			set => SetValue(ChangeLoginStateCommandProperty, value);
		}

		public static readonly DependencyProperty ChangeLoginStateCommandProperty =
			DependencyProperty.Register(nameof(ChangeLoginStateCommand), typeof(ICommand), typeof(MenuControl));
	}
}
