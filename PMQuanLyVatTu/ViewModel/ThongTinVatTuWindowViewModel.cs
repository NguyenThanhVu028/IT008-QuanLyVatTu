using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinVatTuWindowViewModel : BaseViewModel
    {
        public ThongTinVatTuWindowViewModel(string mavt = null)
        {
            if(mavt != null) { EditMode = true; Title = "CHỈNH SỬA VẬT TƯ"; LoadData(mavt); }
            else { EditMode = false; EnableEditing = true; Title = "THÊM VẬT TƯ"; }

            CloseCommand = new RelayCommand<Window>(CloseWin);
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
        private string _maVT = "";
        private string _tenVatTu;
        private string _loaiVT;
        private string _donViTinh;
        private string _maNCC;
        private string _maKho;
        private string _giaNhap;
        private string _giaXuat;
        private string _soLuongTonKho;
        private string _imageLocation;
        public string MaVT
        {
            get { return _maVT; }
            set { _maVT = value; OnPropertyChanged(); }
        }
        public string TenVatTu
        {
            get { return _tenVatTu; }
            set { _tenVatTu = value; OnPropertyChanged(); }
        }
        public string LoaiVT
        {
            get { return _loaiVT; }
            set { _loaiVT = value; OnPropertyChanged(); }
        }
        public string DonViTinh
        {
            get { return _donViTinh; }
            set { _donViTinh = value; OnPropertyChanged(); }
        }
        public string MaNCC
        {
            get { return _maNCC; }
            set { _maNCC = value; OnPropertyChanged(); }
        }
        public string MaKho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); }
        }
        public string GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; OnPropertyChanged(); }
        }
        public string GiaXuat
        {
            get { return _giaXuat; }
            set { _giaXuat = value; OnPropertyChanged(); }
        }
        public string SoLuongTonKho
        {
            get { return _soLuongTonKho; }
            set { _soLuongTonKho = value; OnPropertyChanged(); }
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
        ObservableCollection<string> _nhaCungCap = new ObservableCollection<string>() {"NCC1", "NCC2", "NCC3", "NCC4" };
        public ObservableCollection<string> NhaCungCap
        {
            get { return _nhaCungCap; }
            set { _nhaCungCap = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _loaiVatTu = new ObservableCollection<string>() {"NVL", "CC", "TB" };
        public ObservableCollection<string> LoaiVatTu
        {
            get { return _loaiVatTu; }
            set { _loaiVatTu = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _khoChua = new ObservableCollection<string>() { "KHO1", "KHO2", "KHO3" };
        public ObservableCollection<string> KhoChua
        {
            get { return _khoChua; }
            set { _khoChua = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseCommand { get; set; }
        void CloseWin(Window window)
        {
            EnableEditing = false;
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa vật tư đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                EnableEditing = false;
                //Dùng MaVT để xóa trong database
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            if(EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                //Lưu thông tin trên database theo MaVT
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm vật tư
            {
                if(MaVT == "") //Chưa nhập mã vật tư
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã vật tư.");
                    msg1.ShowDialog();
                    return;
                }
                if (false) //Trùng mã vật tư
                {
                    AlreadyExistsError msg2 = new AlreadyExistsError();
                    msg2.ShowDialog();
                    return;
                }
                // Hợp lệ
                EnableEditing = false;
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm vật tư thành công.");
                msg.ShowDialog();
                (t as Window).Close();
            }
        }
        public ICommand ChangeAvatarCommand {  get; set; }
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
        void LoadData(string mavt)
        {
            //Dùng mavt để load dữ liệu từ database, dùng các property trong "Info" để hiện thông tin
            MaVT = mavt;
            TenVatTu = "Máy hàn chịu nhiệt";
            LoaiVT = "TB";
            DonViTinh = "Chiếc";
            MaNCC = "NCC0001";
            MaKho = "KHO0002";
            GiaNhap = "10000000";
            GiaXuat = "12000000";
            SoLuongTonKho = "30";
            ImageLocation = "/Material/Images/Supplies/welding_machine.jpg";
        }
        #endregion
    }
}
