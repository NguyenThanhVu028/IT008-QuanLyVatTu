using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class LoginWindowViewModel:BaseViewModel
    {
        public LoginWindowViewModel()
        {
            LoginButtonCommand = new RelayCommand<Window>(LoginButton);
            ForgetPasswordCommand = new RelayCommand<object>(ForgetPassword);
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        private bool _returnValue;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; OnPropertyChanged(); }
        }
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
        public ICommand LoginButtonCommand {  get; set; }
        void LoginButton(Window window)
        {
            if (true) //Check tài khoản hợp lệ
            {
                ReturnValue = true;
                window.Close();
            }
            else
            {
                LoginError lgError = new LoginError();
                lgError.ShowDialog();
            }
        }
        public ICommand ForgetPasswordCommand { get; set; }
        void ForgetPassword(object t)
        {
            ForgetPasswordWindow forgetPasswordWindow = new ForgetPasswordWindow();
            forgetPasswordWindow.ShowDialog();
        }
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
        {
            ReturnValue = false;
            window.Close();
        }
        public ICommand MinimizeWindowCommand {  get; set; }
        void MinimizeWindow(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
    }
}
