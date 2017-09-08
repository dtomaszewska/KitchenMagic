using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KitchenMagic.Common.PO
{
	public class BasePO : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
