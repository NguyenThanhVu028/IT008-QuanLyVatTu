using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static PMQuanLyVatTu.ViewModel.NhanVienViewModel;

namespace PMQuanLyVatTu.ViewModel
{
    public class YeuCauXuatHangViewModel : BaseViewModel
    {
        public YeuCauXuatHangViewModel()
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            KiemTraTonKhoCommand = new RelayCommand<object>(KiemTraTonKho);
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
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã yêu cầu xuất", "Mã nhân viên", "Mã khách hàng", "Ngày lập yêu cầu", "Trạng thái" };
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
        #region DanhSachYeuCauXuatHang
        private ObservableCollection<ExportRequest> _danhSachYeuCauXuat = new ObservableCollection<ExportRequest>();
        public ObservableCollection<ExportRequest> DanhSachYeuCauXuat
        {
            get { return _danhSachYeuCauXuat; }
            set { _danhSachYeuCauXuat = value; }
        }
        private ExportRequest _selectedYeuCauXuat;
        public ExportRequest SelectedYeuCauXuat
        {
            get { return _selectedYeuCauXuat; }
            set { _selectedYeuCauXuat = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand AddCommand {  get; set; }
        void Add(object t)
        {
            ThongTinYeuCauXuatHangWindow AddWin = new ThongTinYeuCauXuatHangWindow();
            ThongTinYeuCauXuatHangWindowViewModel VM = new ThongTinYeuCauXuatHangWindowViewModel();
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand {  get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("ycx");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand KiemTraTonKhoCommand { get; set; }
        void KiemTraTonKho(object t)
        {
            DanhSachYeuCauXuat.Clear();
            var ListFromDB = DataProvider.Instance.DB.ExportRequests.ToList();
            foreach (var list in ListFromDB)
            {
                //Duyệt từng sản phẩm trong ExportRequestInfo của list.MaYcx, nếu có hàng không đủ => Duyệt qua ExportResquests, Cập nhật trạng thái của list, TrangThai = "Thiếu hàng"
                //Nhớ SaveChanges()
                DanhSachYeuCauXuat.Add(list);
            }
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachYeuCauXuat.Clear();

            var ListFromDB = DataProvider.Instance.DB.ExportRequests.ToList();
            foreach( var list in ListFromDB)
            {
                DanhSachYeuCauXuat.Add(list);
            }
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t)
        {
            string s = SelectedYeuCauXuat.MaYcx;
            ThongTinYeuCauXuatHangWindowViewModel TTVTVM = new ThongTinYeuCauXuatHangWindowViewModel(s);
            ThongTinYeuCauXuatHangWindow AddWindow = new ThongTinYeuCauXuatHangWindow();
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
                DanhSachYeuCauXuat.Remove(SelectedYeuCauXuat);
            }
            //Xóa trong database
            //Refresh();
        }
        //public ICommand DeleteSelectedCommand {  get; set; }
        //void DeleteSelected(object t)
        //{
        //    int Count = 0;
        //    CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
        //    msg.ShowDialog();
        //    if (msg.ReturnValue == true)
        //    {
        //        foreach (ExportRequest i in DanhSachYeuCauXuat)
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
    //public class ExportRequest
    //{
    //    public bool Checked { get; set; }
    //    public string MaYCX { get; set; }
    //    public string MaNV { get; set; }
    //    public string MaKH { get; set; }
    //    public string MaKho {  get; set; }
    //    public string NgayLap { get; set; }
    //    public string GhiChu { get; set; }
    //    public string TrangThai { get; set; }
    //}
}
