using System.Windows;
using KitchenMagic.Wpf.ViewModels;

namespace KitchenMagic.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += (sender, args) => { ((MainWindowViewModel)base.DataContext).Start(); };
		}
	}
}
