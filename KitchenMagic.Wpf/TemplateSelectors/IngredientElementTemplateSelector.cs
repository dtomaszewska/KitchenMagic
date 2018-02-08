using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using KitchenMagic.Common.PO;

namespace KitchenMagic.Wpf.TemplateSelectors
{
	public class IngredientElementTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var elemnt = container as FrameworkElement;

			if (item is IngredientPO ingredient)
			{
				PropertyChangedEventHandler lambda = null;
				lambda = (o, args) =>
				{
					if (args.PropertyName == "IsTitle")
					{
						ingredient.PropertyChanged -= lambda;
						var cp = (ContentPresenter)container;
						cp.ContentTemplateSelector = null;
						cp.ContentTemplateSelector = this;
					}
				};
				ingredient.PropertyChanged += lambda;

				if (ingredient.IsTitle)
					return elemnt.FindResource("titleElementTemplate") as DataTemplate;
				else
					return elemnt.FindResource("ingredientElementTemplate") as DataTemplate;
			}

			return null;
		}
	}
}
