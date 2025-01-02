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
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); }
        }
        private string _searchString = "";
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
            bool IsSufficient = true;
            var ListFromDB = DataProvider.Instance.DB.ExportRequests.Where(c => c.DaXoa == false && (c.TrangThai == "Thiếu hàng" || c.TrangThai == "Chưa duyệt")).ToList();
            if (ListFromDB == null) return;
            foreach (var list in ListFromDB)
            {
                if (list.GhiChu == null) list.GhiChu = "";
                List<ExportRequestInfo> request = DataProvider.Instance.DB.ExportRequestInfos.Where(c => c.MaYcx == list.MaYcx).ToList();
                if(request == null)
                {
                    continue;
                }
                foreach (var record in request)
                {
                    var Sp = DataProvider.Instance.DB.Supplies.Find(record.MaVt);
                    if (Sp == null || Sp.DaXoa == true)
                    {
                        list.GhiChu += "/*" + Sp.MaVt + " không còn tốn tại";
                        IsSufficient = false;
                    }
                    else if(Sp.SoLuongTonKho < record.SoLuong)
                    {
                        list.GhiChu += "/*" + Sp.MaVt + " thiếu: " + (record.SoLuong - Sp.SoLuongTonKho).ToString();
                        IsSufficient = false;
                    }
                }
                if (IsSufficient)
                {
                    if(list.TrangThai == "Thiếu hàng")
                    {
                        list.TrangThai = "Chưa duyệt";
                        for(int i=0; i<list.GhiChu.Length-1; i++)
                        {
                            if (list.GhiChu[i] == '/' && list.GhiChu[i + 1] == '*')
                                list.GhiChu.Remove(i, list.GhiChu.Length - i);
                        }
                    }
                }
                else
                {
                    if(list.TrangThai == "Chưa duyệt")
                    {
                        list.TrangThai = "Thiếu hàng";
                    }
                }
                //Duyệt từng sản phẩm trong ExportRequestInfo của list.MaYcx, nếu có hàng không đủ => Duyệt qua ExportResquests, Cập nhật trạng thái của list, TrangThai = "Thiếu hàng"
                //Nhớ SaveChanges()
                DataProvider.Instance.DB.SaveChanges();
                Refresh();
            }
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachYeuCauXuat.Clear();

            var ListFromDB = DataProvider.Instance.DB.ExportRequests.Where(c => c.DaXoa == false).ToList();
            foreach( var item in ListFromDB)
            {
                switch (SelectedSearchFilter)
                {
                    case "Mã yêu cầu xuất":
                        if (item.MaYcx != null)
                            if (item.MaYcx.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauXuat.Add(item);
                        break;
                    case "Mã nhân viên":
                        if (item.MaNv != null)
                            if (item.MaNv.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauXuat.Add(item);
                        break;
                    case "Mã khách hàng":
                        if (item.MaKh != null)
                            if (item.MaKh.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauXuat.Add(item);
                        break;
                    case "Ngày lập yêu cầu":
                        if (item.NgayLap != null)
                            if (item.NgayLap.Value.ToString("ddd/dd/MM/yyyy").ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauXuat.Add(item);
                        break;
                    case "Trạng thái":
                        if (item.TrangThai != null)
                            if (item.TrangThai.ToLower().Contains(SearchString.ToLower())) DanhSachYeuCauXuat.Add(item);
                        break;
                    default:
                        DanhSachYeuCauXuat.Add(item);
                        break;
                }
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa yêu cầu xuất đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var YCX = DataProvider.Instance.DB.ExportRequests.Find(SelectedYeuCauXuat.MaYcx);

                if (YCX != null)
                {
                    YCX.DaXoa = true;
                    YCX.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu xuất để xóa!", false);
                }
            }
            Refresh();
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
            var YCX = DataProvider.Instance.DB.ExportRequests.Find(SelectedYeuCauXuat.MaYcx);

            if (YCX != null)
            {
                YCX.TrangThai = "Đã tiếp nhận";
                DataProvider.Instance.DB.SaveChanges();
            }
            else
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu xuất!", false);
                msg.ShowDialog();
            }
            Refresh();
        }
        public ICommand TuChoiCommand { get; set; }
        void TuChoi(object t)
        {
            var YCX = DataProvider.Instance.DB.ExportRequests.Find(SelectedYeuCauXuat.MaYcx);

            if (YCX != null)
            {
                YCX.TrangThai = "Bị từ chối";
                DataProvider.Instance.DB.SaveChanges();
            }
            else
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu xuất!", false);
                msg.ShowDialog();
            }
            Refresh();
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
