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
    public class DuyetViewModel : BaseViewModel
    {
        public DuyetViewModel()
        {
            RefreshCommand = new RelayCommand<object>(Refresh);
            KiemTraTonKhoCommand = new RelayCommand<object>(KiemTraTonKho);
            DuyetPNCommand = new RelayCommand<object>(DuyetPN);
            DuyetPXCommand = new RelayCommand<object>(DuyetPX);
            TuChoiPNCommand = new RelayCommand<object> (TuChoiPN);
            TuChoiPXCommand = new RelayCommand<object>(TuChoiPX);

            Refresh();
        }
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã phiếu", "Mã nhân viên", "Mã khách hàng", "Mã nhà cung cấp", "Ngày lập phiếu", "Kho", "Trạng thái" };
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
        #region DanhSachPhieu
        private ObservableCollection<GoodsReceivedNote> _danhSachPhieuNhap = new ObservableCollection<GoodsReceivedNote>();
        public ObservableCollection<GoodsReceivedNote> DanhSachPhieuNhap
        {
            get { return _danhSachPhieuNhap; }
            set { _danhSachPhieuNhap = value; }
        }
        private GoodsReceivedNote _selectedPhieuNhap;
        public GoodsReceivedNote SelectedPhieuNhap
        {
            get { return _selectedPhieuNhap; }
            set { _selectedPhieuNhap = value; OnPropertyChanged(); }
        }
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
        public ICommand RefreshCommand {  get; set; }
        void Refresh(object t = null)
        {
            DanhSachPhieuNhap.Clear(); DanhSachPhieuXuat.Clear();
            var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.ToList();
            foreach (var item in ListPNFromDB)
            {
                if (item.DaXoa == false){ DanhSachPhieuNhap.Add(item); }
            }
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.ToList();
            foreach (var item in ListPXFromDB)
            {
                if (item.DaXoa == false) { DanhSachPhieuXuat.Add(item); }
            }
        }
        public ICommand KiemTraTonKhoCommand { get; set; }
        void KiemTraTonKho(object t = null)
        {
            DanhSachPhieuNhap.Clear(); DanhSachPhieuXuat.Clear();
            var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.ToList();
            foreach(var item in ListPNFromDB)
            {
                if(item.DaXoa == false)
                {
                    //Duyệt qua Infos, từng mặt hàng, nếu số lượng > Tồn kho => Cập nhật item.TrangThai = "Thiếu hàng". Nhớ SaveChanges()
                    DanhSachPhieuNhap.Add(item);
                }
            }
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.ToList();
            foreach(var item in ListPXFromDB)
            {
                if(item.DaXoa == false)
                {
                    //Duyệt qua Infos, từng mặt hàng, nếu số lượng > Tồn kho => Cập nhật item.TrangThai = "Thiếu hàng". Nhớ SaveChanges()
                    DanhSachPhieuXuat.Add(item);
                }
            }
        }
        public ICommand DuyetPNCommand { get; set; }
        void DuyetPN(object t)
        {
            //Nếu User đang là "Quản Lý", Nếu SelectedPhieuNhap.TrangThai == "Kế toán đã duyệt" thì mới được duyệt, không thì showdialog "Chờ kế toán xác nhận"
            //Nếu User đang là "Kế Toán" => Duyệt qua danh sách phiếu nhập, nếu MaPn == MaPn => Cập nhật trạng thái = "Kế toán đã duyệt", cập nhật tồn kho theo Infos của phiếu nhập
            MessageBox.Show("DuyetPNCommand");
            Refresh();
        }
        public ICommand DuyetPXCommand {  get; set; }
        void DuyetPX(object t)
        {
            //Nếu User đang là "Quản Lý", Nếu SelectedPhieuNhap.TrangThai == "Kế toán đã duyệt" thì mới được duyệt, cập nhật trạng thái, không thì showdialog "Chờ kế toán xác nhận"
            //Nếu User đang là "Kế Toán" => Duyệt qua danh sách phiếu xuất, nếu MaPx == MaPx => Cập nhật trạng thái = "Kế toán đã duyệt", cập nhật tồn kho theo Infos của phiếu xuất
            MessageBox.Show("DuyetPXCommand");
            Refresh();
        }
        public ICommand TuChoiPNCommand { get; set; }
        void TuChoiPN(object t)
        {
            //Nếu User là "Kế Toán" => Cập nhật trạng thái
            //Nếu User là "Quản Lý" => Duyệt qua Infos Phiếu Nhập, cộng lại sản phẩm trong phiếu nhập vào kho, cập nhật trạng thái
            MessageBox.Show("TuChoiPN Command");
            Refresh();
        }
        public ICommand TuChoiPXCommand {  get; set; }
        void TuChoiPX(object t)
        {
            //Nếu User là "Kế Toán" => Cập nhật trạng thái
            //Nếu User là "Quản Lý" => Duyệt qua Infos Phiếu Xuất, cộng lại sản phẩm trong phiếu xuất vào kho, cập nhật trạng thái
            MessageBox.Show("TuChoiPX Command");
            Refresh();
        }
        #endregion
    }
}
