using PMQuanLyVatTu.CustomControls;
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
            RefreshCommand = new RelayCommand<object>(Refresh);
            EditButtonCommand = new RelayCommand<object>(EditButton);
            Refresh();
        }
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() {"Không", "Mã vật tư", "Tên vật tư", "Loại vật tư", "Đơn vị tính", "Mã nhà cung cấp", "Mã kho chứa"};
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
        #endregion
        #region SearchBar
        private string _selectedSearchFilter;
        public string SelectedSearchFilter
        {
            get { return _selectedSearchFilter; }
            set { _selectedSearchFilter = value; OnPropertyChanged(); }
        }
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachVatTu
        private ObservableCollection<Supply> _danhSachVatTu = new ObservableCollection<Supply>();
        public ObservableCollection<Supply> DanhSachVatTu
        {
            get { return _danhSachVatTu; }
            set { _danhSachVatTu = value;}
        }
        private ObservableCollection<VatTuDisplayer> _danhSachVatTuLargeIcon = new ObservableCollection<VatTuDisplayer>();
        public ObservableCollection<VatTuDisplayer> DanhSachVatTuLargeIcon
        {
            get{ return _danhSachVatTuLargeIcon; }
            set{ _danhSachVatTuLargeIcon = value; }
        }
        private Supply _selectedVatTu;
        public Supply SelectedVatTu
        {
            get { return _selectedVatTu; }
            set { _selectedVatTu = value; OnPropertyChanged(); EditButton(); }
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
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t=null)
        {
            //Đổ dữ liệu, bao gồm DanhSachVatTu (danh sách chi tiết) và DanhSachVatTuLargeIcon (danh sách thu gọn, dùng ds tạm temp)
            DanhSachVatTu.Clear();

            DanhSachVatTu.Add(new Supply() { MaVT = "VT0001", TenVatTu = "Gạch 529", LoaiVatTu = "NVL", DonViTinh = "Viên", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/brick.jpg" });
            DanhSachVatTu.Add(new Supply() { MaVT = "VT0002", TenVatTu = "Búa", LoaiVatTu = "CC", DonViTinh = "Chiếc", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/hammer.jpg" });
            DanhSachVatTu.Add(new Supply() { MaVT = "VT0003", TenVatTu = "Máy khoan Mĩ Kim", LoaiVatTu = "TB", DonViTinh = "Máy", MaNCC = "NCC0002", MaKho = "KHO0001", GiaNhap = 9500, GiaXuat = 11000, SoLuongTonKho = 123, ImageLocation = "/Material/Images/Supplies/drill.jpg" });

            ObservableCollection<VatTuDisplayer> temp = new ObservableCollection<VatTuDisplayer>();
            foreach (Supply sup in DanhSachVatTu)
            {
                VatTuDisplayer newButton = new VatTuDisplayer();
                newButton.DataContext = sup; newButton.Margin = new Thickness(10, 10, 10, 10); newButton.Height = 300; newButton.Width = 220; newButton.Click += VatTuDisplayerButtonClick;
                temp.Add(newButton); 
            }

            DanhSachVatTuLargeIcon = temp;
        }
        public ICommand EditButtonCommand {  get; set; }
        void EditButton(object t=null)
        {
            string s = SelectedVatTu.MaVT;
            ThongTinVatTuWindowViewModel TTVTVM = new ThongTinVatTuWindowViewModel(s);
            ThongTinVatTuWindow AddWindow = new ThongTinVatTuWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
            Refresh();
        }
        #endregion
        #region Function
        void VatTuDisplayerButtonClick(object sender, RoutedEventArgs e)
        {
            string mavt = ((sender as VatTuDisplayer).DataContext as Supply).MaVT;
            ThongTinVatTuWindowViewModel TTVTVM = new ThongTinVatTuWindowViewModel(mavt);
            ThongTinVatTuWindow AddWindow = new ThongTinVatTuWindow();
            AddWindow.DataContext = TTVTVM;
            AddWindow.ShowDialog();
        }
        #endregion
    }
    public class Supply
    {
        public bool Checked { get; set; }
        public string MaVT { get; set; }
        public string TenVatTu { get; set; }
        public string LoaiVatTu { get; set; }
        public string DonViTinh { get; set; }
        public string MaNCC { get; set; }
        public string MaKho { get; set; }
        public int GiaNhap { get; set; }
        public int GiaXuat { get; set; }
        public int SoLuongTonKho { get; set; }
        public string ImageLocation {  get; set; }
    }
}
