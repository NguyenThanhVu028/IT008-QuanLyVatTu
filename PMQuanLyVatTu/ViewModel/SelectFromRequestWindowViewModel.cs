using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    class SelectFromRequestWindowViewModel : BaseViewModel
    {
        public SelectFromRequestWindowViewModel(bool isYCN = false)
        {
            IsYCN = isYCN;
            LoadData();

            ConfirmCommand = new RelayCommand<Window>(Confirm);
            CancelCommand = new RelayCommand<Window>(Cancel);
        }
        #region IsYCN
        private bool _isYCN;
        public bool IsYCN
        {
            get { return _isYCN; }
            set { _isYCN = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachYeuCau
        private ObservableCollection<ImportRequest> _danhSachYeuCauNhap = new ObservableCollection<ImportRequest>();
        public ObservableCollection<ImportRequest> DanhSachYeuCauNhap
        {
            get { return _danhSachYeuCauNhap; }
            set { _danhSachYeuCauNhap = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ImportRequest> _selectedYeuCauNhap = new ObservableCollection<ImportRequest>();
        public ObservableCollection<ImportRequest> SelectedYeuCauNhap
        {
            get { return _selectedYeuCauNhap; }
            set { _selectedYeuCauNhap = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ExportRequest> _danhSachYeuCauXuat = new ObservableCollection<ExportRequest>();
        public ObservableCollection<ExportRequest> DanhSachYeuCauXuat
        {
            get { return _danhSachYeuCauXuat; }
            set { _danhSachYeuCauXuat = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ExportRequest> _selectedYeuCauXuat = new ObservableCollection<ExportRequest>();
        public ObservableCollection<ExportRequest> SelectedYeuCauXuat
        {
            get { return _selectedYeuCauXuat; }
            set { _selectedYeuCauXuat = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand ConfirmCommand { get; set; }
        void Confirm(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn lưu các mục đã chọn?");
            msg.ShowDialog();
            if(msg.ReturnValue == true)
            {
                foreach (ImportRequest i in DanhSachYeuCauNhap)
                {
                    if (i.Checked == true) SelectedYeuCauNhap.Add(i);
                }
                foreach (ExportRequest i in DanhSachYeuCauXuat)
                {
                    if (i.Checked == true) SelectedYeuCauXuat.Add(i);
                }
                t.Close();
            }
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window t)
        {
            SelectedYeuCauNhap.Clear();
            SelectedYeuCauXuat.Clear();
            t.Close();
        }
        #endregion
        #region Function
        void LoadData()
        {
            DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0001", NgayLap = "Friday,06/12/2024"});
            DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0002", NgayLap = "Friday,06/12/2024" });
            DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0003", NgayLap = "Friday,06/12/2024" });

            DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0001", NgayLap = "Friday,06/12/2024" });
            DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0002", NgayLap = "Friday,06/12/2024" });
            DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0003", NgayLap = "Friday,06/12/2024" });
        }
        #endregion
    }
}
