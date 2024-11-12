using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMQuanLyVatTu.ViewModel
{
    class LoginWindowViewModel:BaseViewModel
    {
        private string _userName = "";
        private string _passWord = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _passWord; }
            set{ _passWord = value; OnPropertyChanged(); }
        }
        public LoginWindowViewModel()
        {
            MessageBox.Show("An instance of LoginViewModel has been created!");
        }
    }
}
