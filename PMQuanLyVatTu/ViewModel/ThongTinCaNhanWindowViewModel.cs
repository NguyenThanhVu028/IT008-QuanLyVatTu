using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
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
    public class ThongTinCaNhanWindowViewModel : BaseViewModel
    {
        public ThongTinCaNhanWindowViewModel(string MaNV)
        {
            LoadData(MaNV);

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            ChangePasswordCommand = new RelayCommand<object>(ChangePassword);
        }
        #region Info
        private string _maNV = "";
        private string _hoTen = "";
        private string _gTinh = "";
        private string _ngaySinh = "";
        private string _sDT = "";
        private string _email = "";
        private string _diaChi = "";
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; OnPropertyChanged(); }
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
        public string SDT
        {
            get { return _sDT; }
            set { _sDT = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() { "Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
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
        void EditInfo(object t) {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã vào chế độ chỉnh sửa thông tin.");
            msg.ShowDialog();
            if(msg.ReturnValue == true) EnableEditing = true;
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            EnableEditing = false;
            //Lưu xuống database
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
            msg.ShowDialog();
        }
        public ICommand ChangePasswordCommand { get; set; }
        void ChangePassword(object t)
        {
            ChangePasswordWindows changePasswordWindows = new ChangePasswordWindows();
            ChangePasswordViewModel VM = new ChangePasswordViewModel();
            changePasswordWindows.DataContext = VM;
            changePasswordWindows.ShowDialog();
        }
        #endregion
        #region Function
        void LoadData(string manv)
        {
           
            //Đổ thông tin NV vào info
            MaNV = manv;
            HoTen = "Nguyễn Văn A";
            GTinh = "Nam";
            NgaySinh = "23/12/2970";
            SDT = "0123456789";
            Email = "Default@gmail.com";
            DiaChi = "123 Phố Mỹ Hạnh";
        }
        #endregion
    }
}
