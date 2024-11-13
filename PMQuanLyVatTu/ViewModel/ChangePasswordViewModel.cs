using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
        public ChangePasswordViewModel()
        {
            CancelCommand = new RelayCommand<Window>(CloseWindow);
        }
        void CloseWindow(Window uc)
        {
            MessageBox.Show("Command Executed!");
            uc.Close();
        }
    }
}
