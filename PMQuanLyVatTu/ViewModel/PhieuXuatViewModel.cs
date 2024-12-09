using PMQuanLyVatTu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    internal class PhieuXuatViewModel : BaseViewModel
    {
        public PhieuXuatViewModel() 
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);

            Refresh();
        }
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã phiếu xuất", "Mã nhân viên", "Mã khách hàng", "Ngày lập phiếu", "Kho xuất", "Trạng thái" };
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
        #region DanhSachPhieuXuat
        private ObservableCollection<GoodsDeliveryNote> _danhSachPhieuXuat = new ObservableCollection<GoodsDeliveryNote>();
        public ObservableCollection<GoodsDeliveryNote> DanhSachPhieuXuat
        {
            get { return _danhSachPhieuXuat; }
            set { _danhSachPhieuXuat = value; }
        }
        private GoodsDeliveryNote _selectedPhieuXuat;
        public GoodsDeliveryNote SelectedPhieuXuat
        {
            get { return _selectedPhieuXuat; }
            set { _selectedPhieuXuat = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand {  get; set; }
        void Add(object t)
        {
            ChiTietPhieuXuatWindow newWin = new ChiTietPhieuXuatWindow();
            ChiTietPhieuXuatWindowViewModel VM = new ChiTietPhieuXuatWindowViewModel();
            newWin.DataContext = VM;
            newWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand {  get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow newWin = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("px");
            newWin.DataContext = VM;
            newWin.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachPhieuXuat.Clear();

            var ListFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.ToList();
            foreach (var item in ListFromDB)
            {
                if(item.DaXoa == false) { DanhSachPhieuXuat.Add(item); }
            }
        }
        public ICommand EditButtonCommand {  get; set; }
        void EditButton(object t)
        {

        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {

        }
        #endregion
    }
}
