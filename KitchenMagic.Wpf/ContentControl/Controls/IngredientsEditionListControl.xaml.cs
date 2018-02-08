using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KitchenMagic.Common.DTO;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KitchenMagic.Wpf.ContentControl.Controls
{
	/// <summary>
	/// Interaction logic for IngredientsListControl.xaml
	/// </summary>
	public partial class IngredientsEditionListControl : UserControl
	{
		public IngredientsEditionListControl()
		{
			InitializeComponent();
			title.SizeChanged += TitleOnSizeChanged;
			Units = Mvx.Resolve<IRecipeEditionService>().GetUnits();

			AddElementCommand = new MvxCommand<IngredientPO>(AddElementCommandAction);
			RemoveElementCommand = new MvxCommand<IngredientPO>(RemoveElementCommandAction);
		}

		private void RemoveElementCommandAction(IngredientPO element)
		{
			IngredientsList.Remove(element);
		}

		private void AddElementCommandAction(IngredientPO element)
		{
			IngredientsList.Insert(IngredientsList.IndexOf(element) + 1, new IngredientPO());
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
			DependencyProperty.Register(
				nameof(IngredientsList),
				typeof(MvxObservableCollection<IngredientPO>),
				typeof(IngredientsEditionListControl),
				new PropertyMetadata(new MvxObservableCollection<IngredientPO>()));

		public double ViewHeight
		{
			get => (double)GetValue(ViewHeightProperty);
			set => SetValue(ViewHeightProperty, value);
		}

		public static readonly DependencyProperty ViewHeightProperty =
			DependencyProperty.Register(nameof(ViewHeight), typeof(double), typeof(IngredientsEditionListControl));

		public List<Unit> Units
		{
			get => (List<Unit>)GetValue(UnitsProperty);
			set => SetValue(UnitsProperty, value);
		}

		public static readonly DependencyProperty UnitsProperty =
			DependencyProperty.Register(nameof(Units), typeof(List<Unit>), typeof(IngredientsEditionListControl));

		private MvxCommand<IngredientPO> AddElementCommand
		{
			get => (MvxCommand<IngredientPO>)GetValue(AddElementCommandProperty);
			set => SetValue(AddElementCommandProperty, value);
		}

		private static readonly DependencyProperty AddElementCommandProperty =
			DependencyProperty.Register(nameof(AddElementCommand), typeof(MvxCommand<IngredientPO>), typeof(IngredientsEditionListControl));

		private MvxCommand<IngredientPO> RemoveElementCommand
		{
			get => (MvxCommand<IngredientPO>)GetValue(RemoveElementCommandProperty);
			set => SetValue(RemoveElementCommandProperty, value);
		}

		private static readonly DependencyProperty RemoveElementCommandProperty =
			DependencyProperty.Register(nameof(RemoveElementCommand), typeof(MvxCommand<IngredientPO>), typeof(IngredientsEditionListControl));
	}
}
