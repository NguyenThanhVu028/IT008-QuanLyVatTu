using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinNhanVienWindowViewModel : BaseViewModel
    {
        public ThongTinNhanVienWindowViewModel(string manv = null)
        {
            if(manv != null) { EditMode = true; Title = "CHỈNH SỬA NHÂN VIÊN"; LoadData(manv); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM NHÂN VIÊN"; }

            CloseCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            ChangeAvatarCommand = new RelayCommand<object>(ChangeAvatar);
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
        private string _maNV = "";
        private string _hoVaTen;
        private string _cVu;
        private string _gTinh;
        private string _ngaySinh;
        private string _diaChi;
        private string _sDT;
        private string _email;
        private string _luong;
        private string _tenDangNhap;
        private string _matKhau;
        private string _imageLocation;
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string HoTen
        {
            get { return _hoVaTen; }
            set { _hoVaTen = value; OnPropertyChanged(); }
        }
        public string CVu
        {
            get { return _cVu; }
            set { _cVu = value; OnPropertyChanged(); }
        }
        public string GTinh
        {
            get { return _gTinh; }
            set { _gTinh = value; OnPropertyChanged(); }
        }
        public string NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; OnPropertyChanged(); }
        }
        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; OnPropertyChanged();}
        }
        public string SDT
        {
            get { return _sDT; }
            set { _sDT = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged();}
        }
        public string Luong
        {
            get { return _luong; }
            set { _luong = value; OnPropertyChanged(); }
        }
        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set { _tenDangNhap = value; OnPropertyChanged(); }
        }
        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; OnPropertyChanged(); }
        }
        public string ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() {"Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _chucVu = new ObservableCollection<string>() { "QL", "KT", "NX", "TN" };
        public ObservableCollection<string> ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseCommand { get; set; }
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa nhân viên đã chọn?", true);
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
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm nhân viên
            {
                if (MaNV == "") //Chưa nhập mã nhân viên
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã nhân viên.");
                    msg.ShowDialog();
                }
                else if (true) //Trùng mã nhân viên
                {
                    AlreadyExistsError msg = new AlreadyExistsError();
                    msg.ShowDialog();
                }
                else // Hợp lệ
                { 
                    EnableEditing = false;
                    //Lưu thông tin từ Info vào database
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhân viên thành công.");
                    msg.ShowDialog();
                    (t as Window).Close();
                }
            }
        }
        public ICommand ChangeAvatarCommand { get; set; }
        void ChangeAvatar(object t)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG|*.png|JPG|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageLocation = openFileDialog.FileName;
            }
        }
        #endregion
        #region Function
        void LoadData(string manv)
        {
            //Dùng manv để load dữ liệu từ database
            MaNV = manv;
            HoTen = "Nguyễn Văn A";
            CVu = "QL";
            GTinh = "Nam";
            NgaySinh = "23/09/1998";
            DiaChi = "123, Tô Vĩnh Diện, Thành phố Thủ Đức";
            SDT = "0123456789";
            Email = "defaultEmail@gmail.com";
            Luong = "12000000";
            TenDangNhap = "admin1";
            MatKhau = "admin1";
            ImageLocation = "/Material/Images/Avatars/user6.jpg";
        }
        #endregion
    }
}
