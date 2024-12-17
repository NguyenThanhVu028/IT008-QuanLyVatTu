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
    internal class PhieuNhapViewModel : BaseViewModel
    {
        public PhieuNhapViewModel()
        {
            AddCommand = new RelayCommand<object>(Add);
            XoaGanDayCommand = new RelayCommand<object>(XoaGanDay);
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
            //DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            Refresh();
        }
        #region User
        CurrentUser _user = CurrentUser.Instance;
        public CurrentUser User { get { return _user; } }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã phiếu nhập", "Mã nhân viên", "Mã nhà cung cấp", "Ngày lập phiếu", "Kho nhập", "Trạng thái" };
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
        #region DanhSachPhieuNhap
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
        #endregion
        #region Command
        public ICommand AddCommand {  get; set; }
        void Add(object t)
        {
            ChiTietPhieuNhapWindow AddWin = new ChiTietPhieuNhapWindow();
            ChiTietPhieuNhapWindowViewModel VM = new ChiTietPhieuNhapWindowViewModel();
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow newWin = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("pn");
            newWin.DataContext = VM;
            newWin.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {
            DanhSachPhieuNhap.Clear();
            var ListFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false).ToList();

            if (ListFromDB != null)
            {
                foreach(var item in ListFromDB)
                {
                    switch (SelectedSearchFilter)
                    {
                        case "Mã phiếu nhập":
                            if (item.MaPn != null)
                                if (item.MaPn.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuNhap.Add(item);
                            break;
                        case "Mã nhân viên":
                            if (item.MaNv != null)
                                if (item.MaNv.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuNhap.Add(item);
                            break;
                        case "Mã nhà cung cấp":
                            if (item.MaNcc != null)
                                if (item.MaNcc.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuNhap.Add(item);
                            break;
                        case "Ngày lập phiếu":
                            if (item.NgayLap != null)
                                if (item.NgayLap.ToString().Contains(SearchString)) DanhSachPhieuNhap.Add(item);
                            break;
                        case "Kho nhập":
                            if (item.KhoNhap != null)
                                if (item.KhoNhap.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuNhap.Add(item);
                            break;
                        case "Trạng thái":
                            if (item.TrangThai != null)
                                if (item.TrangThai.ToLower().Contains(SearchString.ToLower())) DanhSachPhieuNhap.Add(item);
                            break;
                        default:
                            DanhSachPhieuNhap.Add(item);
                            break;
                    }
                }
            }
        }
        public ICommand EditButtonCommand { get; set; }
        void EditButton(object t)
        {
            ChiTietPhieuNhapWindow AddWin = new ChiTietPhieuNhapWindow();
            ChiTietPhieuNhapWindowViewModel VM = new ChiTietPhieuNhapWindowViewModel(SelectedPhieuNhap.MaPn);
            AddWin.DataContext = VM;
            AddWin.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa phiếu nhập đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var PhieuNhap = DataProvider.Instance.DB.GoodsReceivedNotes.Find(SelectedPhieuNhap.MaPn);
                if(PhieuNhap == null)
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy phiếu để xóa!", false);
                    msg.ShowDialog();
                }
                else
                {
                    PhieuNhap.DaXoa = true;
                    PhieuNhap.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "THÔNG BÁO", "Xóa phiếu nhập thành công.");
                    msg1.ShowDialog();
                }
            }
            Refresh();
        }
        #endregion
        //public class GoodsReceivedNote
        //{
        //    public bool Checked { get; set; }
        //    public string MaPN { get; set; }
        //    public string MaNV {  get; set; }
        //    public string MaNCC { get; set; }
        //    public string NgayLap {  get; set; }
        //    public string KhoNhap {  get; set; }
        //    public string LyDoNhap {  get; set; }
        //    public double ChietSuat {  get; set; }
        //    public double VAT {  get; set; }
        //    public int TongGia { get; set; }
        //    public string TrangThai {  get; set; }
        //}
    }
}
