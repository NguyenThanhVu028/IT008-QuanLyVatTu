using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThemSuaVatTuWindowViewModel : BaseViewModel
    {
        public ThemSuaVatTuWindowViewModel()
        {
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            CancelCommand = new RelayCommand<Window>(Cancel);
            ConfirmCommand = new RelayCommand<object>(Confirm);
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window window)
        {
            window.Close();
        }
        public ICommand ConfirmCommand { get; set; }
        void Confirm(object t) {
            MessageBox.Show("ConfirmCommand Executed");
        }
    }
}
