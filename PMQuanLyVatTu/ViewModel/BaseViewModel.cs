using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region PropertyChangedHandler
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        #region RelayCommand
        public class RelayCommand<T> : ICommand
        {
            private readonly Predicate<T> _canExecute;
            private readonly Action<T> _execute;

            public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
            {
                _canExecute = canExecute;
                _execute = execute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                try
                {
                    return _canExecute == null ? true : _canExecute((T)parameter);
                }
                catch
                {
                    return true;
                }
            }

            public void Execute(object? parameter)
            {
                _execute((T)parameter);
            }
        }
        #endregion
    }
}

