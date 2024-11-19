using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.View;
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
            ThongTinYeuCauMuaHangWindow newWin = new ThongTinYeuCauMuaHangWindow();
            newWin.ShowDialog();

            CloseWindowCommand = new RelayCommand<Window>(CloseWin);
            MaximizeWindowCommand = new RelayCommand<Window>(MaximizeWin);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWin);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);

            TrangChuCommand = new RelayCommand<object>(TrangChu);
            VatTuCommand = new RelayCommand<object>(VatTu);
            NhanVienCommand = new RelayCommand<object>(NhanVien);
            KhachHangCommand = new RelayCommand<object>(KhachHang);
            NhaCungCapCommand = new RelayCommand<object>(NhaCungCap);
            KhoCommand = new RelayCommand<object>(Kho);

            TrangChuVM = new TrangChuViewModel();
            VatTuVM = new VatTuViewModel();
            NhanVienVM = new NhanVienViewModel();
            KhachHangVM = new KhachHangViewModel();
            NhaCungCapVM = new NhaCungCapViewModel();
            KhoVM = new KhoViewModel();


            _currentPage = new TrangChu(); (_currentPage as TrangChu).DataContext = TrangChuVM;

        }
        private object _currentPage;
        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value; OnPropertyChanged();
            }
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

        public ICommand TrangChuCommand {  get; set; }
        void TrangChu(object t) { if((CurrentPage as TrangChu) == null) {CurrentPage = new TrangChu(); (CurrentPage as TrangChu).DataContext = TrangChuVM; } }
        public ICommand VatTuCommand {  get; set; }
        void VatTu(object t) { if ((CurrentPage as VatTu) == null) {CurrentPage = new VatTu(); (CurrentPage as VatTu).DataContext = VatTuVM; } }
        public ICommand NhanVienCommand { get; set; }
        void NhanVien(object t) { if((CurrentPage as NhanVien) == null) {CurrentPage = new NhanVien(); (CurrentPage as NhanVien).DataContext = NhanVienVM; } }
        public ICommand KhachHangCommand { get; set; }
        void KhachHang(object t) { if((CurrentPage as KhachHang) == null) {CurrentPage = new KhachHang(); (CurrentPage as KhachHang).DataContext = KhachHangVM; } }
        public ICommand NhaCungCapCommand {  get; set; }
        void NhaCungCap(object t) { if((CurrentPage as NhaCungCap) == null) {CurrentPage = new NhaCungCap(); (CurrentPage as NhaCungCap).DataContext = NhaCungCapVM; } }
        public ICommand KhoCommand {  get; set; }
        void Kho(object t) { if((CurrentPage as Kho) == null) {CurrentPage = new Kho(); (CurrentPage as Kho).DataContext = KhoVM; } }

        TrangChuViewModel TrangChuVM;
        VatTuViewModel VatTuVM;
        NhanVienViewModel NhanVienVM;
        KhachHangViewModel KhachHangVM;
        NhaCungCapViewModel NhaCungCapVM;
        KhoViewModel KhoVM;
        #endregion
    }
}
