using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
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
        #region User
        CurrentUser _user = CurrentUser.Instance;
        public CurrentUser User
        {
            get { return _user; }
        }
        #endregion
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
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); }
        }
        private string _searchString = "";
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

            var ListFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false).ToList();
            if(ListFromDB != null)
            {
                foreach(var item in ListFromDB)
                {
                    switch (SelectedSearchFilter)
                    {
                        case "Mã phiếu xuất":
                            if (item.MaPx != null)
                                if (item.MaPx.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuXuat.Add(item);
                            break;
                        case "Mã nhân viên":
                            if (item.MaNv != null)
                                if (item.MaNv.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuXuat.Add(item);
                            break;
                        case "Mã khách hàng":
                            if (item.MaKh != null)
                                if (item.MaKh.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuXuat.Add(item);
                            break;
                        case "Ngày lập phiếu":
                            if (item.NgayLap != null)
                                if (item.NgayLap.ToString().Contains(SearchString)) DanhSachPhieuXuat.Add(item);
                            break;
                        case "Kho xuất":
                            if (item.KhoXuat != null)
                                if (item.KhoXuat.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuXuat.Add(item);
                            break;
                        case "Trạng thái":
                            if (item.TrangThai != null)
                                if (item.TrangThai.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuXuat.Add(item);
                            break;
                        default:
                            DanhSachPhieuXuat.Add(item);
                            break;

                    }
                }
                
            }
        }
        public ICommand EditButtonCommand {  get; set; }
        void EditButton(object t)
        {
            if (SelectedPhieuXuat.TrangThai == "Kế toán đã duyệt" || SelectedPhieuXuat.TrangThai == "Đã duyệt")
            {
                CustomMessage msg3 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng không chỉnh sửa phiếu đã được duyệt!", false);
                msg3.ShowDialog();
                return;
            }
            if (SelectedPhieuXuat.TrangThai == "Bị từ chối")
            {
                CustomMessage msg3 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng không chỉnh sửa phiếu đã bị từ chối!", false);
                msg3.ShowDialog();
                return;
            }

            ChiTietPhieuXuatWindow AddWin = new ChiTietPhieuXuatWindow();
            ChiTietPhieuXuatWindowViewModel VM = new ChiTietPhieuXuatWindowViewModel(SelectedPhieuXuat.MaPx);
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa phiếu xuất đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var PhieuXuat = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(SelectedPhieuXuat.MaPx);
                if(PhieuXuat == null)
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy nhà cung cấp để xóa!", false);
                    msg.ShowDialog();
                }
                else
                {
                    PhieuXuat.DaXoa = true;
                    PhieuXuat.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "THÔNG BÁO", "Xóa phiếu xuất thành công.");
                    msg1.ShowDialog();
                }
            }
            Refresh();
        }
        #endregion
    }
}
