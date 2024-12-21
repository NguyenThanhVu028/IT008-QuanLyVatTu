using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class PrintViewModel:BaseViewModel
    {
        public PrintViewModel(ChiTietPhieuNhapWindowViewModel VM)
        {
            MaPN = VM.MaPN;
            MaNV = VM.MaNV;
            MaNCC = VM.MaNCC;
            var ncc = DataProvider.Instance.DB.Suppliers.Find(MaNCC);
            if (ncc != null) { DiaChiNCC = ncc.DiaChi ?? ""; TenNCC = ncc.TenNcc ?? "";}
            NgayLap = VM.NgayLap;
            KhoNhap = VM.KhoNhap;
            var kho = DataProvider.Instance.DB.Warehouses.Find(KhoNhap);
            if (kho != null) DiaChiKho = kho.DiaChi;
            LyDoNhap = VM.LyDoNhap;
            ChietKhau = VM.ChietKhau;
            VAT = VM.VAT;
            TongGia = VM.TongGia;
            var employee = DataProvider.Instance.DB.Employees.Find(MaNV);
            if (employee != null) NguoiLapPhieu = employee.HoTen ?? "";

            LoadDanhSachPN(); Calculate();
        }
        public PrintViewModel(ChiTietPhieuXuatWindowViewModel VM)
        {
            MaPX = VM.MaPX;
            MaNV = VM.MaNV;
            MaKH = VM.MaKH;
            var kh = DataProvider.Instance.DB.Customers.Find(MaKH);
            if (kh != null) { DiaChiKH = kh.DiaChi ?? ""; TenKH = kh.HoTen ?? ""; }
            NgayLap = VM.NgayLap;
            KhoXuat = VM.KhoXuat;
            var kho = DataProvider.Instance.DB.Warehouses.Find(KhoXuat);
            if (kho != null) DiaChiKho = kho.DiaChi ?? "";
            LyDoXuat = VM.LyDoXuat;
            ChietKhau = VM.ChietKhau;
            VAT = VM.VAT;
            TongGia = VM.TongGia;
            var employee = DataProvider.Instance.DB.Employees.Find(MaNV);
            if (employee != null) NguoiLapPhieu = employee.HoTen ?? "";

            LoadDanhSachPX(); Calculate();
        }
        public PrintViewModel(ThongTinYeuCauXuatHangWindowViewModel ycx)
        {
            var kh = DataProvider.Instance.DB.Customers.Find(ycx.MaKH);
            if (kh != null)
            {
                TenKH = kh.HoTen ?? "";
                DiaChiKH = kh.DiaChi ?? "";
                SDT = kh.Sdt ?? "";
                GhiChu = ycx.GhiChu ?? "";
            }
            LoadHoaDon(ycx.MaYCX); CalculatHoaDon();  
        }
        #region GridToPrint
        Grid _gridToPrint;
        public Grid GridToPrint
        {
            get { return _gridToPrint; }
            set { _gridToPrint = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachVatTu
        private ObservableCollection<VatTu> _danhSachVatTu = new ObservableCollection<VatTu>();
        public ObservableCollection<VatTu> DanhSachVatTu
        {
            get { return _danhSachVatTu; }
            set { _danhSachVatTu = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maPN = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maNCC = "";
        private string _tenNCC = "";
        private string _diaChiNCC = ""; 
        private string _ngayLap = "";
        private string _khoNhap = "";
        private string _diaChiKho = "";
        private string _lyDoNhap = "";
        private string _nguoiLapPhieu = "";
        private int _cong = 0;
        private int _chietSuat = 0;
        private int _vAT = 0;
        private int _tongGia = 0;

        private string _maPX = "";
        private string _maKH = "";
        private string _tenKH = "";
        private string _diaChiKH = "";
        private string _sDT = "";
        private string _ghiChu = "";
        private string _khoXuat = "";
        private string _lyDoXuat = "";

        public string MaPN
        {
            get { return _maPN; }
            set { _maPN = value; OnPropertyChanged(); }
        }
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string MaNCC
        {
            get { return _maNCC; }
            set { _maNCC = value; OnPropertyChanged(); }
        }
        public string TenNCC 
        {
            get { return _tenNCC; }
            set { _tenNCC = value; OnPropertyChanged(); }
        }
        public string DiaChiNCC
        {
            get { return _diaChiNCC; }
            set { _diaChiNCC = value; OnPropertyChanged(); }
        }
        public string NgayLap
        {
            get { return _ngayLap; }
            set { _ngayLap = value; OnPropertyChanged(); }
        }
        public string KhoNhap
        {
            get { return _khoNhap; }
            set { _khoNhap = value; OnPropertyChanged(); }
        }
        public string DiaChiKho
        {
            get { return _diaChiKho; }
            set { _diaChiKho = value; OnPropertyChanged(); }
        }
        public string LyDoNhap
        {
            get { return _lyDoNhap; }
            set { _lyDoNhap = value; OnPropertyChanged(); }
        }
        public string NguoiLapPhieu
        {
            get { return _nguoiLapPhieu; }
            set { _nguoiLapPhieu = value; OnPropertyChanged(); }
        }
        public int Cong
        {
            get { return _cong; }
            set { _cong = value; OnPropertyChanged(); }
        }
        public int ChietKhau
        {
            get { return _chietSuat; }
            set { _chietSuat = value; OnPropertyChanged(); }
        }
        public int VAT
        {
            get { return _vAT; }
            set { _vAT = value; OnPropertyChanged(); }
        }
        public int TongGia
        {
            get { return _tongGia; }
            set { _tongGia = value; OnPropertyChanged(); }
        }

        public string MaPX
        {
            get { return _maPX; }
            set { _maPX = value; OnPropertyChanged(); }
        }
        public string MaKH
        {
            get { return _maKH; }
            set { _maKH = value; OnPropertyChanged(); }
        }
        public string TenKH
        {
            get { return _tenKH; }
            set { _tenKH = value; OnPropertyChanged(); }
        }
        public string DiaChiKH
        {
            get { return _diaChiKH; }
            set { _diaChiKH = value; OnPropertyChanged(); }
        }
        public string SDT { get { return _sDT; } set { _sDT = value; OnPropertyChanged(); } }
        public string GhiChu { get { return _ghiChu; } set { _ghiChu = value; OnPropertyChanged(); } }
        public string KhoXuat
        {
            get { return _khoXuat; }
            set { _khoXuat = value; OnPropertyChanged(); }
        }
        public string LyDoXuat
        {
            get { return _lyDoXuat; }
            set { _lyDoXuat = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        void LoadDanhSachPN()
        {
            DanhSachVatTu.Clear(); int Count = 0;
            var PN = DataProvider.Instance.DB.GoodsReceivedNotes.Find(MaPN);
            if (PN != null)
            {
                var PNInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == MaPN).ToList();
                if(PNInfos != null)
                {
                    foreach(var item in PNInfos)
                    {
                        var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                        if(supply != null)
                        {
                            Count++;
                            VatTu temp = new VatTu();
                            temp.STT = Count;
                            temp.TenVT = supply.TenVatTu??"";
                            temp.MaVT = supply.MaVt;
                            temp.DVT = supply.DonViTinh??"";
                            temp.SoLuong = (int)(item.SoLuong??0);
                            temp.DonGia = (int)(supply.GiaNhap??0);
                            //temp.ChietKhau = (int)((temp.SoLuong * temp.DonGia * item.ChietKhau)??0);
                            //temp.VAT = (int)((temp.SoLuong * temp.DonGia * item.Vat) ?? 0);
                            temp.ThanhTien = temp.SoLuong * temp.DonGia;

                            DanhSachVatTu.Add(temp);
                        }
                        
                    }
                }
            }
        }
        void LoadDanhSachPX()
        {
            DanhSachVatTu.Clear(); int Count = 0;
            var PX = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(MaPX);
            if (PX != null)
            {
                var PXInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == MaPX).ToList();
                if (PXInfos != null)
                {
                    foreach (var item in PXInfos)
                    {
                        var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                        if (supply != null)
                        {
                            Count++;
                            VatTu temp = new VatTu();
                            temp.STT = Count;
                            temp.TenVT = supply.TenVatTu ?? "";
                            temp.MaVT = supply.MaVt;
                            temp.DVT = supply.DonViTinh ?? "";
                            temp.SoLuong = (int)(item.SoLuong ?? 0);
                            temp.DonGia = (int)(supply.GiaXuat ?? 0);
                            //temp.ChietKhau = (int)((temp.SoLuong * temp.DonGia * item.ChietKhau) ?? 0);
                            //temp.VAT = (int)((temp.SoLuong * temp.DonGia * item.Vat) ?? 0);
                            temp.ThanhTien = temp.SoLuong * temp.DonGia;

                            DanhSachVatTu.Add(temp);
                        }

                    }
                }
            }
        }
        void LoadHoaDon(string ycx)
        {
            DanhSachVatTu.Clear(); int Count = 0;
            var hd = DataProvider.Instance.DB.ExportRequests.Find(ycx);
            if(hd != null)
            {
                var cthd = DataProvider.Instance.DB.ExportRequestInfos.Where(p => p.MaYcx == ycx).ToList();
                foreach(var item in cthd)
                {
                    var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                    if(supply != null)
                    {
                        Count++;
                        VatTu temp = new VatTu();
                        temp.STT = Count;
                        temp.TenVT = supply.TenVatTu ?? "";
                        temp.MaVT = supply.MaVt;
                        temp.DVT = supply.DonViTinh ?? "";
                        temp.SoLuong = (int)(item.SoLuong ?? 0);
                        temp.DonGia = (int)(supply.GiaXuat ?? 0);
                        temp.CK = item.ChietKhau ?? 0;
                        temp.dVAT = item.Vat ?? 0;
                        //temp.ChietKhau = (int)((temp.SoLuong * temp.DonGia * item.ChietKhau) ?? 0);
                        //temp.VAT = (int)((temp.SoLuong * temp.DonGia * item.Vat) ?? 0);
                        temp.ThanhTien = temp.SoLuong * temp.DonGia;

                        DanhSachVatTu.Add(temp);
                    }
                }
            }
        }
        void Calculate()
        {
            Cong = 0; /*ChietKhau = 0; VAT = 0; TongGia = 0;*/
            foreach(var item in DanhSachVatTu)
            {
                Cong += item.ThanhTien;
                //ChietKhau += item.ChietKhau;
                //VAT += item.VAT;
                //TongGia += (item.ThanhTien - item.ChietKhau + item.VAT);
            }
        }
        void CalculatHoaDon()
        {
            Cong = 0;
            VAT = 0;
            ChietKhau = 0;
            foreach (var item in DanhSachVatTu)
            {
                Cong += item.ThanhTien;
                ChietKhau += (int)(item.CK * item.DonGia * item.SoLuong);
                VAT += (int)(item.dVAT * item.DonGia * item.SoLuong);
            }
            TongGia = Cong - ChietKhau + VAT;
        }
        public void Print()
        {
            if(GridToPrint != null)
            {
                PrintDialog dlg = new PrintDialog();
                if(dlg.ShowDialog() == true)
                {
                    dlg.PrintVisual(GridToPrint, "Phiếu nhập");
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Xuất file pdf thành công.", false);
                    msg.ShowDialog();
                }
            }
        }
        #endregion
        public class VatTu
        {
            public int STT {  get; set; }
            public string TenVT {  get; set; }
            public string MaVT {  get; set; }
            public string DVT { get; set; }
            public int SoLuong {  get; set; }
            public int DonGia {  get; set; }
            public int ChietKhau {  get; set; }
            public double CK {  get; set; }
            public double dVAT {  get; set; }
            public int VAT {  get; set; }
            public int ThanhTien {  get; set; }

        }
    }
}
