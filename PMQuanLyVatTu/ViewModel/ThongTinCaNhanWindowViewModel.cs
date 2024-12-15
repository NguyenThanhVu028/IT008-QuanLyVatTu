using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
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
            //LoadData(MaNV);
            LoadEmployeeInfo();
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
            string manv = CurrentUser.Instance.MaNv;
            var employee = DataProvider.Instance.DB.Employees.Find(manv) as Employee;
            if(employee != null)
            {
                employee.MaNv = MaNV;
                employee.HoTen = HoTen;
                employee.GioiTinh = GioiTinh.ToString();
                if (DateOnly.TryParse(NgaySinh, out DateOnly parsedNgaySinh))
                {
                    employee.NgaySinh = parsedNgaySinh;
                }
                else
                {
                    // Xử lý khi giá trị NgaySinh không hợp lệ, ví dụ: gán giá trị mặc định hoặc báo lỗi
                    employee.NgaySinh = default; // Hoặc một giá trị mặc định
                }
                employee.Sdt = SDT;
                employee.Email = Email;
                employee.DiaChi = DiaChi;
                DataProvider.Instance.DB.SaveChanges();

            }
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
            var result = DataProvider.Instance.DB.Employees.Find(manv);
            if (result != null)
            {
                MaNV = manv;
                HoTen = result.HoTen;
                GTinh = result.GioiTinh;
                NgaySinh = result.NgaySinh.ToString();
                SDT = result.Sdt;
                Email = result.Email;
                DiaChi = result.DiaChi;
            }
            else
            {
                MessageBox.Show("Null");
                MaNV = manv;
                HoTen = "";
                GTinh = "";
                NgaySinh = "";
                SDT = "";
                Email = "";
                DiaChi = "";
            }
        }
        public void LoadEmployeeInfo()
        {
            // Tìm Employee có MaNV trùng với CurrentUser.Instance.MaNV
            //var employee = DataProvider.Instance.DB.Employees
            //                 .FirstOrDefault(e => e.MaNv == CurrentUser.Instance.MaNv);
            string manv = CurrentUser.Instance.MaNv;
            var employee = DataProvider.Instance.DB.Employees.Find(manv) as Employee;

            if (employee != null)
            {
                // Gán giá trị từ Employee vào các thuộc tính, kiểm tra null để gán giá trị rỗng nếu cần
                MaNV = employee.MaNv ?? "";
                HoTen = employee.HoTen ?? "";
                GTinh = employee.GioiTinh ?? "";
                NgaySinh = employee.NgaySinh?.ToString() ?? ""; // Dùng ToString() với xử lý null
                SDT = employee.Sdt ?? "";
                Email = employee.Email ?? "";
                DiaChi = employee.DiaChi ?? "";
            }
            else
            {
                // Nếu không tìm thấy Employee, gán giá trị mặc định
                MaNV = CurrentUser.Instance.MaNv;
                HoTen = "";
                GTinh = "";
                NgaySinh = "";
                SDT = "";
                Email = "";
                DiaChi = "";
            }
        }
        #endregion
    }
}
