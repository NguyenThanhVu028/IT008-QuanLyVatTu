using Microsoft.EntityFrameworkCore.Metadata;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {
        public KhachHangViewModel()
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            //DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            Refresh();
        }
        #region Info
        public string MaNV
        {
            get { return CurrentUser.Instance.MaNv; }
        }
        public string ChucVu
        {
            get { return CurrentUser.Instance.ChucVu; }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã khách hàng", "Họ và tên", "Số điện thoại", "Giới tính", "Ngày sinh" };
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
        private string _searchString = "";
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); Refresh(); }
        }
        #endregion
        #region DanhSachKhachHang
        private ObservableCollection<Customer> _danhSachKhachHang = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> DanhSachKhachHang
        {
            get
            {
                return _danhSachKhachHang;
            }
            set
            {
                _danhSachKhachHang = value; OnPropertyChanged();
            }
        }
        private Customer _selectedKhachHang;
        public Customer SelectedKhachHang
        {
            get { return _selectedKhachHang; }
            set
            {
                _selectedKhachHang = value; OnPropertyChanged();
            }
        }
        #endregion
        #region Command
        public ICommand AddCommand {  get; set; }
        void Add(object t)
        {
            ThongTinKhachHangWindow AddWindow = new ThongTinKhachHangWindow();
            ThongTinKhachHangWindowViewModel TTNVVM = new ThongTinKhachHangWindowViewModel();
            AddWindow.DataContext = TTNVVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("kh");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachKhachHang.Clear();

            var ListFromDB = DataProvider.Instance.DB.Customers.Where(e => e.DaXoa == false).ToList();

            foreach ( var item in ListFromDB)
            {
                switch (SelectedSearchFilter)
                {
                    case "Mã khách hàng":
                        if (item.MaKh != null)
                            if (item.MaKh.ToLower().Contains(SearchString.ToLower())) DanhSachKhachHang.Add(item);
                        break;
                    case "Họ và tên":
                        if (item.HoTen != null)
                            if (item.HoTen.ToLower().Contains(SearchString.ToLower())) DanhSachKhachHang.Add(item);
                        break;
                    case "Số điện thoại":
                        if (item.Sdt != null)
                            if (item.Sdt.ToLower().Contains(SearchString.ToLower())) DanhSachKhachHang.Add(item);
                        break;
                    case "Giới tính":
                        if (item.GioiTinh != null)
                            if (item.GioiTinh.ToLower().Contains(SearchString.ToLower())) DanhSachKhachHang.Add(item);
                        break;
                    case "Ngày sinh":
                        if (item.NgaySinh != null)
                            if (item.NgaySinh.ToString().Contains(SearchString)) DanhSachKhachHang.Add(item);
                        break;
                    default:
                        DanhSachKhachHang.Add(item);
                        break;
                }
            }
        }
        //void Refresh(object t = null)
        //{
        //    DanhSachKhachHang.Clear();

        //    var ListFromDB = DataProvider.Instance.DB.Customers.ToList();

        //    foreach (var item in ListFromDB)
        //    {
        //        DanhSachKhachHang.Add(item);
        //    }
        //}
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa khách hàng đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var kh = DataProvider.Instance.DB.Customers.Find(SelectedKhachHang.MaKh);
                if (kh != null)
                {
                    kh.DaXoa = true;
                    DataProvider.Instance.DB.SaveChanges();
                }
            }
            Refresh();
        }
        public ICommand EditButtonCommand {  get; set; }
        void EditButton(object t)
        {
            string s = SelectedKhachHang.MaKh;
            ThongTinKhachHangWindowViewModel TTVTVM = new ThongTinKhachHangWindowViewModel(s);
            ThongTinKhachHangWindow AddWindow = new ThongTinKhachHangWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        bool EditButtonCondition(object t)
        {
            if (CurrentUser.Instance.ChucVu == "Quản Lý") return true;
            else return false;
        }
        //public ICommand DeleteSelectedCommand { get; set; }
        //void DeleteSelected(object t)
        //{
        //    int Count = 0;
        //    CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
        //    msg.ShowDialog();
        //    if (msg.ReturnValue == true)
        //    {
        //        foreach (Customer i in DanhSachKhachHang)
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
    }

    //public class Customer
    //{
    //    public bool Checked {  get; set; }
    //    public string MaKH { get; set; }
    //    public string HoTen { get; set; }
    //    public string GioiTinh { get; set; }
    //    public string SDT { get; set; }
    //    public string Email { get; set; }
    //    public string NgaySinh { get; set; }
    //    public string DiaChi { get; set; }
    //}
}
