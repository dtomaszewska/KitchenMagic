using System.Windows;
using System.Windows.Controls;
using KitchenMagic.Common.PO;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Wpf.ContentControl.Controls
{
	/// <summary>
	/// Interaction logic for IngredientsListControl.xaml
	/// </summary>
	public partial class IngredientsListControl : UserControl
	{
		public IngredientsListControl()
		{
			InitializeComponent();
			title.SizeChanged += TitleOnSizeChanged;
		}

		private void TitleOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			list.Height = root.ViewHeight - sizeChangedEventArgs.NewSize.Height;
		}

		public MvxObservableCollection<IngredientPO> IngredientsList
		{
			get => (MvxObservableCollection<IngredientPO>)GetValue(IngredientsListProperty);
			set => SetValue(IngredientsListProperty, value);
		}

		public static readonly DependencyProperty IngredientsListProperty =
			DependencyProperty.Register(nameof(IngredientsList), typeof(MvxObservableCollection<IngredientPO>), typeof(IngredientsListControl));

		public double ViewHeight
		{
			get => (double)GetValue(ViewHeightProperty);
			set => SetValue(ViewHeightProperty, value);
		}

		public static readonly DependencyProperty ViewHeightProperty =
			DependencyProperty.Register(nameof(ViewHeight), typeof(double), typeof(IngredientsListControl));
	}
}
