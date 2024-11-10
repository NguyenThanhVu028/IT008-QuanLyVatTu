using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMQuanLyVatTu.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            LoginWindow lgw = new LoginWindow();
            lgw.ShowDialog();
        }
    }
}
