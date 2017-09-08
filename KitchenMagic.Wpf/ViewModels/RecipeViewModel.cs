using System;
using MvvmCross.Core.ViewModels;

namespace KitchenMagic.Wpf.ViewModels
{
	internal class RecipeViewModel : MvxViewModel
	{
		private Guid id;

		public RecipeViewModel(Guid id)
		{
			this.id = id;
		}
	}
}