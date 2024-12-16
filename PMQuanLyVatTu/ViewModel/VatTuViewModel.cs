using PMQuanLyVatTu.CustomControls;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class VatTuViewModel:BaseViewModel
    {
        public VatTuViewModel() {
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
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() {"Mã vật tư", "Tên vật tư", "Loại vật tư", "Đơn vị tính", "Mã nhà cung cấp", "Mã kho chứa"};
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); Refresh(); }
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
            set { _searchString = value; OnPropertyChanged(); Refresh();}
        }
        #endregion
        #region DanhSachVatTu
        private ObservableCollection<Supply> _danhSachVatTu = new ObservableCollection<Supply>();
        public ObservableCollection<Supply> DanhSachVatTu
        {
            get { return _danhSachVatTu; }
            set { _danhSachVatTu = value;}
        }
        public ObservableCollection<VatTuDisplayer> _tempDS = new ObservableCollection<VatTuDisplayer>();
        private ObservableCollection<VatTuDisplayer> _danhSachVatTuLargeIcon;
        public ObservableCollection<VatTuDisplayer> DanhSachVatTuLargeIcon
        {
            get{ return _danhSachVatTuLargeIcon; }
            set{ _danhSachVatTuLargeIcon = value; }
        }
        private Supply _selectedVatTu;
        public Supply SelectedVatTu
        {
            get { return _selectedVatTu; }
            set { _selectedVatTu = value; OnPropertyChanged(); /*EditButton();*/ }
        }
        #endregion
        #region Command
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            ThongTinVatTuWindowViewModel TTVTVM = new ThongTinVatTuWindowViewModel();
            ThongTinVatTuWindow AddWindow = new ThongTinVatTuWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh(); 
        }
        public ICommand XoaGanDayCommand { get; set; }
        void XoaGanDay(object t)
        {
            XoaGanDayWindow xoaGanDayWindow = new XoaGanDayWindow();
            XoaGanDayWindowViewModel VM = new XoaGanDayWindowViewModel("vt");
            xoaGanDayWindow.DataContext = VM;
            xoaGanDayWindow.ShowDialog();
            Refresh();
        }
        public ICommand RefreshCommand { get; set; }
        //void Refresh(object t=null)
        //{
        //    //Đổ dữ liệu, bao gồm DanhSachVatTu (danh sách chi tiết) và DanhSachVatTuLargeIcon (danh sách thu gọn, dùng ds tạm temp)
        //    DanhSachVatTu.Clear();

        //    DanhSachVatTu.Add(new Supply() { MaVT = "VT0001", TenVatTu = "Gạch 529", LoaiVatTu = "NVL", DonViTinh = "Viên", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/brick.jpg" });
        //    DanhSachVatTu.Add(new Supply() { MaVT = "VT0002", TenVatTu = "Búa", LoaiVatTu = "CC", DonViTinh = "Chiếc", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/hammer.jpg" });
        //    DanhSachVatTu.Add(new Supply() { MaVT = "VT0003", TenVatTu = "Máy khoan Mĩ Kim", LoaiVatTu = "TB", DonViTinh = "Máy", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/drill.jpg" });

        //    ObservableCollection<VatTuDisplayer> temp = new ObservableCollection<VatTuDisplayer>();
        //    foreach (Supply sup in DanhSachVatTu)
        //    {
        //        VatTuDisplayer newButton = new VatTuDisplayer();
        //        newButton.DataContext = sup; newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 300; newButton.Width = 220; newButton.Click += VatTuDisplayerButtonClick;
        //        temp.Add(newButton); 
        //    }

        //    DanhSachVatTuLargeIcon = temp;
        //}
        void Refresh(object t = null)
        {
            DanhSachVatTu.Clear(); DanhSachVatTuLargeIcon = null;
            var ListFromDB = DataProvider.Instance.DB.Supplies.Where(c=>c.DaXoa == false).ToList();
            if(ListFromDB != null) {
                foreach ( var item in ListFromDB)
                {
                    switch (SelectedSearchFilter)
                    {
                        case "Mã vật tư":
                            if(item.MaVt != null)
                                if(item.MaVt.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        case "Tên vật tư":
                            if (item.TenVatTu != null)
                                if (item.TenVatTu.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        case "Loại vật tư":
                            if (item.LoaiVatTu != null)
                                if (item.LoaiVatTu.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        case "Đơn vị tính":
                            if (item.DonViTinh != null)
                                if (item.DonViTinh.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        case "Mã nhà cung cấp":
                            if (item.MaNcc != null)
                                if (item.MaNcc.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        case "Mã kho chứa":
                            if (item.MaKho != null)
                                if (item.MaKho.ToLower().Contains(SearchString.ToLower())) DanhSachVatTu.Add(item);
                            break;
                        default:
                            DanhSachVatTu.Add(item);
                            break;
                    }
                }
            }
            //Phần dành cho LargeIcon
            if(_tempDS.Count() < DanhSachVatTu.Count())
            {
                int temp1 = DanhSachVatTu.Count(), temp2 = _tempDS.Count();
                for (int i=0; i< temp1 - temp2; i++)
                {
                    VatTuDisplayer newButton = new VatTuDisplayer();
                    newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 290; newButton.Width = 210; newButton.Click += VatTuDisplayerButtonClick;
                    _tempDS.Add(newButton);
                }
            }
            for (int i=0; i<DanhSachVatTu.Count(); i++)
            {
                _tempDS[i].Visibility = Visibility.Visible;
                _tempDS[i].DataContext = null;
                _tempDS[i].DataContext = DanhSachVatTu[i];
            }
            for(int i=DanhSachVatTu.Count(); i<_tempDS.Count(); i++) _tempDS[i].Visibility = Visibility.Collapsed;
            DanhSachVatTuLargeIcon = _tempDS;
        }
        public ICommand EditButtonCommand {  get; set; }
        void EditButton(object t=null)
        {
            string s = SelectedVatTu.MaVt;
            ThongTinVatTuWindowViewModel TTVTVM = new ThongTinVatTuWindowViewModel(s);
            ThongTinVatTuWindow AddWindow = new ThongTinVatTuWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        public ICommand DeleteButtonCommand {  get; set; }
        void DeleteButton(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa vật tư đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                //Xóa trong database
                var vt = DataProvider.Instance.DB.Supplies.Find(SelectedVatTu.MaVt);
                if (vt != null)
                {
                    vt.DaXoa = true;
                    vt.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "THÔNG BÁO", "Xóa vật tư thành công.");
                    msg1.ShowDialog();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy vật tư để xóa!", false);
                    msg.ShowDialog();
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
        //        foreach (Supply i in DanhSachVatTu)
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
        #endregion
        #region Function
        void VatTuDisplayerButtonClick(object sender, RoutedEventArgs e)
        {
            if(CurrentUser.Instance.ChucVu == "Quản Lý" || CurrentUser.Instance.ChucVu == "Kế Toán")
            {
                string mavt = ((sender as VatTuDisplayer).DataContext as Supply).MaVt;
                ThongTinVatTuWindowViewModel TTVTVM = new ThongTinVatTuWindowViewModel(mavt);
                ThongTinVatTuWindow AddWindow = new ThongTinVatTuWindow();
                AddWindow.DataContext = TTVTVM;
                AddWindow.ShowDialog();
                Refresh();
            }

        }
        #endregion
        //public class Supply
        //{
        //    public bool Checked { get; set; }
        //    public string MaVT { get; set; }
        //    public string TenVatTu { get; set; }
        //    public string LoaiVatTu { get; set; }
        //    public string DonViTinh { get; set; }
        //    public string MaNCC { get; set; }
        //    public string MaKho { get; set; }
        //    public int GiaNhap { get; set; }
        //    public int GiaXuat { get; set; }
        //    public int SoLuongTonKho { get; set; }
        //    public string ImageLocation { get; set; }
        //}
    }
}
