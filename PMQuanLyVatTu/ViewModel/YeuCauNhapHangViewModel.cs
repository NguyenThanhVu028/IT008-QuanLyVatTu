using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

            var ListFromDB = DataProvider.Instance.DB.ImportRequests.ToList();
            foreach( var item in ListFromDB)
            {
                DanhSachYeuCauNhap.Add(item);
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa yêu cầu đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                //Xóa trong database
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
            MessageBox.Show("Đã tiếp nhận");
        }
        public ICommand TuChoiCommand { get; set; }
        void TuChoi(object t)
        {
            MessageBox.Show("Từ chối");
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
