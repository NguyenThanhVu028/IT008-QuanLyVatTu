using Microsoft.EntityFrameworkCore.Metadata;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
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
            set { _selectedSearchFilter = value; OnPropertyChanged(); }
        }
        private string _searchString;
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

            var ListFromDB = DataProvider.Instance.DB.Customers.ToList();

            foreach ( var item in ListFromDB)
            {
                DanhSachKhachHang.Add(item);
            }

            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH000", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH001", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH002", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH003", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH004", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH005", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH006", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH007", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH008", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH009", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = true, MaKh = "KH010", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            //DanhSachKhachHang.Add(new Customer() { Checked = false, MaKh = "KH011", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa khách hàng đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                //Xóa trong database
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
