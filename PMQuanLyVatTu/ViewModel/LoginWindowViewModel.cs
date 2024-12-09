using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
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
        #region ReturnValue
        private bool _returnValue;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; OnPropertyChanged(); }
        }
        #endregion
        #region UserName Password
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
        #endregion
        #region Command
        public ICommand LoginButtonCommand {  get; set; }
        void LoginButton(Window window)
        {
            bool Check = false;
            
            if (UserName == "") //Nếu chưa nhập tên đăng nhập, k check password vì có thể cho tk k có mật khẩu
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập tên đăng nhập.");
                msg.ShowDialog();
                return;
            }
            var ListFromDB = DataProvider.Instance.DB.Accounts.ToList();
            foreach (var account in ListFromDB)
            {
                if(account.TenDn == UserName)
                {
                    if(account.MatKhau == Password)
                    {
                        Check = true;
                        var ListEmployee = DataProvider.Instance.DB.Employees.ToList();
                        foreach (var emp in ListEmployee)
                        {
                            if(emp.MaNv == account.MaNv)
                            {
                                CurrentUser.Instance.MaNv = emp.MaNv;
                                CurrentUser.Instance.HoTen = emp.HoTen;
                                CurrentUser.Instance.ChucVu = emp.ChucVu;
                                CurrentUser.Instance.ImageLocation = emp.ImageLocation;
                            }
                        }
                        break;
                    }
                }
            }
            if (Check) //Check tài khoản hợp lệ
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
            ForgetPasswordWindowViewModel viewModel = new ForgetPasswordWindowViewModel();
            forgetPasswordWindow.DataContext = viewModel;
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
        #endregion
    }
}
