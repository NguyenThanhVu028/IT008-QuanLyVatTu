using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {
        public KhachHangViewModel()
        {
            DeleteButtonCommand = new RelayCommand<object>(Delete);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            RefreshCommand = new RelayCommand<object>(Refresh);
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH000", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH001", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH002", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH003", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH004", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH005", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH006", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH007", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH008", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH009", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = true, MaKH = "KH010", HoTen = "Nguyen Van A", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
            DanhSachKhachHang.Add(new KhachHang() { Checked = false, MaKH = "KH011", HoTen = "Nguyen Van B", GioiTinh = "Nam", SDT = "0123456789", Email = "stte@gmail.com", NgaySinh = "28/6/1998", DiaChi = "Phường Đông Hòa, Dĩ An" });
        }

        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã khách hàng", "Họ và tên", "Số điện thoại", "Giới tính", "Ngày sinh" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
        private ObservableCollection<KhachHang> _danhSachKhachHang = new ObservableCollection<KhachHang>();
        public ObservableCollection<KhachHang> DanhSachKhachHang
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
        private KhachHang _selectedKhachHang;
        public KhachHang SelectedKhachHang
        {
            get { return _selectedKhachHang; }
            set
            {
                _selectedKhachHang = value; OnPropertyChanged();
            }
        }
        public ICommand DeleteButtonCommand { get; set; }
        void Delete(object t)
        {
            if (SelectedKhachHang != null)
            {
                MessageBox.Show(SelectedKhachHang.HoTen);
            }
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            foreach (KhachHang kh in DanhSachKhachHang)
            {
                if (kh.Checked == true)
                {
                    MessageBox.Show(kh.HoTen);
                }
            }
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t)
        {
            MessageBox.Show("Refresh command is executed");
        }
    }

    public class KhachHang
    {
        public bool Checked {  get; set; }
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string NgaySinh { get; set; }
        public string DiaChi { get; set; }
    }
}
