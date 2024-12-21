using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongKeViewModel : BaseViewModel
    {
        public ThongKeViewModel()
        {
            LoadYearList();
            LoadMonthLabels(); LoadYearLabels();
            LoadNhapMonthSeries(SelectedMonth, SelectedYear);
            LoadNhapYearSeries(SelectedYear);
            LoadXuatMonthSeries(SelectedMonth, SelectedYear);
            LoadXuatYearSeries(SelectedYear);
            LoadDoanhThuMonthSeries(SelectedMonth, SelectedYear);
            LoadDoanhThuYearSeries(SelectedYear);
            LoadDanhSachVatTuNhap(SelectedMonth, SelectedYear);
            LoadDanhSachVatTuXuat(SelectedMonth, SelectedYear);

            Formatter = value => value.ToString("N0");

            PrintCommand = new RelayCommand<Grid>(Print);

        }
        #region Info

        private bool _isTongQuan = false;
        public bool IsTongQuan { get { return _isTongQuan; } set { _isTongQuan = value; OnPropertyChanged(); FindBestVT(); FindTienNHTienXH(); LoadNhapXuatChart(); LoadPhieuXuatChart(); } }
        private bool _theoThang = true;
        public bool TheoThang
        {
            get { return _theoThang; }
            set { _theoThang = value; OnPropertyChanged();
                LoadDanhSachVatTuNhap(SelectedMonth, SelectedYear);
                LoadNhapMonthSeries(SelectedMonth, SelectedYear);
                LoadNhapYearSeries(SelectedYear);
                LoadXuatMonthSeries(SelectedMonth, SelectedYear);
                LoadXuatYearSeries(SelectedYear);
                LoadDoanhThuMonthSeries(SelectedMonth, SelectedYear);
                LoadDoanhThuYearSeries(SelectedYear);
                LoadDanhSachVatTuXuat(SelectedMonth, SelectedYear);

                FindBestVT();
            }
        }

        private string _bestVT = "";
        public string BestVT { get { return _bestVT; } set { _bestVT = value; OnPropertyChanged(); } }
        private int _bestSL = 0;
        public int BestSL { get { return _bestSL; } set { _bestSL = value; OnPropertyChanged(); } }
        private int _doanhThuNamNay = 0;
        public int DoanhThuNamNay { get { return _doanhThuNamNay; } set { _doanhThuNamNay = value; OnPropertyChanged(); } }
        private int _tienNhapHang = 0;
        public int TienNhapHang { get { return _tienNhapHang; } set { _tienNhapHang = value; OnPropertyChanged(); } }
        private int _tienXuatHang = 0;
        public int TienXuatHang { get { return _tienXuatHang; } set { _tienXuatHang = value; OnPropertyChanged(); } }

        #endregion
        #region DataForSelectionlist
        ObservableCollection<string> _monthList = new ObservableCollection<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
        string _selectedMonth = "Tháng " + DateTime.Now.Month.ToString();
        ObservableCollection<string> _yearList = new ObservableCollection<string>();
        string _selectedYear = "Năm " + DateTime.Now.Year.ToString();
        public ObservableCollection<string> MonthList
        {
            get { return _monthList; }
            set { _monthList = value; OnPropertyChanged(); }
        }
        public string SelectedMonth { get { return _selectedMonth; } set { _selectedMonth = value;
                OnPropertyChanged();
                LoadNhapMonthSeries(_selectedMonth, SelectedYear);
                LoadDanhSachVatTuNhap(_selectedMonth, SelectedYear);
                LoadDanhSachVatTuXuat(_selectedMonth, SelectedYear);
                LoadXuatMonthSeries(_selectedMonth, SelectedYear);
                LoadNhapYearSeries(SelectedYear);
                LoadXuatYearSeries(SelectedYear);
                LoadDoanhThuMonthSeries(_selectedMonth, SelectedYear);
                LoadDoanhThuYearSeries(SelectedYear);
                FindBestVT(); } }
        public ObservableCollection<string> YearList
        {
            get { return _yearList; }
            set { _yearList = value; OnPropertyChanged(); }
        }
        public string SelectedYear { get { return _selectedYear; } set { _selectedYear = value;
                OnPropertyChanged();
                LoadNhapMonthSeries(SelectedMonth, _selectedYear);
                LoadDanhSachVatTuNhap(SelectedMonth, _selectedYear);
                LoadDanhSachVatTuXuat(SelectedMonth, _selectedYear);
                LoadXuatMonthSeries(SelectedMonth, _selectedYear);
                LoadNhapYearSeries(_selectedYear);
                LoadXuatYearSeries(_selectedYear);
                LoadDoanhThuMonthSeries(SelectedMonth, _selectedYear);
                LoadDoanhThuYearSeries(_selectedYear);
                FindBestVT(); } }
        #endregion
        #region NhapMonthChart
        SeriesCollection _nhapMonthSeries = new SeriesCollection();
        public SeriesCollection NhapMonthSeries
        {
            get { return _nhapMonthSeries; }
            set { _nhapMonthSeries = value; OnPropertyChanged(); }
        }
        private List<string> _monthLabels = new List<string>();
        public List<string> MonthLabels { get { return _monthLabels; } set { _monthLabels = value; OnPropertyChanged(); } }
        public Func<double, string> Formatter { get; set; }
        #endregion
        #region NhapYearChart
        SeriesCollection _nhapYearSeries = new SeriesCollection();
        public SeriesCollection NhapYearSeries
        {
            get { return _nhapYearSeries; }
            set { _nhapYearSeries = value; OnPropertyChanged(); }
        }
        private List<string> _yearLabels = new List<string>();
        public List<string> YearLabels { get { return _yearLabels; } set { _yearLabels = value; OnPropertyChanged(); } }
        #endregion
        #region XuatMonthChart
        SeriesCollection _xuatMonthSeries = new SeriesCollection();
        public SeriesCollection XuatMonthSeries
        {
            get { return _xuatMonthSeries; }
            set { _xuatMonthSeries = value; OnPropertyChanged(); }
        }
        #endregion
        #region XuatYearChart
        SeriesCollection _xuatYearSeries = new SeriesCollection();
        public SeriesCollection XuatYearSeries
        {
            get { return _xuatYearSeries; }
            set { _xuatYearSeries = value; OnPropertyChanged(); }
        }
        #endregion
        #region DoanhThuMonthChart
        SeriesCollection _danhThuMonthSeries = new SeriesCollection();
        public SeriesCollection DoanhThuMonthSeries
        {
            get { return _danhThuMonthSeries; }
            set { _danhThuMonthSeries = value; OnPropertyChanged(); }
        }
        private List<string> _khachHangLabels = new List<string>();
        public List<string> KhachHangLabels
        {
            get { return _khachHangLabels; }
            set { _khachHangLabels = value; OnPropertyChanged(); }
        }
        #endregion
        #region DoanhThuYearChart
        SeriesCollection _danhThuYearSeries = new SeriesCollection();
        public SeriesCollection DoanhThuYearSeries
        {
            get { return _danhThuYearSeries; }
            set { _danhThuYearSeries = value; OnPropertyChanged(); }
        }
        #endregion
        #region NhapXuatChart
        SeriesCollection _nhapXuatSeries = new SeriesCollection()
        {
            new PieSeries()
            {
                Title="Tiền nhập hàng",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill=Brushes.DeepSkyBlue
            },
            new PieSeries()
            {
                Title="Tiền xuất hàng",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill=Brushes.Aquamarine
            }
        };
        public SeriesCollection NhapXuatSeries
        {
            get { return _nhapXuatSeries; }
            set { _nhapXuatSeries= value; OnPropertyChanged(); }
        }
        #endregion
        #region PhieuXuatChart
        SeriesCollection _phieuXuatSeries = new SeriesCollection()
        {
            new PieSeries()
            {
                Title = "Chưa duyệt",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill = Brushes.DeepSkyBlue,
            },
            new PieSeries()
            {
                Title = "Đã duyệt",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill = Brushes.Green,
            },
            new PieSeries()
            {
                Title = "Bị từ chối",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill = Brushes.OrangeRed
            },
            new PieSeries()
            {
                Title = "Thiếu hàng",
                Values = new ChartValues<ObservableValue>(),
                DataLabels = true,
                Fill = Brushes.Red,
            }
        };
        public SeriesCollection PhieuXuatSeries
        {
            get { return _phieuXuatSeries; }
            set { _phieuXuatSeries = value; OnPropertyChanged(); }
        }
        #endregion
        #region KhachHangChart
        SeriesCollection _khachHangSeries = new SeriesCollection();
        public SeriesCollection KhachHangSeries
        {
            get { return _khachHangSeries; }
            set { _khachHangSeries = value; OnPropertyChanged(); }
        }

        #endregion
        #region DanhSachVatTu
        ObservableCollection<VatTu> _danhSachVatTuNhap = new ObservableCollection<VatTu>();
        public ObservableCollection<VatTu> DanhSachVatTuNhap
        {
            get { return _danhSachVatTuNhap; }
            set { _danhSachVatTuNhap = value; OnPropertyChanged(); }
        }
        ObservableCollection<VatTu> _danhSachVatTuXuat = new ObservableCollection<VatTu>();
        public ObservableCollection<VatTu> DanhSachVatTuXuat
        {
            get { return _danhSachVatTuXuat; }
            set { _danhSachVatTuXuat = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand PrintCommand {  get; set; }
        void Print(Grid d)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                dlg.PrintVisual(d, "Biểu đồ tương quan nhập xuất");
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Xuất file pdf thành công.", false);
                msg.ShowDialog();
            }
        }
        #endregion
        #region Function
        void LoadYearList()
        {
            YearList.Clear();
            int Year = DateTime.Now.Year;
            for (int i = Year - 10; i <= Year; i++) YearList.Add("Năm " + i.ToString());
        }
        void LoadMonthLabels()
        {
            MonthLabels.Clear();
            for (int i = 1; i <= 31; i++) MonthLabels.Add(i.ToString());
        }
        void LoadYearLabels()
        {
            YearLabels.Clear();
            for (int i = 1; i<=12 ; i++) YearLabels.Add(i.ToString());
        }
        int ConvertMonth(string m)
        {
            return int.Parse(m.Remove(0, 6));
        }
        int ConvertYear(string y)
        {
            return int.Parse(y.Remove(0, 4));
        }
        void LoadNhapMonthSeries(string m, string y)
        {
            int Month = ConvertMonth(m); int Year = ConvertYear(y);
            NhapMonthSeries.Clear();
            NhapMonthSeries.Add(
                    new ColumnSeries()
                    {
                        Title = m,
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Aqua
                    }
                ) ;
            for (int i = 1; i <= 31; i++) 
            {
                var ListFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == Month && ((DateTime)(p.NgayLap ?? DateTime.Now)).Day == i).ToList();
                if(ListFromDB == null)
                {
                    NhapMonthSeries[0].Values.Add(0); continue;
                }
                int Total = 0;
                foreach (var item in ListFromDB) Total += item.TongGia??0;
                NhapMonthSeries[0].Values.Add(Total); 
            };
        }
        void LoadNhapYearSeries(string y)
        {
            int Year = ConvertYear(y);
            NhapYearSeries.Clear();
            NhapYearSeries.Add(
                    new ColumnSeries()
                    {
                        Title = y,
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Aqua
                    }
                );
            for(int i=1; i<=12; i++)
            {
                var ListFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == i).ToList();
                if (ListFromDB == null)
                {
                    NhapMonthSeries[0].Values.Add(0); continue;
                }
                int Total = 0;
                foreach (var item in ListFromDB) Total += item.TongGia ?? 0;
                NhapYearSeries[0].Values.Add(Total);
            }
        }
        void LoadXuatMonthSeries(string m, string y)
        {
            int Month = ConvertMonth(m); int Year = ConvertYear(y);
            XuatMonthSeries.Clear();
            XuatMonthSeries.Add(
                    new ColumnSeries()
                    {
                        Title = m,
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Aqua
                    }
                );
            for (int i = 1; i <= 31; i++)
            {
                var ListFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == Month && ((DateTime)(p.NgayLap ?? DateTime.Now)).Day == i).ToList();
                if (ListFromDB == null)
                {
                    XuatMonthSeries[0].Values.Add(0); continue;
                }
                int Total = 0;
                foreach (var item in ListFromDB) Total += item.TongGia ?? 0;
                XuatMonthSeries[0].Values.Add(Total);
            };
        }
        void LoadXuatYearSeries(string y)
        {
            int Year = ConvertYear(y);
            XuatYearSeries.Clear();
            XuatYearSeries.Add(
                    new ColumnSeries()
                    {
                        Title = y,
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Aqua
                    }
                );
            for (int i = 1; i <= 12; i++)
            {
                var ListFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == i).ToList();
                if (ListFromDB == null)
                {
                    XuatMonthSeries[0].Values.Add(0); continue;
                }
                int Total = 0;
                foreach (var item in ListFromDB) Total += item.TongGia ?? 0;
                XuatYearSeries[0].Values.Add(Total);
            }
        }
        void LoadDoanhThuMonthSeries(string m, string y)
        {
            int Month = ConvertMonth(m); int Year = ConvertYear(y);
            DoanhThuMonthSeries.Clear();
            DoanhThuMonthSeries.Add(
                    new ColumnSeries()
                    {
                        Title = "Lời",
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Yellow
                    }
                );
            DoanhThuMonthSeries.Add(
                    new ColumnSeries()
                    {
                        Title = "Lỗ",
                        Values = new ChartValues<int>(),
                        Fill = Brushes.OrangeRed
                    }
                );
            for(int i=1; i<=31; i++)
            {
                int Total = 0;
                var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == Month && ((DateTime)(p.NgayLap ?? DateTime.Now)).Day == i).ToList();
                if(ListPNFromDB != null)
                {
                    foreach(var item in ListPNFromDB) Total -= item.TongGia ?? 0;
                }
                var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == Month && ((DateTime)(p.NgayLap ?? DateTime.Now)).Day == i).ToList();
                if(ListPXFromDB != null) 
                {
                    foreach(var item in ListPXFromDB) Total += item.TongGia ?? 0;
                }
                if (Total < 0)
                {
                    DoanhThuMonthSeries[1].Values.Add(-1*Total);
                    DoanhThuMonthSeries[0].Values.Add(0);
                }
                else
                {
                    DoanhThuMonthSeries[1].Values.Add(0);
                    DoanhThuMonthSeries[0].Values.Add(Total);
                }
            }
        }
        void LoadDoanhThuYearSeries(string y)
        {
            int Year = ConvertYear(y);
            DoanhThuYearSeries.Clear();
            DoanhThuYearSeries.Add(
                    new ColumnSeries()
                    {
                        Title = "lời",
                        Values = new ChartValues<int>(),
                        Fill = Brushes.Yellow
                    }
                );
            DoanhThuYearSeries.Add(
                    new ColumnSeries()
                    {
                        Title = "Lỗ",
                        Values = new ChartValues<int>(),
                        Fill = Brushes.OrangeRed
                    }
                );
            for(int i=1; i<=12; i++)
            {
                int Total = 0;
                var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == i).ToList();
                if(ListPNFromDB != null)
                {
                    foreach (var item in ListPNFromDB) Total -= item.TongGia ?? 0;
                }
                var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(p.NgayLap ?? DateTime.Now)).Month == i).ToList();
                if(ListPXFromDB != null)
                {
                    foreach (var item in ListPXFromDB) Total += item.TongGia ?? 0;
                }
                if (Total < 0)
                {
                    DoanhThuYearSeries[1].Values.Add(-1 * Total);
                    DoanhThuYearSeries[0].Values.Add(0);
                }
                else
                {
                    DoanhThuYearSeries[1].Values.Add(0);
                    DoanhThuYearSeries[0].Values.Add(Total);
                }
            }
        }
        void LoadDanhSachVatTuNhap(string m, string y)
        {
            DanhSachVatTuNhap.Clear();
            int Month = ConvertMonth(m); int Year = ConvertYear(y);
            if (TheoThang)
            {
                var supplies = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
                if(supplies != null)
                {
                    foreach(var supply in supplies)
                    {
                        int SL = 0;
                        var ListInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaVt == supply.MaVt).ToList();
                        if (ListInfos == null)
                        {
                            DanhSachVatTuNhap.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = 0 }); continue;
                        }
                        foreach(var info in ListInfos)
                        {
                            var note = DataProvider.Instance.DB.GoodsReceivedNotes.Find(info.MaPn);
                            if (note != null)
                            {
                                if (note.DaXoa == false && note.TrangThai == "Đã duyệt" && ((DateTime)(note.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(note.NgayLap ?? DateTime.Now)).Month == Month) SL += info.SoLuong ?? 0;
                            }
                        }
                        DanhSachVatTuNhap.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = SL });
                    }
                }
            }
            else
            {
                var supplies = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
                if (supplies != null)
                {
                    foreach (var supply in supplies)
                    {
                        int SL = 0;
                        var ListInfos = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(p => p.MaVt == supply.MaVt).ToList();
                        if (ListInfos == null)
                        {
                            DanhSachVatTuNhap.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = 0 }); continue;
                        }
                        foreach (var info in ListInfos)
                        {
                            var note = DataProvider.Instance.DB.GoodsReceivedNotes.Find(info.MaPn);
                            if (note != null)
                            {
                                if (note.DaXoa == false && note.TrangThai == "Đã duyệt" && ((DateTime)(note.NgayLap ?? DateTime.Now)).Year == Year) SL += info.SoLuong ?? 0;
                            }
                        }
                        DanhSachVatTuNhap.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = SL });
                    }
                }
            }
        }
        void LoadDanhSachVatTuXuat(string m, string y)
        {
            DanhSachVatTuXuat.Clear();
            int Month = ConvertMonth(m); int Year = ConvertYear(y);
            if (TheoThang)
            {
                var supplies = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
                if (supplies != null)
                {
                    foreach (var supply in supplies)
                    {
                        int SL = 0;
                        var ListInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaVt == supply.MaVt).ToList();
                        if (ListInfos == null)
                        {
                            DanhSachVatTuXuat.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = 0 }); continue;
                        }
                        foreach (var info in ListInfos)
                        {
                            var note = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(info.MaPx);
                            if (note != null)
                            {
                                if (note.DaXoa == false && note.TrangThai == "Đã duyệt" && ((DateTime)(note.NgayLap ?? DateTime.Now)).Year == Year && ((DateTime)(note.NgayLap ?? DateTime.Now)).Month == Month) SL += info.SoLuong ?? 0;
                            }
                        }
                        DanhSachVatTuXuat.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = SL });
                    }
                }
            }
            else
            {
                var supplies = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
                if (supplies != null)
                {
                    foreach (var supply in supplies)
                    {
                        int SL = 0;
                        var ListInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaVt == supply.MaVt).ToList();
                        if (ListInfos == null)
                        {
                            DanhSachVatTuXuat.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = 0 }); continue;
                        }
                        foreach (var info in ListInfos)
                        {
                            var note = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(info.MaPx);
                            if (note != null)
                            {
                                if (note.DaXoa == false && note.TrangThai == "Đã duyệt" && ((DateTime)(note.NgayLap ?? DateTime.Now)).Year == Year) SL += info.SoLuong ?? 0;
                            }
                        }
                        if (SL > BestSL) 
                        { 
                            BestVT = supply.MaVt; BestSL = SL; 
                        }
                        DanhSachVatTuXuat.Add(new VatTu() { MaVt = supply.MaVt, SoLuong = SL });
                    }
                }
            }
        }
        void FindBestVT()
        {
            int BestGia = 0;
            var supplies = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
            if (supplies != null)
            {
                foreach (var supply in supplies)
                {
                    int SL = 0;
                    var ListInfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(p => p.MaVt == supply.MaVt).ToList();
                    if (ListInfos == null) continue;
                    foreach (var info in ListInfos)
                    {
                        var note = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(info.MaPx);
                        if (note != null)
                        {
                            if (note.DaXoa == false && note.TrangThai == "Đã duyệt" && ((DateTime)(note.NgayLap ?? DateTime.Now)).Year == 2024) SL += info.SoLuong ?? 0;
                        }
                    }
                    if (SL * supply.GiaXuat > BestGia)
                    {
                        BestVT = supply.MaVt; BestSL = SL; BestGia = SL * supply.GiaXuat ?? 0;
                    }
                }
            }
        }
        void FindTienNHTienXH()
        {
            DoanhThuNamNay = 0;
            TienNhapHang = 0;
            var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? default)).Year == 2024).ToList();
            foreach(var info in ListPNFromDB) { TienNhapHang += info.TongGia ?? 0; }
            TienXuatHang = 0;
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false && p.TrangThai == "Đã duyệt" && ((DateTime)(p.NgayLap ?? DateTime.Now)).Year == 2024).ToList();
            foreach (var info in ListPXFromDB) { TienXuatHang += info.TongGia ?? 0; }
            DoanhThuNamNay = TienXuatHang - TienNhapHang;
        }
        void LoadNhapXuatChart()
        {
            NhapXuatSeries[0].Values.Clear(); NhapXuatSeries[1].Values.Clear();
            NhapXuatSeries[0].Values.Add(new ObservableValue(TienNhapHang));
            NhapXuatSeries[1].Values.Add(new ObservableValue(TienXuatHang));
        }
        void LoadPhieuXuatChart()
        {
            for(int i=0; i<4; i++)
                PhieuXuatSeries[i].Values.Clear();
            int CD = 0, DD = 0, BTC = 0, TH = 0;
            var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(p => p.DaXoa == false).ToList();
            if(ListPXFromDB != null)
            {
                foreach(var item in ListPXFromDB)
                {
                    if (item.TrangThai == "Chưa duyệt" || item.TrangThai == "Kế toán đã duyệt") CD++;
                    if (item.TrangThai == "Đã duyệt") DD++;
                    if (item.TrangThai == "Bị từ chối") BTC++;
                    if (item.TrangThai == "Thiếu hàng") TH++;
                }
            }
            PhieuXuatSeries[0].Values.Add(new ObservableValue(CD));
            PhieuXuatSeries[1].Values.Add(new ObservableValue(DD));
            PhieuXuatSeries[2].Values.Add(new ObservableValue(BTC));
            PhieuXuatSeries[3].Values.Add(new ObservableValue(TH));
        }

        #endregion
        public class VatTu
        {
            public string MaVt { get; set; }
            public int SoLuong { get; set; }
        }
        public class KhachHang
        {
            public string MaKh { get; set; }
            public int DaMua { get; set; }
        }
    }
    

}
