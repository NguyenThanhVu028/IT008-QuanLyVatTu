using Microsoft.IdentityModel.Tokens;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.PrintWindows;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PMQuanLyVatTu.ViewModel
{
    public class ChiTietPhieuXuatWindowViewModel : BaseViewModel
    {
        public ChiTietPhieuXuatWindowViewModel(string mapx = null) 
        {
            if (mapx != null) { EditMode = true; Title = "CHỈNH SỬA PHIẾU XUẤT"; LoadData(mapx); LoadDanhSach(); }
            else { EditMode = false; EnableEditing = true; Title = "THÊM PHIẾU XUẤT"; }

            LoadNhanVien(); LoadKho(); LoadKhachHang();

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            InPhieuCommand = new RelayCommand<object>(InPhieu);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<Window>(SaveInfo);
            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            SelectFromRequestCommand = new RelayCommand<object>(SelectFromRequest);
        }
        #region User
        CurrentUser _user = CurrentUser.Instance;
        public CurrentUser User { get { return _user; } }
        #endregion
        #region Title
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maPX = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maKH = "";
        private string _ngayLap = DateTime.Now.ToString("ddd/dd/MM/yyyy");
        private string _khoXuat = "";
        private string _lyDoXuat = "";
        private int _chietSuat = 0;
        private int _vAT = 0;
        private int _tongGia = 0;
        public string MaPX
        {
            get { return _maPX; }
            set { _maPX = value; OnPropertyChanged(); }
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
        public string NgayLap
        {
            get { return _ngayLap; }
            set { _ngayLap = value; OnPropertyChanged(); }
        }
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
        #endregion
        #region ListEmpty
        private bool _listEmpty = true;
        public bool ListEmpty
        {
            get { return _listEmpty; }
            set { _listEmpty = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _kho = new ObservableCollection<string>();
        public ObservableCollection<string> Kho
        {
            get { return _kho; }
            set { _kho = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _khachHang = new ObservableCollection<string>();
        public ObservableCollection<string> KhachHang
        {
            get { return _khachHang; }
            set { _khachHang = value; OnPropertyChanged(); }
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
        #region DanhSachVatTuToDelete
        private ObservableCollection<VatTu> _danhSachVatTuToDelete = new ObservableCollection<VatTu>();
        public ObservableCollection<VatTu> DanhSachVatTuToDelete
        {
            get { return _danhSachVatTuToDelete; }
            set { _danhSachVatTuToDelete = value; OnPropertyChanged(); }
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
        public ICommand InPhieuCommand { get; set; }
        void InPhieu(object t)
        {
            if (EnableEditing == true)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng thoát chế độ chỉnh sửa trước khi in.", false);
                msg.ShowDialog();
            }
            else
            {
                PrintPhieuXuat printPhieuXuat = new PrintPhieuXuat();
                PrintViewModel VM = new PrintViewModel(this);
                printPhieuXuat.DataContext = VM;

                VM.GridToPrint = printPhieuXuat.Print;
                VM.Print();

            }
        }
        public ICommand DeleteButtonCommand {  get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa phiếu xuất đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            { //Chấp nhận xóa
                EnableEditing = false;
                var PhieuXuat = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(MaPX);
                if(PhieuXuat == null)
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy phiếu để xóa!", false);
                    msg.ShowDialog();
                }
                else
                {
                    PhieuXuat.DaXoa = true;
                    PhieuXuat.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "THÔNG BÁO", "Xóa phiếu nhập thành công.");
                    msg1.ShowDialog();
                }

                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có xác nhận lưu?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == false) return;
            if (EditMode == true)
            {
                EnableEditing = false;
                var PhieuXuat = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(MaPX);
                if(PhieuXuat != null)
                {
                    PhieuXuat.MaNv = MaNV;
                    PhieuXuat.MaKh = MaKH;
                    PhieuXuat.KhoXuat = KhoXuat;
                    try{ PhieuXuat.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                    catch{ PhieuXuat.NgayLap = default; }
                    PhieuXuat.LyDoXuat = LyDoXuat;
                    PhieuXuat.ChietKhau = ChietKhau;
                    PhieuXuat.Vat = VAT;
                    PhieuXuat.TongGia = TongGia;
                    PhieuXuat.DaXoa = false;

                    var PhieuXuatInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == MaPX);
                    if (PhieuXuatInfos != null) DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(PhieuXuatInfos);

                    foreach (var item in DanhSachVatTu)
                    {
                        GoodsDeliveryNoteInfo temp = new GoodsDeliveryNoteInfo() { MaPx = MaPX, MaVt = item.MaVT, SoLuong = item.SoLuong, ChietKhau = item.ChietKhau, Vat = item.VAT };
                        DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Add(temp);
                    }
                    DataProvider.Instance.DB.SaveChanges();
                    CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.", false);
                    msg2.ShowDialog();
                }
                else
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu đã không còn trên hệ thống.");
                    msg1.ShowDialog();
                    return;
                }

            }
            else 
            {
                if (MaPX.IsNullOrEmpty()) 
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã phiếu xuất hàng.", false);
                    msg1.ShowDialog();
                    return;
                }
                if(MaPX.Length > 6)
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mã phiếu không được vượt quá 6 ký tự.", false);
                    msg1.ShowDialog();
                    return;
                }
                if (MaNV.IsNullOrEmpty())
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã nhân viên.", false);
                    msg1.ShowDialog();
                    return;
                }
                if (MaKH.IsNullOrEmpty())
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã khách hàng.", false);
                    msg1.ShowDialog();
                    return;
                }
                if (KhoXuat.IsNullOrEmpty())
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn kho xuất.", false);
                    msg1.ShowDialog();
                    return;
                }
                var PhieuXuat = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(MaPX);
                if (PhieuXuat != null) 
                {
                    if(PhieuXuat.DaXoa == false)
                    {
                        AlreadyExistsError msg1 = new AlreadyExistsError();
                        msg1.ShowDialog();
                        return;
                    }
                    else
                    {
                        PhieuXuat.MaNv = MaNV;
                        PhieuXuat.MaKh = MaKH;
                        PhieuXuat.KhoXuat = KhoXuat;
                        try { PhieuXuat.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                        catch { PhieuXuat.NgayLap = default; }
                        PhieuXuat.LyDoXuat = LyDoXuat;
                        PhieuXuat.ChietKhau = ChietKhau;
                        PhieuXuat.Vat = VAT;
                        PhieuXuat.TongGia = TongGia;
                        PhieuXuat.TrangThai = "Chưa duyệt";
                        PhieuXuat.DaXoa = false;

                        var PhieuXuatInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == MaPX);
                        if (PhieuXuatInfos != null) DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(PhieuXuatInfos);

                        foreach (var item in DanhSachVatTu)
                        {
                            GoodsDeliveryNoteInfo temp = new GoodsDeliveryNoteInfo() { MaPx = MaPX, MaVt = item.MaVT, SoLuong = item.SoLuong, ChietKhau = item.ChietKhau, Vat = item.VAT };
                            DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Add(temp);
                        }
                    }
                }
                else
                {
                    GoodsDeliveryNote px = new GoodsDeliveryNote();
                    px.MaPx = MaPX;
                    px.MaNv = MaNV;
                    px.MaKh = MaKH;
                    px.KhoXuat = KhoXuat;
                    try { px.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                    catch { px.NgayLap = default; }
                    px.LyDoXuat = LyDoXuat;
                    px.ChietKhau = ChietKhau;
                    px.Vat = VAT;
                    px.TongGia = TongGia;
                    px.TrangThai = "Chưa duyệt";
                    px.DaXoa = false;
                    DataProvider.Instance.DB.GoodsDeliveryNotes.Add(px);

                    var PhieuXuatInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == MaPX);
                    if (PhieuXuatInfos != null) DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(PhieuXuatInfos);

                    foreach (var item in DanhSachVatTu)
                    {
                        GoodsDeliveryNoteInfo temp = new GoodsDeliveryNoteInfo() { MaPx = MaPX, MaVt = item.MaVT, SoLuong = item.SoLuong, ChietKhau = item.ChietKhau, Vat = item.VAT };
                        DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Add(temp);
                    }
                }

                EnableEditing = false;
                DataProvider.Instance.DB.SaveChanges();
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm phiếu xuất thành công.", false);
                msg2.ShowDialog();
                t.Close();

            }
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            if (KhoXuat.IsNullOrEmpty())
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập đầy đủ kho xuất hàng.");
                msg.ShowDialog();
                return;
            }
            ThemSuaVatTuWindow themSuaVatTuWindow = new ThemSuaVatTuWindow();
            ThemSuaVatTuWindowViewModel VM = new ThemSuaVatTuWindowViewModel(KhoXuat, "");
            themSuaVatTuWindow.DataContext = VM;
            themSuaVatTuWindow.ShowDialog();

            if (VM.ReturnValue == true)
            {
                foreach (var item in DanhSachVatTu)
                {
                    if (item.MaVT == VM.MaVT)
                    {
                        CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mặt hàng đã được yêu cầu, vui lòng chọn mặt hàng khác.");
                        msg2.ShowDialog();
                        return;
                    }
                }
                var item2 = DataProvider.Instance.DB.Supplies.Find(VM.MaVT);
                if (item2 == null || item2.DaXoa == true)
                {
                    CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vật tư đã không còn trên hện thống, vui lòng chọn vật tư khác.");
                    msg2.ShowDialog();
                    return;
                }
                int GNY = item2 != null ? (int)item2.GiaNhap : 0;
                DanhSachVatTu.Add(new VatTu() { Checked = false, MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT, GiaNiemYet = GNY, ThanhTien = (int?)(GNY * VM.SoLuong * (1 - VM.ChietKhau + VM.VAT)) }); Calculate();
                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
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
                foreach (VatTu i in DanhSachVatTu)
                {
                    if (i.Checked == true)
                    {
                        NeedDeleting.Add(i);
                        Count++;
                    }
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                foreach (VatTu i in NeedDeleting)
                {
                    DanhSachVatTu.Remove(i);
                }
                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
                Calculate();
            }
        }
        public ICommand SelectFromRequestCommand { get; set; }
        void SelectFromRequest(object t)
        {
            SelectFromRequestWindow SelectWin = new SelectFromRequestWindow();
            SelectFromRequestWindowViewModel VM = new SelectFromRequestWindowViewModel(false, MaKH, KhoXuat);
            SelectWin.DataContext = VM;
            SelectWin.ShowDialog();

            if (VM.ReturnValue == true && VM.SelectedYeuCau != null)
            {
                string MaYCX = VM.SelectedYeuCau.MaYC;
                var YCX = DataProvider.Instance.DB.ExportRequests.Find(MaYCX);
                if(YCX == null || YCX.DaXoa == true)
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu đã chọn! Vui lòng chọn mã yêu cầu khác.", false);
                    msg.ShowDialog();
                    return;
                }
                else
                {
                    var YCXInfo = DataProvider.Instance.DB.ExportRequestInfos.Where(p => p.MaYcx == YCX.MaYcx);
                    if(YCXInfo != null)
                    {
                        foreach(var item in YCXInfo)
                        {
                            var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                            if(supply == null || supply.DaXoa == true)
                            {
                                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mã vật tư: " + item.MaVt + " đã không còn tồn tại.");
                                msg2.ShowDialog();
                                continue;
                            }
                            foreach(var vt in DanhSachVatTu)
                            {
                                if(vt.MaVT == supply.MaVt)
                                {
                                    CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mã vật tư: " + supply.MaVt + " đã được yêu cầu.");
                                    msg2.ShowDialog();
                                    continue;
                                }
                            }

                            VatTu temp = new VatTu() 
                            {
                                Checked = false,
                                MaVT = item.MaVt,
                                SoLuong = item.SoLuong,
                                ChietKhau = item.ChietKhau,
                                VAT = item.Vat
                            };

                            temp.GiaNiemYet = supply.GiaXuat ?? 0;
                            temp.ThanhTien = (int?)(temp.GiaNiemYet * temp.SoLuong * (1 - ChietKhau + VAT));
                            DanhSachVatTu.Add(temp);
                        }
                    }
                    MaKH = VM.Ma1 ?? "";
                    KhoXuat = VM.Ma2 ?? "";
                }
            }
            Calculate();
            if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
            else ListEmpty = true;
        }
        #endregion
        #region Function
        void LoadKhachHang()
        {
            KhachHang.Clear();

            var ListFromDB = DataProvider.Instance.DB.Customers.ToList();
            foreach (var item in ListFromDB)
            {
                if(item.DaXoa == false)
                {
                    KhachHang.Add(item.MaKh);
                }
            }
        }
        void LoadKho()
        {
            Kho.Clear();

            var ListFromDB = DataProvider.Instance.DB.Warehouses.ToList();
            foreach (var item in ListFromDB)
            {
                if (item.DaXoa == false)
                {
                    Kho.Add(item.MaKho);
                }
            }
        }
        void LoadNhanVien()
        {
            var ListFromDB = DataProvider.Instance.DB.Employees.ToList();
            foreach (var item in ListFromDB)
            {
                if ((item.ChucVu == "Nhập Xuất" || item.ChucVu == "Quản Lý") && item.DaXoa == false) NhanVien.Add(item.MaNv);
            }
        }
        void LoadData(string mapx)
        {
            var PhieuXuat = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(mapx);
            if(PhieuXuat == null)
            {
                MaPX = "";
            }
            else
            {
                MaPX = PhieuXuat.MaPx ?? "";
                MaNV = PhieuXuat.MaNv ?? "";
                MaKH = PhieuXuat.MaKh ?? "";
                NgayLap = PhieuXuat.NgayLap?.ToString("ddd/dd/MM/yyyy") ?? "";
                KhoXuat = PhieuXuat.KhoXuat ?? "";
                LyDoXuat = PhieuXuat.LyDoXuat ?? "";
            }
        }
        void LoadDanhSach()
        {
            DanhSachVatTu.Clear();
            var InnerList = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == MaPX);
            if(InnerList != null)
            {
                foreach (var item in InnerList)
                {
                    VatTu add = new VatTu(item);
                    DanhSachVatTu.Add(add);
                }
            }

            if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
            else ListEmpty = true;
            Calculate();
        }
        void Calculate()
        {
            ChietKhau = 0;
            VAT = 0;
            TongGia = 0;
            foreach (VatTu i in DanhSachVatTu)
            {
                var supply = DataProvider.Instance.DB.Supplies.Find(i.MaVT);
                if (supply != null)
                {
                    ChietKhau += (int)((supply.GiaXuat ?? 0) * i.SoLuong * i.ChietKhau);
                    VAT += (int)((supply.GiaXuat ?? 0) * i.SoLuong * i.VAT);
                    TongGia += (int)((supply.GiaXuat ?? 0) * i.SoLuong);
                }

                //Lấy giá nhập của vật tư i
                //ChietSuat += GiaNhap(i) * SoLuong * ChietSuat
                //VAT += GiaNHap(i) * SoLuong * VAT
                //TongGia += GiaNhap(i) * SoLuong
            }
            TongGia = TongGia - ChietKhau + VAT;
        }
        #endregion
        public class VatTu
        {
            public VatTu() { }
            public VatTu(GoodsDeliveryNoteInfo t) 
            {
                Checked = false;
                MaVT = t.MaVt ?? "";
                SoLuong = t.SoLuong ?? 0;
                ChietKhau = t.ChietKhau ?? 0;
                VAT = t.Vat ?? 0;
                var supply = DataProvider.Instance.DB.Supplies.Find(MaVT);
                if(supply == null)
                {
                    ThanhTien = 0;
                }
                else
                {
                    GiaNiemYet = supply.GiaXuat ?? 0;
                    ThanhTien = (int?)(GiaNiemYet * SoLuong - GiaNiemYet * SoLuong * ChietKhau + GiaNiemYet * SoLuong * VAT);
                }
                
            }
            public bool Checked { get; set; }
            public string MaVT { get; set; }
            public int? SoLuong { get; set; }
            public double? ChietKhau { get; set; }
            public double? VAT { get; set; }
            public int? GiaNiemYet { get; set; }
            public int? ThanhTien { get; set; }
        }
    }
}
