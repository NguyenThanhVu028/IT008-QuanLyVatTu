﻿using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
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
            #region WindowButtons Command
            LoadWindowCommand = new RelayCommand<Window>(LoadWindow);
            CloseWindowCommand = new RelayCommand<Window>(CloseWin);
            MaximizeWindowCommand = new RelayCommand<Window>(MaximizeWin);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWin);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            AccountInfoCommand = new RelayCommand<object>(AccountInfo);
            LogOutCommand = new RelayCommand<Window>(LogOut);
            #endregion
            #region PageCommand
            TrangChuCommand = new RelayCommand<object>(TrangChu);
            VatTuCommand = new RelayCommand<object>(VatTu);
            NhanVienCommand = new RelayCommand<object>(NhanVien);
            KhachHangCommand = new RelayCommand<object>(KhachHang);
            NhaCungCapCommand = new RelayCommand<object>(NhaCungCap);
            KhoCommand = new RelayCommand<object>(Kho);
            DuyetCommand = new RelayCommand<object>(Duyet);
            ThongKeCommand = new RelayCommand<object>(ThongKe);
            YeuCauXuatHangCommand = new RelayCommand<object>(YeuCauXuatHang);
            YeuCauNhapHangCommand = new RelayCommand<object>(YeuCauNhapHang);
            PhieuNhapCommand = new RelayCommand<object>(PhieuNhap);
            PhieuXuatCommand = new RelayCommand<object> (PhieuXuat);
            #endregion
            #region Page ViewModel
            try
            {
                LoginVM = new LoginWindowViewModel();
                TrangChuVM = new TrangChuViewModel();
                VatTuVM = new VatTuViewModel();
                NhanVienVM = new NhanVienViewModel();
                KhachHangVM = new KhachHangViewModel();
                NhaCungCapVM = new NhaCungCapViewModel();
                KhoVM = new KhoViewModel();
                DuyetVM = new DuyetViewModel();
                ThongKeVM = new ThongKeViewModel();
                YeuCauXuatHangVM = new YeuCauXuatHangViewModel();
                YeuCauNhapHangVM = new YeuCauNhapHangViewModel();
                PhieuNhapVM = new PhieuNhapViewModel();
                PhieuXuatVM = new PhieuXuatViewModel();
            }
            catch
            {
                ErrorMessage.CustomMessage msg = new ErrorMessage.CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Đã xảy ra lỗi khi kết nối với cơ sở dữ liệu!", false);
                msg.ShowDialog();
                Application.Current.Shutdown();
            }
            #endregion
            //Khởi tạo các trang/window ban đầu
            _currentPage = new TrangChu(); (_currentPage as TrangChu).DataContext = TrangChuVM;
            LoginWindow newLogin = new LoginWindow(); newLogin.DataContext = LoginVM; newLogin.ShowDialog();

        }
        #region CurrentPage
        private object _currentPage;
        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value; OnPropertyChanged();
            }
        }
        #endregion
        #region Command
        public ICommand LoadWindowCommand {  get; set; }
        void LoadWindow(Window window)
        {
            if (LoginVM.ReturnValue == false) window.Close();
            else {
                window.Hide();
                CustomMessage popUpMessage = new CustomMessage("/Material/Images/Icons/success.png", "ĐĂNG NHẬP THÀNH CÔNG", "Đã đăng nhập thành công với tên: " + LoginVM.UserName);
                popUpMessage.ShowDialog();
                if (popUpMessage.ReturnValue == false) window.Close();
                window.Show();
            };
        }
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
        public ICommand AccountInfoCommand { get; set; }
        void AccountInfo(object t)
        {
            ThongTinCaNhanWindow AccountWin = new ThongTinCaNhanWindow();
            ThongTinCaNhanWindowViewModel TTCNVM = new ThongTinCaNhanWindowViewModel();
            AccountWin.DataContext = TTCNVM;
            AccountWin.ShowDialog();

        }
        public ICommand LogOutCommand { get; set; }
        void LogOut(Window t)
        {
            t.Hide();
            LoginWindow newLogin = new LoginWindow(); newLogin.DataContext = LoginVM; newLogin.ShowDialog();
            if (LoginVM.ReturnValue == true)
            {
                CustomMessage popUpMessage = new CustomMessage("/Material/Images/Icons/success.png", "ĐĂNG NHẬP THÀNH CÔNG", "Đã đăng nhập thành công với tên: " + LoginVM.UserName);
                popUpMessage.ShowDialog();
                if (popUpMessage.ReturnValue == false) t.Close();
                else
                {
                    MaNV = CurrentUser.Instance.MaNv;
                    ChucVu = CurrentUser.Instance.ChucVu;
                    HoTen = CurrentUser.Instance.HoTen;
                    Avatar = CurrentUser.Instance.ImageLocation;

                    CurrentPage = null;
                    t.Show();
                }
            }
            else t.Close();
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
        public ICommand DuyetCommand { get; set; }
        void Duyet(object t) { if(CurrentPage as Duyet == null) { CurrentPage = new Duyet(); (CurrentPage as Duyet).DataContext = DuyetVM; } }
        public ICommand ThongKeCommand { get; set; }
        void ThongKe(object t) { if(CurrentPage as ThongKe == null) { CurrentPage = new ThongKe(); (CurrentPage as ThongKe).DataContext = ThongKeVM; } }
        public ICommand YeuCauXuatHangCommand { get; set; }
        void YeuCauXuatHang(object t) { if (CurrentPage as YeuCauXuatHang == null) { CurrentPage = new YeuCauXuatHang(); (CurrentPage as YeuCauXuatHang).DataContext = YeuCauXuatHangVM; } }
        public ICommand YeuCauNhapHangCommand { get; set; }
        void YeuCauNhapHang(object t) { if (CurrentPage as YeuCauNhapHang == null) { CurrentPage = new YeuCauNhapHang(); (CurrentPage as YeuCauNhapHang).DataContext = YeuCauNhapHangVM; } }
        public ICommand PhieuNhapCommand { get; set; }
        void PhieuNhap(object t) { if(CurrentPage as PhieuNhap == null) { CurrentPage = new PhieuNhap(); (CurrentPage as PhieuNhap).DataContext = PhieuNhapVM; } }
        public ICommand PhieuXuatCommand { get; set; }
        void PhieuXuat(object t) { if(CurrentPage as PhieuXuat == null) { CurrentPage = new PhieuXuat(); (CurrentPage as PhieuXuat).DataContext = PhieuXuatVM; } }

        LoginWindowViewModel LoginVM;
        TrangChuViewModel TrangChuVM;
        VatTuViewModel VatTuVM;
        NhanVienViewModel NhanVienVM;
        KhachHangViewModel KhachHangVM;
        NhaCungCapViewModel NhaCungCapVM;
        KhoViewModel KhoVM;
        DuyetViewModel DuyetVM;
        ThongKeViewModel ThongKeVM;
        YeuCauXuatHangViewModel YeuCauXuatHangVM;
        YeuCauNhapHangViewModel YeuCauNhapHangVM;
        PhieuNhapViewModel PhieuNhapVM;
        PhieuXuatViewModel PhieuXuatVM;
        #endregion
        #region Info
        private CurrentUser user = CurrentUser.Instance;
        public CurrentUser User
        {
            get { return user; }
        }
        private string _maNV;
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        private string _hoTen;
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; OnPropertyChanged();}
        }
        private string _chucVu;
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
