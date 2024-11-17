using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class XoaGanDayWindowViewModel:BaseViewModel
    {
        public XoaGanDayWindowViewModel()
        {
            CloseCommand = new RelayCommand<Window>(Close);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        public ICommand CloseCommand { get; set; }
        void Close(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
    }
}
