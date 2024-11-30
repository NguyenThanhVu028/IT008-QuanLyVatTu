using PMQuanLyVatTu.CustomControls;
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
    public class NhanVienViewModel : BaseViewModel
    {
        public NhanVienViewModel() {
            RefreshCommand = new RelayCommand<object>(Refresh);
            Refresh();
        }
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() {"Mã nhân viên", "Họ và tên", "Chức vụ", "Giới tính", "Chức vụ", "Ngày sinh", "Số điện thoại", "Email", "Địa chỉ", "Lương" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
        #endregion
        #region SearchBar
        private string _selectedSearchFilter;
        public string SelectedSearchFilter
        {
            get { return _selectedSearchFilter; }
            set { _selectedSearchFilter = value; OnPropertyChanged(); }
        }
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachNhanVien
        private ObservableCollection<Employee> _danhSachNhanVien = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> DanhSachNhanVien
        {
            get { return _danhSachNhanVien; }
            set { _danhSachNhanVien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NhanVienDisplayer> _danhSachNhanVienLargeIcon = new ObservableCollection<NhanVienDisplayer>();
        public ObservableCollection<NhanVienDisplayer> DanhSachNhanVienLargeIcon
        {
            get { return _danhSachNhanVienLargeIcon; }
            set { _danhSachNhanVienLargeIcon = value; OnPropertyChanged(); }
        }
        private Employee _selectedNhanVien;
        public Employee SelectedNhanVien
        {
            get { return _selectedNhanVien; }
            set { _selectedNhanVien = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachNhanVien.Clear();

            DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0001", HoTen = "Nguyễn Văn An", ChucVu = "QL", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user1.jpg" });
            DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0002", HoTen = "Nguyễn Thị Bình", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nữ", SDT = "0912345678", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user2.jpg" });
            DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "NX", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user3.jpg" });
            DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Hữu Tính", ChucVu = "TN", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user4.jpg" });
            DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user5.jpg" });

            ObservableCollection<NhanVienDisplayer> temp = new ObservableCollection<NhanVienDisplayer>();
            foreach (Employee emp in DanhSachNhanVien)
            {
                NhanVienDisplayer newButton = new NhanVienDisplayer();
                newButton.DataContext = emp; newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 300; newButton.Width = 220;
                temp.Add(newButton);
            }
            DanhSachNhanVienLargeIcon = temp;
        }
        #endregion
    }
    public class Employee
    {
        public bool Checked { get; set; }
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public int Luong { get; set; }
        public string ImageLocation { get; set; }
    }
}
