using MvvmCross.Core.ViewModels;
using System;

namespace KitchenMagic.Common.PO
{
	public interface ITreeListElement
	{
		Guid Id { get; set; }
		string Name { get; set; }
		MvxObservableCollection<ITreeListElement> ChildList { get; set; }
	}
}
