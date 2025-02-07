﻿using Microsoft.IdentityModel.Tokens;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.PrintWindows;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinYeuCauXuatHangWindowViewModel:BaseViewModel
    {
        public ThongTinYeuCauXuatHangWindowViewModel(string maycx = null) 
        {
            if(maycx != null) { EditMode = true; Title = "CHỈNH SỬA YÊU CẦU XUẤT"; LoadData(maycx); LoadDanhSach(); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM YÊU CẦU XUẤT"; }

            LoadNhanVien();
            LoadKhachHang();
            LoadKho();

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<Window>(SaveInfo);

            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            CreateReceiptCommand = new RelayCommand<object>(CreateRecepit);
        }
        #region Title
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maYCX = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maKH = "";
        public string _maKho = "";
        private string _ngayLap = DateTime.Now.ToString("ddd/dd/MM/yyyy");
        private string _ghiChu = "";
        public string MaYCX
        {
            get { return _maYCX; }
            set { _maYCX = value; OnPropertyChanged(); }
        }
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string MaKH
        {
            get { return _maKH; }
            set { _maKH = value; OnPropertyChanged(); }
        }
        public string MaKho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); DanhSachVatTuYeuCau.Clear(); }
        }
        public string NgayLap
        {
            get { return _ngayLap; }
            set { _ngayLap = value; OnPropertyChanged(); }
        }
        public string GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; OnPropertyChanged(); }
        }
        #endregion
        #region ListEmpty
        private bool _listEmpty = true;
        public bool ListEmpty
        {
            get { return _listEmpty; }
            set { _listEmpty = value; OnPropertyChanged();}
        }
        #endregion
        #region EditMode
        private bool _editMode = false;
        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; OnPropertyChanged(); }
        }
        #endregion
        #region EnableEditing
        private bool _enableEditing = false;
        public bool EnableEditing
        {
            get { return _enableEditing; }
            set { _enableEditing = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _nhanVien = new ObservableCollection<string>();
        public ObservableCollection<string> NhanVien
        {
            get { return _nhanVien; }
            set { _nhanVien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _khachHang = new ObservableCollection<string>();
        public ObservableCollection<string> KhachHang
        {
            get { return _khachHang; }
            set { _khachHang = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _kho = new ObservableCollection<string>();
        public ObservableCollection<string> Kho
        {
            get { return _kho; }
            set { _kho = value; OnPropertyChanged(); }
        }

        #endregion
        #region DanhSachVatTuYeuCau
        private ObservableCollection<VatTu> _danhSachVatTuYeuCau = new ObservableCollection<VatTu>();
        public ObservableCollection<VatTu> DanhSachVatTuYeuCau
        {
            get { return _danhSachVatTuYeuCau; }
            set { _danhSachVatTuYeuCau = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand EditInfoCommand { get; set; }
        void EditInfo(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã vào chế độ chỉnh sửa thông tin.");
            msg.ShowDialog();
            if (msg.ReturnValue == true) EnableEditing = true;
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa yêu cầu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                EnableEditing = false;
                var YCX = DataProvider.Instance.DB.ExportRequests.Find(MaYCX);

                if (YCX != null)
                {
                    YCX.DaXoa = true;
                    YCX.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu xuất để xóa!", false);
                }
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(Window t)
        {
            if (MaYCX.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã yêu cầu xuất.");
                msg.ShowDialog();
                return;
            }
            if (MaYCX.Length > 7)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mã yêu cầu xuất không được vượt quá 7 ký tự.", false);
                msg1.ShowDialog();
                return;
            }
            if (MaNV.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã nhân viên.");
                msg.ShowDialog();
                return;
            }
            if (MaKH.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã khách hàng.");
                msg.ShowDialog();
                return;
            }
            if (MaKho.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã kho.");
                msg.ShowDialog();
                return;
            }
            if (NgayLap.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn ngày lập.");
                msg.ShowDialog();
                return;
            }
            if (EditMode == true) // Nếu đang chế độ chỉnh sửa
            {
                var YCX = DataProvider.Instance.DB.ExportRequests.Find(MaYCX);

                if (YCX != null)
                {
                    var existingRecords = DataProvider.Instance.DB.ExportRequestInfos.Where(c => c.MaYcx == MaYCX).ToList();
                    DataProvider.Instance.DB.ExportRequestInfos.RemoveRange(existingRecords);
                    // Cập nhật thông tin vào ExportRequests
                    YCX.MaKh = MaKH;
                    YCX.MaNv = MaNV;
                    YCX.GhiChu = GhiChu;
                    YCX.KhoXuat = MaKho;
                    try
                    {
                        YCX.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        YCX.NgayLap = DateTime.Now;
                    }
                    // Thêm mới các ExportRequestInfo từ DanhSachVatTuYeuCau
                    foreach (var vatTu in DanhSachVatTuYeuCau)
                    {
                        var newRequestInfo = new ExportRequestInfo
                        {
                            MaYcx = MaYCX,
                            MaVt = vatTu.MaVT,
                            SoLuong = vatTu.SoLuong,
                            ChietKhau = vatTu.ChietKhau,
                            Vat = vatTu.VAT
                        };
                        DataProvider.Instance.DB.ExportRequestInfos.Add(newRequestInfo);
                    }
                    DataProvider.Instance.DB.SaveChanges();
                    EnableEditing = false;
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                    msg.ShowDialog();
                }
                else
                {
                    var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Yêu cầu đã không còn tồn tại trên hệ thống.");
                    msg.ShowDialog();
                    return;
                }
            }
            else // Nếu trong chế độ thêm YCX
            {
                var YCX = DataProvider.Instance.DB.ExportRequests.Find(MaYCX);
                if (YCX != null)
                {
                    if (YCX.DaXoa == true)
                    {
                        // Xóa hẳn các record của MaYCX bên ExportRequestInfo
                        var existingRecords = DataProvider.Instance.DB.ExportRequestInfos.Where(c => c.MaYcx == MaYCX).ToList();
                        DataProvider.Instance.DB.ExportRequestInfos.RemoveRange(existingRecords);

                        YCX.MaKh = MaKH;
                        YCX.MaNv = MaNV;
                        YCX.GhiChu = GhiChu;
                        YCX.KhoXuat = MaKho;
                        YCX.DaXoa = false;
                        YCX.TrangThai = "Chưa tiếp nhận";
                        try
                        {
                            YCX.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        catch
                        {
                            YCX.NgayLap = DateTime.Now;
                        }
                        foreach (var vatTu in DanhSachVatTuYeuCau)
                        {
                            var newRequestInfo = new ExportRequestInfo
                            {
                                MaYcx = MaYCX,
                                MaVt = vatTu.MaVT,
                                SoLuong = vatTu.SoLuong,
                                ChietKhau = vatTu.ChietKhau,
                                Vat = vatTu.VAT
                            };
                            DataProvider.Instance.DB.ExportRequestInfos.Add(newRequestInfo);
                        }

                        DataProvider.Instance.DB.SaveChanges();
                    }
                    else // Trùng mã YCX
                    {
                        AlreadyExistsError msg1 = new AlreadyExistsError();
                        msg1.ShowDialog();
                        return;
                    }
                }
                else
                {
                    ExportRequest newYCX = new ExportRequest
                    {
                        MaYcx = MaYCX,
                        MaNv = MaNV,
                        MaKh = MaKH,
                        KhoXuat = MaKho,
                        GhiChu = GhiChu,
                        TrangThai = "Chưa tiếp nhận",
                        DaXoa = false,
                    };
                    try
                    {
                        newYCX.NgayLap = DateTime.ParseExact(NgayLap, "dd/ddd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        newYCX.NgayLap = DateTime.Now;
                    }
                    DataProvider.Instance.DB.ExportRequests.Add(newYCX);

                    foreach (var vatTu in DanhSachVatTuYeuCau)
                    {
                        var newRequestInfo = new ExportRequestInfo
                        {
                            MaYcx = MaYCX,
                            MaVt = vatTu.MaVT,
                            SoLuong = vatTu.SoLuong,
                            ChietKhau = vatTu.ChietKhau,
                            Vat = vatTu.VAT
                        };
                        DataProvider.Instance.DB.ExportRequestInfos.Add(newRequestInfo);
                    }
                    DataProvider.Instance.DB.SaveChanges();
                }
                CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm yêu cầu xuất thành công.");
                msgSuccess.ShowDialog();
                t.Close();
            }
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            if (MaKho == null || MaKho == "")
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn kho để yêu cầu xuất hàng.", false);
                msg.ShowDialog();
            }
            else
            {
                ThemSuaVatTuWindow themSuaVatTuWindow = new ThemSuaVatTuWindow();
                ThemSuaVatTuWindowViewModel VM = new ThemSuaVatTuWindowViewModel(MaKho);
                themSuaVatTuWindow.DataContext = VM;
                themSuaVatTuWindow.ShowDialog();

                if (VM.ReturnValue == true)
                {
                    foreach (var item in DanhSachVatTuYeuCau)
                    {
                        if(item.MaVT == VM.MaVT)
                        {
                            CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mặt hàng đã được yêu cầu, vui lòng chọn mặt hàng khác.");
                            msg2.ShowDialog();
                            return;
                        }
                    }
                    DanhSachVatTuYeuCau.Add(new VatTu() { MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT });
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin.", false);
                    msg.ShowDialog();
                }
                if (DanhSachVatTuYeuCau.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
            }
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                List<VatTu> NeedDeleting = new List<VatTu>();
                foreach (VatTu i in DanhSachVatTuYeuCau)
                {
                    if (i.Checked == true)
                    {
                        NeedDeleting.Add(i);
                        Count++;
                    }
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                foreach(VatTu v in NeedDeleting) DanhSachVatTuYeuCau.Remove(v);

                if (DanhSachVatTuYeuCau.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
            }
        }
        public ICommand CreateReceiptCommand {  get; set; }
        void CreateRecepit(object t)
        {
            if (EnableEditing == true)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng thoát chế độ chỉnh sửa trước khi in.", false);
                msg.ShowDialog();
            }
            else
            {
                HoaDonWindow printHoaDon = new HoaDonWindow();
                PrintViewModel VM = new PrintViewModel(this);
                printHoaDon.DataContext = VM;

                VM.GridToPrint = printHoaDon.Print;
                VM.Print();
            }
        }
        #endregion
        #region Function
        void LoadNhanVien()
        {
            NhanVien.Clear();
            var ListFromDB = DataProvider.Instance.DB.Employees.Where(p=>p.DaXoa ==false).ToList();
            foreach (var item in ListFromDB)
            {
                if(item.ChucVu == "Tiếp Nhận") NhanVien.Add(item.MaNv);
            }
        }
        void LoadKhachHang()
        {
            KhachHang.Clear();
            var ListFromDB = DataProvider.Instance.DB.Customers.Where(p => p.DaXoa == false).ToList();
            foreach(var item in ListFromDB)
            {
                KhachHang.Add(item.MaKh);
            }
        }
        void LoadKho()
        {
            Kho.Clear();
            var ListFromDB = DataProvider.Instance.DB.Warehouses.Where(p => p.DaXoa == false).ToList();
            foreach(var item in ListFromDB)
            {
                Kho.Add(item.MaKho);
            }
        }
        void LoadData(string maycx)
        {
            var YCX = DataProvider.Instance.DB.ExportRequests.Find(maycx);
            if (YCX != null)
            {
                MaYCX = maycx;
                MaKH = (YCX.MaKh != null) ? YCX.MaKh : "";
                MaNV = (YCX.MaNv != null) ? YCX.MaNv : "";
                MaKho = (YCX.KhoXuat != null) ? YCX.KhoXuat : "";
                NgayLap = (YCX.NgayLap != null) ? YCX.NgayLap.Value.ToString("ddd dd/MM/yyyy") : "";
                GhiChu = (YCX.GhiChu != null) ? YCX.GhiChu : "";
            }
        }
        void LoadDanhSach()
        {
            DanhSachVatTuYeuCau.Clear(); ListEmpty = true;
            VatTu Vt;
            List<ExportRequestInfo> request = DataProvider.Instance.DB.ExportRequestInfos.Where(c => c.MaYcx == MaYCX).ToList();
            foreach (var record in request)
            {
                Vt = new VatTu(record);
                DanhSachVatTuYeuCau.Add(Vt);
            }
            if (DanhSachVatTuYeuCau.Count() > 0) { ListEmpty = false; }
        }
        #endregion
        public class VatTu
        {
            public VatTu() { }
            public VatTu(ExportRequestInfo t)
            {
                Checked = false;
                MaVT = (t.MaVt == null)? "" : t.MaVt;
                SoLuong = (int)((t.SoLuong == null) ? 0 : t.SoLuong);
                ChietKhau = (double)((t.ChietKhau == null) ? 0: t.ChietKhau);
                VAT = (double)((t.Vat == null) ? 0 : t.Vat);
            }
            public bool Checked { get; set; }
            public string MaVT { get; set; }
            public int SoLuong { get; set; }
            public double ChietKhau { get; set; }
            public double VAT { get; set; }
        }
    }
}
