using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using KitchenMagic.Common.PO;
using KitchenMagic.Common.Services;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Wpf.ContentControl.Controls
{
	/// <summary>
	/// Interaction logic for IngredientsListControl.xaml
	/// </summary>
	public partial class MultiselectControl : UserControl
	{
		private readonly MultiselectControlService _service;

		public MultiselectControl()
		{
			InitializeComponent();

			_service = new MultiselectControlService();

			FilteredCategoriesList = new MvxObservableCollection<MultiSelectObject>();

			textBox.PreviewKeyDown += TextBoxKeysPressed;
			textBox.TextChanged += TextBoxTextChanged;
			textBox.LostFocus += TextBoxLostFocus;

			mainBorder.SizeChanged += UpdateControlHeight;
			autocomplete.SizeChanged += UpdateControlHeight;

			CategoryClickedCommand = new MvxCommand<MultiSelectObject>(CategoryClickedCommandAction);
			CategoryRemovedCommand = new MvxCommand<CategoryPO>(CategoryRemovedCommandAction);
		}

		private void UpdateControlHeight(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			UpdateControlHeight();
		}

		private void UpdateControlHeight()
		{
			root.Height = mainBorder.ActualHeight;
			if (autocomplete.Visibility == Visibility.Visible)
				root.Height += autocomplete.ActualHeight;
		}

		private void CategoryClickedCommandAction(MultiSelectObject obj)
		{
			if (!obj.IsActive)
				return;

			textBox.Text = string.Empty;
			_service.AddSelectedCategory(obj);
			UpdateSelectedCategoriesList();
			HideAutocomplete();
		}

		private void CategoryRemovedCommandAction(CategoryPO obj)
		{
			_service.RemoveSelectedCategory(obj);
			UpdateSelectedCategoriesList();
		}

		private void TextBoxLostFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			HideAutocomplete();
		}

		private void TextBoxTextChanged(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!string.IsNullOrWhiteSpace(textBox.Text))
				ShowAutocomplete();
			else
				HideAutocomplete();
		}

		private void TextBoxKeysPressed(object sender, RoutedEventArgs routedEventArgs)
		{
			var args = routedEventArgs as KeyEventArgs;
			if (args?.Key == Key.Down && autocomplete.Visibility == Visibility.Collapsed)
				ShowAutocomplete();
			else if (args?.Key == Key.Down && autocomplete.Visibility == Visibility.Visible)
				autocomplete.SelectedIndex++;
			else if (args?.Key == Key.Up && autocomplete.Visibility == Visibility.Visible && autocomplete.SelectedIndex >= 0)
				autocomplete.SelectedIndex--;
			else if (args?.Key == Key.Enter && autocomplete.Visibility == Visibility.Visible && autocomplete.SelectedIndex >= 0)
				CategoryClickedCommandAction(autocomplete.Items.GetItemAt(autocomplete.SelectedIndex) as MultiSelectObject);
		}

		private void HideAutocomplete()
		{
			autocomplete.Visibility = Visibility.Collapsed;
			UpdateControlHeight();
		}

		private void ShowAutocomplete()
		{
			UpdateAutocompletePosition();
			UpdateAutocompleteSugestions();
			autocomplete.Visibility = Visibility.Visible;
			UpdateControlHeight();
		}

		private void UpdateAutocompletePosition()
		{
			AutocompleteLeft = selectedCategories.ActualWidth;
			AutocompleteTop = mainBorder.ActualHeight;
		}

		private void UpdateAutocompleteSugestions()
		{
			FilteredCategoriesList.Clear();
			foreach (var category in _service.GetAutocompleteSugestions(textBox.Text))
				FilteredCategoriesList.Add(category);
		}

		private void UpdateSelectedCategoriesList()
		{
			// We do not need it as long as MultiselectControlService has original ObservableCollection

			//SelectedCategoriesList.Clear();
			//foreach (var category in _service.GetSelectedCategories())
			//{
			//	SelectedCategoriesList.Add(category);
			//}
		}

		public List<CategoryPO> AllCategoriesList
		{
			get => (List<CategoryPO>)GetValue(AllCategoriesListProperty);
			set => SetValue(AllCategoriesListProperty, value);
		}

		public static readonly DependencyProperty AllCategoriesListProperty =
			DependencyProperty.Register(
				nameof(AllCategoriesList),
				typeof(List<CategoryPO>),
				typeof(MultiselectControl),
				new PropertyMetadata(
					new List<CategoryPO>(),
					(o, args) => { (o as MultiselectControl)?._service.ChangeAllCategoriesList(args.NewValue as List<CategoryPO>); }));

		public MvxObservableCollection<CategoryPO> SelectedCategoriesList
		{
			get => (MvxObservableCollection<CategoryPO>)GetValue(SelectedCategoriesListProperty);
			set => SetValue(SelectedCategoriesListProperty, value);
		}

		public static readonly DependencyProperty SelectedCategoriesListProperty =
			DependencyProperty.Register(
				nameof(SelectedCategoriesList),
				typeof(MvxObservableCollection<CategoryPO>),
				typeof(MultiselectControl),
				new PropertyMetadata(
					new MvxObservableCollection<CategoryPO>(),
					(o, args) =>
					{
						var control = o as MultiselectControl;
						if (control == null)
							return;

						control._service.ChangeSelectedCategoriesList(args.NewValue as MvxObservableCollection<CategoryPO>);
					}));

		public Brush ForegroundColor
		{
			get => (Brush)GetValue(ForegroundColorProperty);
			set => SetValue(ForegroundColorProperty, value);
		}

		public static readonly DependencyProperty ForegroundColorProperty =
			DependencyProperty.Register(nameof(ForegroundColor), typeof(Brush), typeof(MultiselectControl));

		private MvxObservableCollection<MultiSelectObject> FilteredCategoriesList
		{
			get => (MvxObservableCollection<MultiSelectObject>)GetValue(FilteredCategoriesListProperty);
			set => SetValue(FilteredCategoriesListProperty, value);
		}

		private static readonly DependencyProperty FilteredCategoriesListProperty =
			DependencyProperty.Register(nameof(FilteredCategoriesList), typeof(MvxObservableCollection<MultiSelectObject>), typeof(MultiselectControl));

		private MvxCommand<MultiSelectObject> CategoryClickedCommand
		{
			get => (MvxCommand<MultiSelectObject>)GetValue(CategoryClickedCommandProperty);
			set => SetValue(CategoryClickedCommandProperty, value);
		}

		private static readonly DependencyProperty CategoryClickedCommandProperty =
			DependencyProperty.Register(nameof(CategoryClickedCommand), typeof(MvxCommand<MultiSelectObject>), typeof(MultiselectControl));

		private MvxCommand<CategoryPO> CategoryRemovedCommand
		{
			get => (MvxCommand<CategoryPO>)GetValue(CategoryRemovedCommandProperty);
			set => SetValue(CategoryRemovedCommandProperty, value);
		}

		private static readonly DependencyProperty CategoryRemovedCommandProperty =
			DependencyProperty.Register(nameof(CategoryRemovedCommand), typeof(MvxCommand<CategoryPO>), typeof(MultiselectControl));

		private double AutocompleteLeft
		{
			get => (double)GetValue(AutocompleteLeftProperty);
			set => SetValue(AutocompleteLeftProperty, value);
		}

		private static readonly DependencyProperty AutocompleteLeftProperty =
			DependencyProperty.Register(nameof(AutocompleteLeft), typeof(double), typeof(MultiselectControl));

		private double AutocompleteTop
		{
			get => (double)GetValue(AutocompleteTopProperty);
			set => SetValue(AutocompleteTopProperty, value);
		}

		private static readonly DependencyProperty AutocompleteTopProperty =
			DependencyProperty.Register(nameof(AutocompleteTop), typeof(double), typeof(MultiselectControl));
	}
}
