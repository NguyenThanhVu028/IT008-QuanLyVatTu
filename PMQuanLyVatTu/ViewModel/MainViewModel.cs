using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        #region Commands
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        #endregion
        public MainViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWin);
            MaximizeWindowCommand = new RelayCommand<Window>(MaximizeWin);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWin);
            //LoginWindow lgw = new LoginWindow();
            //lgw.ShowDialog();

            //ForgetPasswordWindow fpw = new ForgetPasswordWindow();
            //fpw.ShowDialog();

            //LoginError lge = new LoginError();
            //lge.ShowDialog();

            //ChangePasswordWindows cpw = new ChangePasswordWindows();
            //cpw.ShowDialog();

            //AlreadyExistsError ae = new AlreadyExistsError();
            //ae.ShowDialog();

            //CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "Nhập sai dữ liệu", "Vui lòng nhập lại dữ liệu đúng với định dạng yêu cầu!");
            //msg.ShowDialog();
        }
        void CloseWin(Window window)
        {
            window.Close();
        }
        void MaximizeWin(Window window)
        {
            if(window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
        void MinimizeWin(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }
}
