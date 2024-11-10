using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PMQuanLyVatTu.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _userName = "";
        private string _passWord = "";
        public string UserName {
            get { return _userName; }
            set { _userName = value; }
        }
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }
        public LoginViewModel()
        {

        }
    }
}
