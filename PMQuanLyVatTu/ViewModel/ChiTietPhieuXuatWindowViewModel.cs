using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            SelectFromRequestCommand = new RelayCommand<object>(SelectFromRequest);
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
        private string _maPX = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maKH = "";
        private string _ngayLap = "";
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
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Xuất file pdf thành công.", false);
                msg.ShowDialog();
                //Mở file pdf
            }
        }
        public ICommand DeleteButtonCommand {  get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa vật tư đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            { //Chấp nhận xóa
                EnableEditing = false;
                //Xóa
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                //Thay đổi Info
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.", false);
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm 
            {
                if (MaPX == "") //Chưa nhập mã 
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã phiếu xuất hàng.", false);
                    msg1.ShowDialog();
                    return;
                }
                if (true) //Trùng mã 
                {
                    AlreadyExistsError msg1 = new AlreadyExistsError();
                    msg1.ShowDialog();
                }

                EnableEditing = false;
                //Thêm phiếu + Thêm Info
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm phiếu xuất thành công.", false);
                msg.ShowDialog();
                (t as Window).Close();

            }
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            if (MaKH == "" || KhoXuat == "")
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập đầy đủ khách hàng và kho xuất hàng.");
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
                int GNY = 0;
                if (item2 != null) GNY = (int)item2.GiaNhap;
                DanhSachVatTu.Add(new VatTu() { Checked = false, MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT, GiaNiemYet = GNY, ThanhTien = (int?)(GNY * VM.SoLuong * (1 - VM.ChietKhau + VM.VAT)) }); Calculate();
                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
            }
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
            }
        }
        public ICommand SelectFromRequestCommand { get; set; }
        void SelectFromRequest(object t)
        {
            SelectFromRequestWindow SelectWin = new SelectFromRequestWindow();
            SelectFromRequestWindowViewModel VM = new SelectFromRequestWindowViewModel(false, MaKH, KhoXuat);
            SelectWin.DataContext = VM;
            SelectWin.ShowDialog();

            //Thực hiện

            if (VM.ReturnValue == true)
            {
                MaKH = VM.Ma1; KhoXuat = VM.Ma2;
            }
        }
        #endregion
        #region Function
        void LoadData(string mapx)
        {
            //Load
        }
        void LoadDanhSach()
        {
            DanhSachVatTu.Clear();

            if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
            else ListEmpty = true;

            Calculate();
        }
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
                if (item.ChucVu == "Nhập Xuất" && item.DaXoa == false) NhanVien.Add(item.MaNv);
            }
        }
        void Calculate()
        {
            ChietKhau = 0;
            VAT = 0;
            TongGia = 0;
            foreach (VatTu i in DanhSachVatTu)
            {
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
                MaVT = t.MaVt; SoLuong = t.SoLuong; ChietKhau = t.ChietKhau; VAT = t.Vat;
                var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
                foreach (var item in ListFromDB)
                {
                    if (item.MaVt == MaVT) { GiaNiemYet = item.GiaXuat; break; }
                }
                ThanhTien = (int?)(GiaNiemYet * SoLuong - GiaNiemYet * SoLuong * ChietKhau + GiaNiemYet * SoLuong * VAT);
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
