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
        public MainViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWin);
            MaximizeWindowCommand = new RelayCommand<Window>(MaximizeWin);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWin);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);

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

            //ThongTinVatTuWindow newWin = new ThongTinVatTuWindow();
            //newWin.ShowDialog();

            //ThongTinNhanVienWindow newWin = new ThongTinNhanVienWindow();
            //newWin.ShowDialog();

            //ThongTinCaNhanWindow newWin = new ThongTinCaNhanWindow();
            //newWin.ShowDialog();

            //XoaGanDayWindow newWin = new XoaGanDayWindow();
            //newWin.ShowDialog();

            //ThongTinKhoWindow newWin = new ThongTinKhoWindow();
            //newWin.ShowDialog();

            //ThongTinKhachHangWindow newWin = new ThongTinKhachHangWindow();
            //newWin.ShowDialog();

            //ThongTinNhaCungCapWindow newWin = new ThongTinNhaCungCapWindow();
            //newWin.ShowDialog();

            //ThongTinYeuCauMuaHangWindow newWin = new ThongTinYeuCauMuaHangWindow();
            //newWin.ShowDialog();

            //ThongTinYeuCauNhapHangWindow newWin = new ThongTinYeuCauNhapHangWindow();
            //newWin.ShowDialog();

            //ThemSuaVatTuWindow newWin = new ThemSuaVatTuWindow();
            //newWin.ShowDialog();

            //ChiTietPhieuXuatWindow newWin = new ChiTietPhieuXuatWindow();
            //newWin.ShowDialog();

            ChiTietPhieuNhapWindow newWin = new ChiTietPhieuNhapWindow();
            newWin.ShowDialog();
        }

        #region Commands
        public ICommand CloseWindowCommand { get; set; }
        void CloseWin(Window window)
        {
            window.Close();
        }
        public ICommand MaximizeWindowCommand { get; set; }
        void MaximizeWin(Window window)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
        public ICommand MinimizeWindowCommand { get; set; }
        void MinimizeWin(Window window)
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
