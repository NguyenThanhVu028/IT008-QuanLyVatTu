using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
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
using System.Windows.Interop;
using static PMQuanLyVatTu.ViewModel.ThongTinYeuCauXuatHangWindowViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class ChiTietPhieuNhapWindowViewModel : BaseViewModel
    {
        public ChiTietPhieuNhapWindowViewModel(string mapn = null) 
        {
            if(mapn != null) { EditMode = true; Title = "CHỈNH SỬA PHIẾU NHẬP"; LoadData(mapn); LoadDanhSach();}
            else { EditMode = false; EnableEditing = true; Title = "THÊM PHIẾU NHẬP"; }

            LoadNhaCungCap(); LoadKho(); LoadNhanVien();

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
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maPN = "";
        private string _maNV;
        private string _maNCC = "";
        private string _ngayLap;
        private string _khoNhap = "";
        private string _lyDoNhap;
        private int _chietSuat;
        private int _vAT;
        private int _tongGia;
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
        private bool _editMode;
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
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Xuất file pdf thành công.", false);
                msg.ShowDialog();
                //Mở file pdf
            }
        }
        public ICommand DeleteButtonCommand { get; set; }
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
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.", false);
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm 
            {
                if (MaPN == "") //Chưa nhập mã 
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã yêu cầu xuất hàng.", false);
                    msg.ShowDialog();
                }
                else if (true) //Trùng mã 
                {
                    AlreadyExistsError msg = new AlreadyExistsError();
                    msg.ShowDialog();
                }
                else // Hợp lệ
                {
                    EnableEditing = false;
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhân viên thành công.", false);
                    msg.ShowDialog();
                    (t as Window).Close();
                }
            }
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            if (MaNCC == "" || KhoNhap == "")
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập đầy đủ nhà cung cấp và kho nhập hàng.");
                msg.ShowDialog();
                return;
            }
            ThemSuaVatTuWindow themSuaVatTuWindow = new ThemSuaVatTuWindow();
            ThemSuaVatTuWindowViewModel VM = new ThemSuaVatTuWindowViewModel();
            themSuaVatTuWindow.DataContext = VM;
            themSuaVatTuWindow.ShowDialog();

            if(VM.ReturnValue == true)
            {
                DanhSachVatTu.Add(new VatTu() { MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT }); Calculate();
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
                        if (EditMode == true) //Nếu đang chỉnh sửa
                        {
                            //Xóa trong database database
                        }
                        else //Đang thêm mới
                        {
                            NeedDeleting.Add(i);
                        }
                        Count++;
                    }
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                if (EditMode == true) { LoadDanhSach(); } //Cập nhật danh sách từ database
                else
                {
                    foreach (VatTu v in NeedDeleting) DanhSachVatTu.Remove(v);
                }
                if (DanhSachVatTu.Count() > 0) { ListEmpty = false; }
                else ListEmpty = true;
            }
        }
        public ICommand SelectFromRequestCommand { get; set; }
        void SelectFromRequest(object t)
        {
            if (MaNCC == "" || KhoNhap == "")
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập đầy đủ nhà cung cấp và kho nhập hàng.");
                msg.ShowDialog();
                return;
            }
            SelectFromRequestWindow SelectWin = new SelectFromRequestWindow();
            SelectFromRequestWindowViewModel VM = new SelectFromRequestWindowViewModel(true);
            SelectWin.DataContext = VM;
            SelectWin.ShowDialog();
        }
        #endregion
        #region Function
        void LoadNhaCungCap()
        {
            NhaCungCap.Clear();

            var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
            foreach(var item in ListFromDB)
            {
                if(item.DaXoa == false)
                {
                    if(item.MaKho.Contains(KhoNhap)) NhaCungCap.Add(item.MaNcc);
                }
            }
            NhaCungCap = new ObservableCollection<string>(NhaCungCap.Distinct().ToList().OrderBy(q=>q));
        }
        void LoadKho()
        {
            Kho.Clear();

            var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
            foreach (var item in ListFromDB)
            {
                if (item.DaXoa == false)
                {
                    if(item.MaNcc.Contains(MaNCC)) Kho.Add(item.MaKho);
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
                    if(item.ChucVu == "Nhập Xuất") NhanVien.Add(item.MaNv);
                }
            }
        }
        void LoadData(string maycx)
        {
            MaPN = maycx;
            MaNV = "NV0001";
            MaNCC = "NCC0005";
            NgayLap = "03/12/2024";
            KhoNhap = "KHO0004";
            LyDoNhap = "Kho hàng cần được làm mới";
        }
        void LoadDanhSach()
        {
            DanhSachVatTu.Clear();

            var ListFromDB = DataProvider.Instance.DB.GoodsReceivedNoteInfos.ToList();
            foreach(var item in ListFromDB)
            {
                if(item.MaPn == MaPN)
                {
                    VatTu temp = new VatTu(item);
                    DanhSachVatTu.Add(temp);
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
            public VatTu(GoodsReceivedNoteInfo t) 
            {
                Checked = false;
                MaVT = t.MaVt; SoLuong = t.SoLuong; ChietKhau = t.ChietKhau; VAT = t.Vat;
                var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
                foreach(var item in ListFromDB)
                {
                    if(item.MaVt == t.MaVt) { GiaNiemYet = item.GiaXuat; break; }
                }
                ThanhTien = (int?)(GiaNiemYet * SoLuong - (GiaNiemYet * SoLuong * ChietKhau) + GiaNiemYet * SoLuong + VAT);
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
