using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class KhoViewModel : BaseViewModel
    {
        public KhoViewModel()
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
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã kho", "Loại vật tư", "Địa chỉ" };
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
        #region DanhSachKho
        private ObservableCollection<Warehouse> _danhSachKho = new ObservableCollection<Warehouse>();
        public ObservableCollection<Warehouse> DanhSachKho
        {
            get { return _danhSachKho; }
            set { _danhSachKho = value; }
        }
        private Warehouse _selectedKho;
        public Warehouse SelectedKho
        {
            get { return _selectedKho; }
            set { _selectedKho = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            ThongTinKhoWindow AddWin = new ThongTinKhoWindow();
            ThongTinKhoWindowViewModel VM = new ThongTinKhoWindowViewModel();
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("kho");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand {  get; set; }
        void Refresh(object t = null)
        {
            DanhSachKho.Clear();

            var ListFromDB = DataProvider.Instance.DB.Warehouses.Where(c => c.DaXoa == false).ToList();
            if (ListFromDB != null)
            {
                foreach (var item in ListFromDB)
                {
                    switch (SelectedSearchFilter)
                    {
                        case "Mã kho":
                            if (item.MaKho != null)
                                if (item.MaKho.ToLower().Contains(SearchString.ToLower())) DanhSachKho.Add(item);
                            break;
                        case "Loại vật tư":
                            if (item.LoaiVatTu != null)
                                if (item.LoaiVatTu.ToLower().Contains(SearchString.ToLower())) DanhSachKho.Add(item);
                            break;
                        case "Địa chỉ":
                            if (item.DiaChi != null)
                                if (item.DiaChi.ToLower().Contains(SearchString.ToLower())) DanhSachKho.Add(item);
                            break;
                        default:
                            DanhSachKho.Add(item);
                            break;
                    }
                }
            }
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t)
        {
            string s = SelectedKho.MaKho;
            ThongTinKhoWindowViewModel TTVTVM = new ThongTinKhoWindowViewModel(s);
            ThongTinKhoWindow AddWindow = new ThongTinKhoWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa kho đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var Kho = DataProvider.Instance.DB.Warehouses.Find(SelectedKho.MaKho);

                if (Kho != null)
                {
                    Kho.DaXoa = true;
                    Kho.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                    msg = new CustomMessage("/Material/Images/Icons/success.png", "THÔNG BÁO", "Xóa kho thành công.");
                    msg.ShowDialog();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy kho để xóa!", false);
                }
            }
            Refresh();
        }
        #endregion
    }
}
