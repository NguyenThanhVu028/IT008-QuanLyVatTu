using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); }
        }
        private string _searchString = "";
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
            var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p=>p.DaXoa == false).ToList();
            if(ListPNFromDB != null)
            foreach (var item in ListPNFromDB)
                DanhSachPhieuNhap.Add(item);
         
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p=>p.DaXoa == false).ToList();
            if (ListPXFromDB != null)
                foreach (var item in ListPXFromDB)
                    DanhSachPhieuXuat.Add(item);
        }
        public ICommand KiemTraTonKhoCommand { get; set; }
        void KiemTraTonKho(object t = null)
        {
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p=>p.DaXoa == false && (p.TrangThai == "Chưa duyệt" || p.TrangThai == "Thiếu hàng")).ToList();
            if (ListPXFromDB == null) return;
            foreach (var PX in ListPXFromDB)
            {
                if (PX.TrangThai == null)
                {
                    PX.TrangThai = "Chưa duyệt"; continue;
                }

                bool Sufficient = true;

                var InnerList = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == PX.MaPx).ToList();
                if (InnerList == null) continue;
                foreach(var item in InnerList)
                {
                    var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                    if (supply == null || supply.DaXoa == true)
                    {
                        Sufficient = false;
                        PX.LyDoXuat += "/*" + item.MaVt + " không còn tồn lại";
                    }
                    else
                    {
                        if(item.SoLuong > supply.SoLuongTonKho)
                        {
                            Sufficient = false;
                            PX.LyDoXuat += "/*" + item.MaVt + " thiếu " + (item.SoLuong - supply.SoLuongTonKho).ToString();
                        }
                    }
                }
                if (Sufficient)
                {
                    if (PX.LyDoXuat == null) PX.LyDoXuat = "";
                    if(PX.TrangThai == "Thiếu hàng")
                    {
                        for(int i=0; i<PX.LyDoXuat.Length-1; i++)
                        {
                            if (PX.LyDoXuat[i] == '/' && PX.LyDoXuat[i+1] == '*')
                            {
                                PX.LyDoXuat = PX.LyDoXuat.Remove(i, PX.LyDoXuat.Length - i);
                                break;
                            }
                        }
                        PX.TrangThai = "Chưa duyệt";
                    }
                }
                else
                {
                    PX.TrangThai = "Thiếu hàng";
                }
            }
            DataProvider.Instance.DB.SaveChanges();
            Refresh();
        }
        public ICommand DuyetPNCommand { get; set; }
        void DuyetPN(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có xác nhận duyệt phiếu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == false) return;
            if (SelectedPhieuNhap == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu nhập đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            var PN = DataProvider.Instance.DB.GoodsReceivedNotes.Find(SelectedPhieuNhap.MaPn);
            if (PN == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu nhập đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            if (CurrentUser.Instance.ChucVu == "Quản Lý")
            {
                if(PN.TrangThai == "Chưa duyệt")
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Kế toán chưa duyệt phiếu nhập.", false);
                    msg1.ShowDialog();
                }
                else if(PN.TrangThai == "Kế toán đã duyệt")
                {
                    PN.TrangThai = "Đã duyệt";
                }
            }
            else if(CurrentUser.Instance.ChucVu == "Kế Toán")
            {
                if(PN.TrangThai == "Kế toán đã duyệt")
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu nhập chờ quản lý duyệt.", false);
                    msg1.ShowDialog();
                }
                else if(PN.TrangThai == "Chưa duyệt")
                {
                    var InnerList = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == PN.MaPn).ToList();
                    if(InnerList != null)
                    {
                        foreach(var item in InnerList)
                        {
                            var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                            if(supply == null)
                            {
                                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Đã có lỗi xảy ra, vui lòng kiểm tra mã vật tư.", false);
                                msg1.ShowDialog();
                                return;
                            }
                            supply.SoLuongTonKho += item.SoLuong;
                        }
                        PN.TrangThai = "Kế toán đã duyệt";
                    }
                }
            }
            DataProvider.Instance.DB.SaveChanges();
            Refresh();
        }
        public ICommand DuyetPXCommand {  get; set; }
        void DuyetPX(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có xác nhận duyệt phiếu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == false) return;
            if (SelectedPhieuXuat == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu xuất đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            var PX = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(SelectedPhieuXuat.MaPx);
            if (PX == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu xuất đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            if (CurrentUser.Instance.ChucVu == "Quản Lý")
            {
                if (PX.TrangThai == "Chưa duyệt")
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Kế toán chưa duyệt phiếu xuất.", false);
                    msg1.ShowDialog();
                }
                else if (PX.TrangThai == "Kế toán đã duyệt")
                {
                    PX.TrangThai = "Đã duyệt";
                }
            }
            else if (CurrentUser.Instance.ChucVu == "Kế Toán")
            {
                if (PX.TrangThai == "Kế toán đã duyệt")
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu xuất chờ quản lý duyệt.", false);
                    msg1.ShowDialog();
                }
                else if (PX.TrangThai == "Chưa duyệt")
                {
                    var InnerList = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == PX.MaPx).ToList();
                    if (InnerList != null)
                    {
                        foreach (var item in InnerList)
                        {
                            var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                            if (supply == null)
                            {
                                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Đã có lỗi xảy ra, vui lòng kiểm tra mã vật tư.", false);
                                msg1.ShowDialog();
                                return;
                            }
                            if(supply.SoLuongTonKho < item.SoLuong)
                            {
                                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Đã có lỗi xảy ra, vui lòng kiểm tra tồn kho.", false);
                                msg1.ShowDialog();
                                return;
                            }
                            supply.SoLuongTonKho -= item.SoLuong;
                        }
                        PX.TrangThai = "Kế toán đã duyệt";
                    }
                }
            }
            DataProvider.Instance.DB.SaveChanges();
            Refresh();
        }
        public ICommand TuChoiPNCommand { get; set; }
        void TuChoiPN(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có xác nhận từ chối phiếu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == false) return;
            if (SelectedPhieuNhap == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu nhập đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            var PN = DataProvider.Instance.DB.GoodsReceivedNotes.Find(SelectedPhieuNhap.MaPn);
            if (PN == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu nhập đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            if (PN.TrangThai == "Chưa duyệt") { PN.TrangThai = "Bị từ chối"; DataProvider.Instance.DB.SaveChanges(); }
            else if(PN.TrangThai == "Kế toán đã duyệt")
            {
                var InnerList = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaPn == PN.MaPn);
                if(InnerList != null)
                {
                    foreach(var item in InnerList)
                    {
                        var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                        if (supply != null) supply.SoLuongTonKho -= item.SoLuong;
                    }
                }
                PN.TrangThai = "Bị từ chối";
                DataProvider.Instance.DB.SaveChanges();
            }
            Refresh();
        }
        public ICommand TuChoiPXCommand {  get; set; }
        void TuChoiPX(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có xác nhận từ chối phiếu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == false) return;
            if (SelectedPhieuXuat == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu xuất đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            var PX = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(SelectedPhieuXuat.MaPx);
            if (PX == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Phiếu xuất đã không còn tồn tại trên hệ thống.", false);
                msg1.ShowDialog();
                return;
            }
            if (PX.TrangThai == "Chưa duyệt") { PX.TrangThai = "Bị từ chối"; DataProvider.Instance.DB.SaveChanges(); }
            else if (PX.TrangThai == "Kế toán đã duyệt")
            {
                var InnerList = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaPx == PX.MaPx);
                if (InnerList != null)
                {
                    foreach (var item in InnerList)
                    {
                        var supply = DataProvider.Instance.DB.Supplies.Find(item.MaVt);
                        if (supply != null) supply.SoLuongTonKho += item.SoLuong;
                    }
                }
                PX.TrangThai = "Bị từ chối";
                DataProvider.Instance.DB.SaveChanges();
            }
            Refresh();
        }
        #endregion
    }
}
