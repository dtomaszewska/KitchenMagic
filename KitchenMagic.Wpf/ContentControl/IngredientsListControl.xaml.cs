using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KitchenMagic.Common.PO;

namespace KitchenMagic.Wpf.ContentControl
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

		public List<IngredientPO> IngredientsList
		{
			get => (List<IngredientPO>)GetValue(IngredientsListProperty);
			set => SetValue(IngredientsListProperty, value);
		}

		public static readonly DependencyProperty IngredientsListProperty =
			DependencyProperty.Register("IngredientsList", typeof(List<IngredientPO>), typeof(IngredientsListControl));

		public double ViewHeight
		{
			get => (double)GetValue(ViewHeightProperty);
			set => SetValue(ViewHeightProperty, value);
		}

		public static readonly DependencyProperty ViewHeightProperty =
			DependencyProperty.Register("ViewHeight", typeof(double), typeof(IngredientsListControl));
	}
}
