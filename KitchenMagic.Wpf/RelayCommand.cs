using System;
using System.Windows.Input;

namespace KitchenMagic.Wpf.Extensions
{
	public class RelayCommand : ICommand
	{
		private readonly Func<bool> _canExecute;
		private readonly Action _execute;

		public RelayCommand(Action execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke() ?? true;
		}

		public void Execute(object parameter)
		{
			_execute();
		}
	}
}
