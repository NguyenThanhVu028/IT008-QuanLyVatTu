using PMQuanLyVatTu.CustomControls;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PMQuanLyVatTu.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        public NhanVienViewModel() 
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
            //DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
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
        private string _selectedSearchFilter = "Không";
        public string SelectedSearchFilter
        {
            get { return _selectedSearchFilter; }
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); }
        }
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); Refresh(); }
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
        public ObservableCollection<NhanVienDisplayer> DisplayDanhSachNhanVienLargeIcon { get; set; }
        private Employee _selectedNhanVien;
        public Employee SelectedNhanVien
        {
            get { return _selectedNhanVien; }
            set { _selectedNhanVien = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            ThongTinNhanVienWindow AddWindow = new ThongTinNhanVienWindow();
            ThongTinNhanVienWindowViewModel TTNVVW = new ThongTinNhanVienWindowViewModel();
            AddWindow.DataContext = TTNVVW;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("nv");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        //void Refresh(object t = null)
        //{
        //    DanhSachNhanVien.Clear();

        //    DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0001", HoTen = "Nguyễn Văn An", ChucVu = "QL", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user1.jpg" });
        //    DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0002", HoTen = "Nguyễn Thị Bình", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nữ", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user2.jpg" });
        //    DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "NX", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user3.jpg" });
        //    DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Hữu Tính", ChucVu = "TN", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user4.jpg" });
        //    DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user5.jpg" });

        //    ObservableCollection<NhanVienDisplayer> temp = new ObservableCollection<NhanVienDisplayer>();
        //    foreach (Employee emp in DanhSachNhanVien)
        //    {
        //        NhanVienDisplayer newButton = new NhanVienDisplayer();
        //        if (emp.ChucVu == "QL") newButton.BorderBrush = Brushes.Yellow;
        //        else if(false) newButton.BorderBrush = Brushes.Green;               //Nếu là ng đang dùng
        //        else newButton.BorderBrush = Brushes.Aqua;
        //        newButton.DataContext = emp; newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 300; newButton.Width = 220; newButton.Click += NhanVienDisplayerClicked;
        //        temp.Add(newButton);
        //    }
        //    DanhSachNhanVienLargeIcon = temp;
        //}
        void Refresh(object t = null)
        {
            DanhSachNhanVien.Clear();

            var ListFromDB = DataProvider.Instance.DB.Employees.ToList();
            foreach(var item in  ListFromDB)
            {
                DanhSachNhanVien.Add(item);
            }

            //DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0001", HoTen = "Nguyễn Văn An", ChucVu = "QL", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user1.jpg" });
            //DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0002", HoTen = "Nguyễn Thị Bình", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nữ", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user2.jpg" });
            //DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "NX", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user3.jpg" });
            //DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Hữu Tính", ChucVu = "TN", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user4.jpg" });
            //DanhSachNhanVien.Add(new Employee() { Checked = true, MaNV = "NV0003", HoTen = "Nguyễn Trọng Phước Thành Danh", ChucVu = "KT", NgaySinh = "19/5/1998", GioiTinh = "Nam", SDT = "0912345678", Email = "de@gmqaial.com", DiaChi = "Số 354 Phường Linh Xuân, Quận Tân Hiệp Thành", Luong = 35000000, ImageLocation = "/Material/Images/Avatars/user5.jpg" });

            if(DanhSachNhanVien.Count() > DanhSachNhanVienLargeIcon.Count())
            {
                int temp1 = DanhSachNhanVien.Count(), temp2 = DanhSachNhanVienLargeIcon.Count();
                for(int i=0; i< temp1 - temp2; i++)
                {
                    NhanVienDisplayer newButton = new NhanVienDisplayer();
                    newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 300; newButton.Width = 220; newButton.Click += NhanVienDisplayerClicked;
                    DanhSachNhanVienLargeIcon.Add(newButton);
                }
            }

            ObservableCollection<NhanVienDisplayer> temp = new ObservableCollection<NhanVienDisplayer>();
            for(int i=0; i<DanhSachNhanVien.Count(); i++)
            {
                DanhSachNhanVienLargeIcon[i].DataContext = DanhSachNhanVien[i];
                if (DanhSachNhanVien[i].ChucVu == "Quản Lý") DanhSachNhanVienLargeIcon[i].BorderBrush = Brushes.Yellow;
                else if (false) DanhSachNhanVienLargeIcon[i].BorderBrush = Brushes.Green;               //Nếu là ng đang dùng
                else DanhSachNhanVienLargeIcon[i].BorderBrush = Brushes.Aqua;
                temp.Add(DanhSachNhanVienLargeIcon[i]);
            }
            DisplayDanhSachNhanVienLargeIcon = temp;
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t = null)
        {
            string s = SelectedNhanVien.MaNv;
            ThongTinNhanVienWindowViewModel TTVTVM = new ThongTinNhanVienWindowViewModel(s);
            ThongTinNhanVienWindow AddWindow = new ThongTinNhanVienWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa nhân viên đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                //Xóa trong database theo SelectedNhanVien
            }
            Refresh();
        }
        //public ICommand DeleteSelectedCommand { get; set; }
        //void DeleteSelected(object t)
        //{
        //    int Count = 0;
        //    CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
        //    msg.ShowDialog();
        //    if (msg.ReturnValue == true)
        //    {
        //        foreach (Employee i in DanhSachNhanVien)
        //        {
        //            if (i.Checked == true)
        //            {
        //                //Xóa
        //                Count++;
        //            }
        //        }
        //        CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
        //        msg2.ShowDialog();
        //        Refresh();
        //    }
        //}
        #endregion
        #region Function
        void NhanVienDisplayerClicked(object sender, EventArgs e)
        {
            string manv = ((sender as NhanVienDisplayer).DataContext as Employee).MaNv;
            ThongTinNhanVienWindowViewModel TTVTVM = new ThongTinNhanVienWindowViewModel(manv);
            ThongTinNhanVienWindow AddWindow = new ThongTinNhanVienWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        #endregion
        //public class Employee
        //{
        //    public bool Checked { get; set; }
        //    public string MaNV { get; set; }
        //    public string HoTen { get; set; }
        //    public string ChucVu { get; set; }
        //    public string NgaySinh { get; set; }
        //    public string GioiTinh { get; set; }
        //    public string SDT { get; set; }
        //    public string Email { get; set; }
        //    public string DiaChi { get; set; }
        //    public int Luong { get; set; }
        //    public string ImageLocation { get; set; }
        //}
    }
}
