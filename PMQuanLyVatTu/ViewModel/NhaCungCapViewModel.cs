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
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class NhaCungCapViewModel : BaseViewModel
    {
        public NhaCungCapViewModel()
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
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
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã nhà cung cấp", "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại", "Email" };
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
        #region DanhSachNhaCungCap
        private ObservableCollection<Supplier> _danhSachNhaCungCap = new ObservableCollection<Supplier>();
        public ObservableCollection<Supplier> DanhSachNhaCungCap
        {
            get { return _danhSachNhaCungCap; }
            set { _danhSachNhaCungCap = value; }
        }
        private Supplier _selectedNhaCungCap;
        public Supplier SelectedNhaCungCap
        {
            get { return _selectedNhaCungCap; }
            set { _selectedNhaCungCap = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand {  get; set; }
        void Add(object t)
        {
            ThongTinNhaCungCapWindow AddWin = new ThongTinNhaCungCapWindow();
            ThongTinNhaCungCapWindowViewModel VM = new ThongTinNhaCungCapWindowViewModel();
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("ncc");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachNhaCungCap.Clear();

            var ListFromDB = DataProvider.Instance.DB.Suppliers.Where(c => c.DaXoa == false).ToList();
            if (ListFromDB != null)
            {
                foreach (var item in ListFromDB)
                {
                    switch (SelectedSearchFilter)
                    {
                        case "Mã nhà cung cấp":
                            if (item.MaNcc != null)
                                if (item.MaNcc.Contains(SearchString)) DanhSachNhaCungCap.Add(item);
                            break;
                        case "Tên nhà cung cấp":
                            if (item.TenNcc != null)
                                if (item.TenNcc.Contains(SearchString)) DanhSachNhaCungCap.Add(item);
                            break;
                        case "Địa chỉ":
                            if (item.DiaChi != null)
                                if (item.DiaChi.Contains(SearchString)) DanhSachNhaCungCap.Add(item);
                            break;
                        case "Số điện thoại":
                            if (item.Sdt != null)
                                if (item.Sdt.Contains(SearchString)) DanhSachNhaCungCap.Add(item);
                            break;
                        case "Email":
                            if (item.Email != null)
                                if (item.Email.Contains(SearchString)) DanhSachNhaCungCap.Add(item);
                            break;
                        default:
                            DanhSachNhaCungCap.Add(item);
                            break;
                    }
                }
            }
            //DanhSachNhaCungCap.Add(new Supplier() { Checked = true, MaNCC = "NCC0001", TenNCC = "Đại lý bán hàng A", SDT = "0123456789", Email = "DLA@gmail.com", DiaChi = "123 Phường Đồng Xuân An" });
            //DanhSachNhaCungCap.Add(new Supplier() { Checked = true, MaNCC = "NCC0002", TenNCC = "Đại lý bán hàng B", SDT = "0123456789", Email = "DLB@gmail.com", DiaChi = "123 Phường Đồng Xuân An" });
            //DanhSachNhaCungCap.Add(new Supplier() { Checked = true, MaNCC = "NCC0003", TenNCC = "Đại lý bán hàng C", SDT = "0123456789", Email = "DLC@gmail.com", DiaChi = "123 Phường Đồng Xuân An" });
            //DanhSachNhaCungCap.Add(new Supplier() { Checked = true, MaNCC = "NCC0004", TenNCC = "Đại lý bán hàng D", SDT = "0123456789", Email = "DLD@gmail.com", DiaChi = "123 Phường Đồng Xuân An" });
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t = null)
        {
            string s = SelectedNhaCungCap.MaNcc;
            ThongTinNhaCungCapWindowViewModel TTVTVM = new ThongTinNhaCungCapWindowViewModel(s);
            ThongTinNhaCungCapWindow AddWindow = new ThongTinNhaCungCapWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa nhà cung cấp đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var NCC = DataProvider.Instance.DB.Suppliers
                            .Find(SelectedNhaCungCap.MaNcc);

                if (NCC != null)
                {
                    NCC.DaXoa = true;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy nhà cung cấp để xóa!", false);
                }
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
        //        foreach (Supplier i in DanhSachNhaCungCap)
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
        //public class Supplier
        //{
        //    public bool Checked { get; set; }
        //    public string MaNCC {  get; set; }
        //    public string TenNCC {  get; set; }
        //    public string SDT {  get; set; }
        //    public string Email {  get; set; }
        //    public string DiaChi { get; set; }
        //}
    }
}
