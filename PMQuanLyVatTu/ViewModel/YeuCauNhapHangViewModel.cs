using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using static PMQuanLyVatTu.ViewModel.YeuCauXuatHangViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class YeuCauNhapHangViewModel : BaseViewModel
    {
        public YeuCauNhapHangViewModel() 
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
            //DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DaTiepNhanCommand = new RelayCommand<object>(DaTiepNhan);
            TuChoiCommand = new RelayCommand<object>(TuChoi);
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
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã yêu cầu nhập", "Mã nhân viên", "Mã vật tư", "Ngày lập yêu cầu", "Trạng thái" };
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
        #region DanhSachYeuCauNhapHang
        private ObservableCollection<ImportRequest> _danhSachYeuCauNhap = new ObservableCollection<ImportRequest>();
        public ObservableCollection<ImportRequest> DanhSachYeuCauNhap
        {
            get { return _danhSachYeuCauNhap; }
            set { _danhSachYeuCauNhap = value; }
        }
        private ImportRequest _selectedYeuCauNhap;
        public ImportRequest SelectedYeuCauNhap
        {
            get { return _selectedYeuCauNhap; }
            set { _selectedYeuCauNhap = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            ThongTinYeuCauNhapHangWindow AddWin = new ThongTinYeuCauNhapHangWindow();
            ThongTinYeuCauNhapHangWindowViewModel VM = new ThongTinYeuCauNhapHangWindowViewModel();
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t) 
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("ycn");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand {  get; set; }
        void Refresh(object t = null)
        {
            DanhSachYeuCauNhap.Clear();

            var ListFromDB = DataProvider.Instance.DB.ImportRequests.Where(c => c.DaXoa == false).ToList();
            foreach ( var item in ListFromDB)
            {
                switch (SelectedSearchFilter)
                {
                    case "Mã yêu cầu nhập":
                        if (item.MaYcn != null)
                            if (item.MaYcn.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauNhap.Add(item);
                        break;
                    case "Mã nhân viên":
                        if (item.MaNv != null)
                            if (item.MaNv.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauNhap.Add(item);
                        break;
                    case "Mã vật tư":
                        if (item.MaVt != null)
                            if (item.MaVt.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauNhap.Add(item);
                        break;
                    case "Ngày lập yêu cầu":
                        if (item.NgayLap != null)
                            if (item.NgayLap.Value.ToString("ddd/dd/MM/yyyy").ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauNhap.Add(item);
                        break;
                    case "Trạng thái":
                        if (item.TrangThai != null)
                            if (item.TrangThai.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauNhap.Add(item);
                        break;
                    default:
                        DanhSachYeuCauNhap.Add(item);
                        break;
                }
            }
            //DanhSachYeuCauNhap.Add(new ImportRequest() { Checked = true, MaYCN = "YCN0001", MaNV = "NV0012", MaVT = "VT0008", SoLuong = 15, NgayLap = "Friday/03/12/2024", GhiChu = "Nhập hàng trong tuần này.", TrangThai = "Đã tiếp nhận" });
            //DanhSachYeuCauNhap.Add(new ImportRequest() { Checked = true, MaYCN = "YCN0002", MaNV = "NV0010", MaVT = "VT0002", SoLuong = 30, NgayLap = "Friday/03/12/2024", GhiChu = "Nhập hàng trong tuần này.", TrangThai = "Chờ tiếp nhận" });
            //DanhSachYeuCauNhap.Add(new ImportRequest() { Checked = true, MaYCN = "YCN0003", MaNV = "NV0005", MaVT = "VT0001", SoLuong = 15, NgayLap = "Friday/03/12/2024", GhiChu = "Nhập hàng trong tuần này.", TrangThai = "Đã bị từ chối" });
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t)
        {
            string s = SelectedYeuCauNhap.MaYcn;
            ThongTinYeuCauNhapHangWindowViewModel TTVTVM = new ThongTinYeuCauNhapHangWindowViewModel(s);
            ThongTinYeuCauNhapHangWindow AddWindow = new ThongTinYeuCauNhapHangWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa yêu cầu nhập đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var YCN = DataProvider.Instance.DB.ImportRequests.Find(SelectedYeuCauNhap.MaYcn);

                if (YCN != null)
                {
                    YCN.DaXoa = true;
                    YCN.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu nhập để xóa!", false);
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
        //        foreach (ImportRequest i in DanhSachYeuCauNhap)
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
        public ICommand DaTiepNhanCommand { get; set; }
        void DaTiepNhan(object t)
        {
            var YCN = DataProvider.Instance.DB.ImportRequests.Find(SelectedYeuCauNhap);

            if (YCN != null)
            {
                YCN.TrangThai = "Đã tiếp nhận";
                DataProvider.Instance.DB.SaveChanges();
            }
            else
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu nhập!", false);
                msg.ShowDialog();
            }
        }
        public ICommand TuChoiCommand { get; set; }
        void TuChoi(object t)
        {
            var YCN = DataProvider.Instance.DB.ImportRequests.Find(SelectedYeuCauNhap);

            if (YCN != null)
            {
                YCN.TrangThai = "Bị từ chối";
                DataProvider.Instance.DB.SaveChanges();
            }
            else
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu nhập!", false);
            }
        }
        #endregion
    }
    //public class ImportRequest
    //{
    //    public bool Checked { get; set; }
    //    public string MaYCN { get; set; }
    //    public string MaNV { get; set; }
    //    public string MaVT { get; set; }
    //    public string NgayLap { get; set; }
    //    public int SoLuong { get; set; }
    //    public string GhiChu { get; set; }
    //    public string TrangThai { get; set; }
    //}
}
