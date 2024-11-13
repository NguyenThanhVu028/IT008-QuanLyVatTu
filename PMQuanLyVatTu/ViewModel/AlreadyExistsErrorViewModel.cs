using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class AlreadyExistsErrorViewModel : BaseViewModel
    {
        public AlreadyExistsErrorViewModel() {
            CloseCommand = new RelayCommand<Window>(CloseWin);
        }
        public ICommand CloseCommand { get; set; }
        public void CloseWin(Window window)
        {
            window.Close();
        }
    }
}
