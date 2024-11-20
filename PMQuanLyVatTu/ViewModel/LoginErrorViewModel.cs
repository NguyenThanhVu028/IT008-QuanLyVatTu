using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class LoginErrorViewModel : BaseViewModel
    {
        public LoginErrorViewModel()
        {
            TypeAgainCommand = new RelayCommand<Window>(TypeAgain);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        public ICommand TypeAgainCommand {  get; set; }
        void TypeAgain(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window) {
            window.DragMove();
        }
    }
}
