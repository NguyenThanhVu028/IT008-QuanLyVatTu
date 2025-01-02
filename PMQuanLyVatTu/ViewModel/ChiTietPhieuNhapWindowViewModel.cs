using Microsoft.IdentityModel.Tokens;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.PrintWindows;
using PMQuanLyVatTu.User;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using static PMQuanLyVatTu.ViewModel.ThongTinYeuCauXuatHangWindowViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class ChiTietPhieuNhapWindowViewModel : BaseViewModel
    {
        public ChiTietPhieuNhapWindowViewModel(string mapn = null) 
        {
            if(mapn != null) { EditMode = true; Title = "CHỈNH SỬA PHIẾU NHẬP"; LoadData(mapn); LoadDanhSach();}
            else { EditMode = false; EnableEditing = true; Title = "THÊM PHIẾU NHẬP"; MaNV = CurrentUser.Instance.MaNv; NgayLap = DateTime.Now.ToString("ddd/dd/MM/yyyy"); }

            LoadNhaCungCap(); LoadKho(); LoadNhanVien();

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
        private string _maPN = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maNCC = "";
        private string _ngayLap = DateTime.Now.ToString("ddd/dd/MM/yyyy");
        private string _khoNhap = "";
        private string _lyDoNhap = "";
        private int _chietSuat = 0;
        private int _vAT = 0;
        private int _tongGia = 0;
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
            set { _maNCC = value; OnPropertyChanged(); LoadKho(); }
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
        public string LyDoNhap
        {
            get { return _lyDoNhap; }
            set { _lyDoNhap = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _nhaCungCap = new ObservableCollection<string>();
        public ObservableCollection<string> NhaCungCap
        {
            get { return _nhaCungCap; }
            set { _nhaCungCap = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _kho = new ObservableCollection<string>();
        public ObservableCollection<string> Kho
        {
            get { return _kho; }
            set { _kho = value; OnPropertyChanged(); }
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
        public ICommand InPhieuCommand {  get; set; }
        void InPhieu(object t)
        {
            if(EnableEditing == true)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng thoát chế độ chỉnh sửa trước khi in.", false);
                msg.ShowDialog();
            }
            else
            {
                PrintPhieuNhap printPhieuNhap = new PrintPhieuNhap();
                PrintViewModel VM = new PrintViewModel(this);
                printPhieuNhap.DataContext = VM;

                VM.GridToPrint = printPhieuNhap.Print;
                VM.Print();

            }
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa phiếu nhập đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                EnableEditing = false;
                var PhieuNhap = DataProvider.Instance.DB.GoodsReceivedNotes.Find(MaPN);
                if (PhieuNhap == null)
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy phiếu để xóa!", false);
                    msg.ShowDialog();
                }
                else
                {
                    PhieuNhap.DaXoa = true;
                    PhieuNhap.ThoiGianXoa = DateTime.Now;
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
                var PhieuNhap = DataProvider.Instance.DB.GoodsReceivedNotes.Find(MaPN);
                if(PhieuNhap != null)
                {
                    PhieuNhap.MaNv = MaNV;
                    PhieuNhap.MaNcc = MaNCC;
                    try{ PhieuNhap.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                    catch{ PhieuNhap.NgayLap = default; }
                    PhieuNhap.KhoNhap = KhoNhap;
                    PhieuNhap.LyDoNhap = LyDoNhap;
                    PhieuNhap.ChietKhau = ChietKhau;
                    PhieuNhap.Vat = VAT;
                    PhieuNhap.TongGia = TongGia;
                    PhieuNhap.DaXoa = false;

                    var PhieuNhapInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == PhieuNhap.MaPn);
                    if(PhieuNhapInfos != null) DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(PhieuNhapInfos);

                    foreach(var item in DanhSachVatTu)
                    {
                        GoodsReceivedNoteInfo temp = new GoodsReceivedNoteInfo() { MaPn = MaPN, MaVt = item.MaVT, ChietKhau = item.ChietKhau, Vat = item.VAT, SoLuong = item.SoLuong };
                        DataProvider.Instance.DB.GoodsReceivedNoteInfos.Add(temp);
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
                if (MaPN.IsNullOrEmpty()) 
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã phiếu nhập hàng.", false);
                    msg1.ShowDialog();
                    return;
                }
                if(MaPN.Length > 6)
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
                if (MaNCC.IsNullOrEmpty())
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã nhà cung cấp.", false);
                    msg1.ShowDialog();
                    return;
                }
                if (KhoNhap.IsNullOrEmpty())
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã kho nhập.", false);
                    msg1.ShowDialog();
                    return;
                }
                var pn = DataProvider.Instance.DB.GoodsReceivedNotes.Find(MaPN);
                if (pn != null) 
                {
                    if(pn.DaXoa == false)
                    {
                        AlreadyExistsError msg1 = new AlreadyExistsError();
                        msg1.ShowDialog();
                        return;
                    }
                    else
                    {
                        pn.MaNv = MaNV;
                        pn.MaNcc = MaNCC;
                        try { pn.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                        catch { pn.NgayLap = default; }
                        pn.KhoNhap = KhoNhap;
                        pn.LyDoNhap = LyDoNhap;
                        pn.ChietKhau = ChietKhau;
                        pn.Vat = VAT;
                        pn.TongGia = TongGia;
                        pn.TrangThai = "Chưa duyệt";
                        pn.DaXoa = false;

                        var PhieuNhapInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == pn.MaPn);
                        if (PhieuNhapInfos != null) DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(PhieuNhapInfos);

                        foreach (var item in DanhSachVatTu)
                        {
                            GoodsReceivedNoteInfo temp = new GoodsReceivedNoteInfo() { MaPn = MaPN, MaVt = item.MaVT, ChietKhau = item.ChietKhau, Vat = item.VAT, SoLuong = item.SoLuong };
                            DataProvider.Instance.DB.GoodsReceivedNoteInfos.Add(temp);
                        }
                    }
                }
                else
                {
                    GoodsReceivedNote newNote = new GoodsReceivedNote();
                    newNote.MaPn = MaPN;
                    newNote.MaNv = MaNV;
                    newNote.MaNcc = MaNCC;
                    try { newNote.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture); }
                    catch { newNote.NgayLap = default; }
                    newNote.KhoNhap = KhoNhap;
                    newNote.LyDoNhap = LyDoNhap;
                    newNote.ChietKhau = ChietKhau;
                    newNote.Vat = VAT;
                    newNote.TongGia = TongGia;
                    newNote.TrangThai = "Chưa duyệt";
                    newNote.DaXoa = false;
                    DataProvider.Instance.DB.GoodsReceivedNotes.Add(newNote);

                    var PhieuNhapInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == newNote.MaPn);
                    if (PhieuNhapInfos != null) DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(PhieuNhapInfos);

                    foreach (var item in DanhSachVatTu)
                    {
                        GoodsReceivedNoteInfo temp = new GoodsReceivedNoteInfo() { MaPn = MaPN, MaVt = item.MaVT, ChietKhau = item.ChietKhau, Vat = item.VAT, SoLuong = item.SoLuong };
                        DataProvider.Instance.DB.GoodsReceivedNoteInfos.Add(temp);
                    }
                }
                EnableEditing = false;
                DataProvider.Instance.DB.SaveChanges();
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm phiếu nhập thành công.", false);
                msg2.ShowDialog();
                t.Close();
            }
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            if (MaNCC.IsNullOrEmpty()|| KhoNhap.IsNullOrEmpty())
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập đầy đủ nhà cung cấp và kho nhập hàng.");
                msg.ShowDialog();
                return;
            }
            ThemSuaVatTuWindow themSuaVatTuWindow = new ThemSuaVatTuWindow();
            ThemSuaVatTuWindowViewModel VM = new ThemSuaVatTuWindowViewModel(KhoNhap, MaNCC);
            themSuaVatTuWindow.DataContext = VM;
            themSuaVatTuWindow.ShowDialog();

            if(VM.ReturnValue == true)
            {
                foreach(var item in DanhSachVatTu)
                {
                    if(item.MaVT == VM.MaVT)
                    {
                        CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mặt hàng đã được yêu cầu, vui lòng chọn mặt hàng khác.");
                        msg2.ShowDialog();
                        return;
                    }
                }
                var item2 = DataProvider.Instance.DB.Supplies.Find(VM.MaVT);
                if(item2 == null || item2.DaXoa == true)
                {
                    CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vật tư đã không còn trên hện thống, vui lòng chọn vật tư khác.");
                    msg2.ShowDialog();
                    return;
                }
                int GNY = item2.GiaNhap != null ? (int)item2.GiaNhap : 0;
                DanhSachVatTu.Add(new VatTu() { Checked = false, MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT, GiaNiemYet = GNY, ThanhTien = (int?)(GNY *VM.SoLuong*(1-VM.ChietKhau+VM.VAT)) }) ; Calculate();
                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
            }
            Calculate();
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                List<VatTu> NeedDeleting = new List<VatTu>();
                foreach (VatTu i in DanhSachVatTu)
                {
                    if(i.Checked == true)
                    {
                        NeedDeleting.Add(i);
                        Count++;
                    }
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                foreach(VatTu i in NeedDeleting) DanhSachVatTu.Remove(i);

                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
                Calculate();
            }
        }
        public ICommand SelectFromRequestCommand { get; set; }
        void SelectFromRequest(object t)
        {
            SelectFromRequestWindow SelectWin = new SelectFromRequestWindow();
            SelectFromRequestWindowViewModel VM = new SelectFromRequestWindowViewModel(true, MaNCC, KhoNhap);
            SelectWin.DataContext = VM;
            SelectWin.ShowDialog();

            if(VM.ReturnValue == true && VM.SelectedYeuCau != null)
            {
                string MaYCN = VM.SelectedYeuCau.MaYC;
                var YeuCau = DataProvider.Instance.DB.ImportRequests.Find(MaYCN);
                if (YeuCau == null || YeuCau.DaXoa == true)
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu đã chọn! Vui lòng chọn mã yêu cầu khác.", false);
                    msg.ShowDialog();
                    return;
                }
                
                var supply = DataProvider.Instance.DB.Supplies.Find(YeuCau.MaVt);
                if (supply == null || supply.DaXoa == true)
                {
                    CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mặt hàng yêu cầu đã không còn tồn tại.");
                    msg2.ShowDialog();
                    return;
                }

                foreach(var item in DanhSachVatTu)
                {
                    if(item.MaVT == supply.MaVt)
                    {
                        CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mặt hàng đã được yêu cầu, vui lòng chọn mặt hàng khác.");
                        msg2.ShowDialog();
                        return;
                    }
                }

                VatTu temp = new VatTu() 
                { 
                    Checked = false, 
                    MaVT = YeuCau.MaVt??"", 
                    SoLuong = YeuCau.SoLuong, 
                    ChietKhau = VM.ChietKhau, 
                    VAT = VM.VAT 
                };
                temp.GiaNiemYet = supply.GiaNhap ?? 0;
                temp.ThanhTien = (int?)(temp.GiaNiemYet * temp.SoLuong * (1 - ChietKhau + VAT));

                MaNCC = (VM.Ma1 == null) ? "" : VM.Ma1; 
                KhoNhap = (VM.Ma2 == null) ? "" : VM.Ma2;
                DanhSachVatTu.Add(temp);
            }
            Calculate();
            if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
            else ListEmpty = true;
        }
        #endregion
        #region Function
        void LoadNhaCungCap()
        {
            NhaCungCap.Clear();

            var ListFromDB = DataProvider.Instance.DB.Suppliers.Where(p=>p.DaXoa==false).ToList();
            foreach(var item in ListFromDB)
            {
                NhaCungCap.Add(item.MaNcc);
            }
        }
        void LoadKho()
        {
            Kho.Clear();

            var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
            foreach (var item in ListFromDB)
            {
                if (item.DaXoa == false)
                {
                    if((item.MaNcc == null || item.MaNcc.Contains(MaNCC)) && item.MaKho != null) Kho.Add(item.MaKho);
                }
            }
            Kho = new ObservableCollection<string>(Kho.Distinct().ToList().OrderBy(q => q));
        }
        void LoadNhanVien()
        {
            NhanVien.Clear();

            var ListFromDB = DataProvider.Instance.DB.Employees.ToList();
            foreach (var item in ListFromDB)
            {
                if (item.DaXoa == false)
                {
                    if((item.ChucVu == "Nhập Xuất" || item.ChucVu == "Quản Lý")) NhanVien.Add(item.MaNv);
                }
            }
        }
        void LoadData(string mapn)
        {
            var pn = DataProvider.Instance.DB.GoodsReceivedNotes.Find(mapn);
            if (pn == null)
            {
                MaPN = "";
            }
            else
            {
                MaPN = pn.MaPn;
                MaNV = pn.MaNv ?? "";
                MaNCC = pn.MaNcc ?? "";
                NgayLap = pn.NgayLap?.ToString("ddd/dd/MM/yyyy") ?? "";
                KhoNhap = pn.KhoNhap ?? "";
                LyDoNhap = pn.LyDoNhap ?? "";
            }

        }
        void LoadDanhSach()
        {
            DanhSachVatTu.Clear();
            var InnerList = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == MaPN);
            if (InnerList != null)
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
                if(supply != null)
                {
                    ChietKhau += (int)((supply.GiaNhap??0) * i.SoLuong * i.ChietKhau);
                    VAT += (int)((supply.GiaNhap??0) * i.SoLuong * i.VAT);
                    TongGia += (int)((supply.GiaNhap??0) * i.SoLuong);
                }
            }
            TongGia = TongGia - ChietKhau + VAT;
        }
        #endregion
        public class VatTu
        {
            public VatTu() { }
            public VatTu(GoodsReceivedNoteInfo t) 
            {
                Checked = false;
                MaVT = (t.MaVt == null)? "" : t.MaVt; 
                SoLuong = (t.SoLuong == null)? 0:t.SoLuong; 
                ChietKhau = (t.ChietKhau == null)? 0:t.ChietKhau; 
                VAT = (t.Vat == null) ? 0 : t.Vat;
                var item = DataProvider.Instance.DB.Supplies.Find(t.MaVt);
                if (item == null) ThanhTien = 0;
                else
                {
                    GiaNiemYet = item.GiaNhap??0;
                    ThanhTien = (int?)(GiaNiemYet * SoLuong - (GiaNiemYet * SoLuong * ChietKhau) + GiaNiemYet * SoLuong * VAT);
                }
            }
            public bool Checked { get; set; }
            public string MaVT { get; set; }
            public int? SoLuong { get; set; }
            public double? ChietKhau { get; set; }
            public double? VAT { get; set; }
            public int? GiaNiemYet {  get; set; }
            public int? ThanhTien {  get; set; }
        }
    }
}
