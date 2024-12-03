using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinYeuCauXuatHangWindowViewModel:BaseViewModel
    {
        public ThongTinYeuCauXuatHangWindowViewModel(string maycx = null) 
        {
            if(maycx != null) { EditMode = true; Title = "CHỈNH SỬA YÊU CẦU XUẤT"; LoadData(maycx); LoadDanhSach(); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM YÊU CẦU XUẤT"; }

            NhanVien.Add("NV0001"); NhanVien.Add("NV0002"); NhanVien.Add("NV0003");
            KhachHang.Add("KH0001"); KhachHang.Add("KH0002");

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);

            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            CreateReceiptCommand = new RelayCommand<object>(CreateRecepit);
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
        private string _maYCX = "";
        private string _maNV;
        private string _maKH;
        private string _ngayLap;
        private string _ghiChu;
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
        private ObservableCollection<string> _khachHang = new ObservableCollection<string>();
        public ObservableCollection<string> KhachHang
        {
            get { return _khachHang; }
            set { _khachHang = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachVatTuYeuCau
        private ObservableCollection<VatTuYeuCau> _danhSachVatTuYeuCau = new ObservableCollection<VatTuYeuCau>();
        public ObservableCollection<VatTuYeuCau> DanhSachVatTuYeuCau
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
                if (MaYCX == "") //Chưa nhập mã 
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã yêu cầu xuất hàng.");
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
            ThemSuaVatTuWindow themSuaVatTuWindow = new ThemSuaVatTuWindow();
            ThemSuaVatTuWindowViewModel VM = new ThemSuaVatTuWindowViewModel();
            themSuaVatTuWindow.DataContext = VM;
            themSuaVatTuWindow.ShowDialog();

            DanhSachVatTuYeuCau.Add(new VatTuYeuCau() { MaVT = VM.MaVT, SoLuong = VM.SoLuong, ChietKhau = VM.ChietKhau, VAT = VM.VAT });
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                List<VatTuYeuCau> NeedDeleting = new List<VatTuYeuCau>();
                foreach (VatTuYeuCau i in DanhSachVatTuYeuCau)
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
                if(EditMode == true) { LoadDanhSach(); }
                else
                {
                    foreach(VatTuYeuCau v in NeedDeleting) DanhSachVatTuYeuCau.Remove(v);
                }
            }
        }
        public ICommand CreateReceiptCommand {  get; set; }
        void CreateRecepit(object t)
        {
            MessageBox.Show("Created Recepit");
        }
        #endregion
        #region Function
        void LoadData(string maycx)
        {
            MaYCX = maycx;
            MaNV = "NV0001";
            MaKH = "KH0005";
            NgayLap = "03/12/2024";
            GhiChu = "Xuất đủ hàng và giao đi trước ngày 07/12/2024";
        }
        void LoadDanhSach()
        {
            DanhSachVatTuYeuCau.Clear();

            DanhSachVatTuYeuCau.Add(new VatTuYeuCau() { Checked = false, MaVT = "VT00001", SoLuong = 50, ChietKhau = 0.6, VAT = 0.7 });
            DanhSachVatTuYeuCau.Add(new VatTuYeuCau() { Checked = true, MaVT = "VT00002", SoLuong = 10, ChietKhau = 0.2, VAT = 0.3 });
            DanhSachVatTuYeuCau.Add(new VatTuYeuCau() { Checked = false, MaVT = "VT00005", SoLuong = 5, ChietKhau = 0.9, VAT = 0.1 });
        }
        #endregion
        public class VatTuYeuCau
        {
            public bool Checked { get; set; }
            public string MaVT { get; set; }
            public int SoLuong { get; set; }
            public double ChietKhau { get; set; }
            public double VAT { get; set; }
        }
    }
}
