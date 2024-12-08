using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
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
        private ObservableCollection<Request> _danhSachYeuCau = new ObservableCollection<Request>();
        public ObservableCollection<Request> DanhSachYeuCau
        {
            get { return _danhSachYeuCau; }
            set { _danhSachYeuCau = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _selectedYeuCau = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedYeuCau
        {
            get { return _selectedYeuCau; }
            set { _selectedYeuCau = value; OnPropertyChanged(); }
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn lưu các mục đã chọn?", true);
            msg.ShowDialog();
            if(msg.ReturnValue == true)
            {
                foreach (Request i in DanhSachYeuCau)
                {
                    if (i.Checked == true) SelectedYeuCau.Add(i.MaYC);
                }
                t.Close();
            }
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window t)
        {
            SelectedYeuCau.Clear();
            SelectedYeuCau.Clear();
            t.Close();
        }
        #endregion
        #region Function
        void LoadData()
        {
            if (IsYCN)
            {
                var ListFromDB = DataProvider.Instance.DB.ImportRequests.ToList();
                foreach(var request in ListFromDB)
                {
                    Request temp = new Request(); temp.MaYC = request.MaYcn; temp.NgayLap = request.NgayLap;
                    DanhSachYeuCau.Add(temp);
                }
            }
            else
            {
                var ListFromDB = DataProvider.Instance.DB.ExportRequests.ToList();
                foreach (var request in ListFromDB)
                {
                    Request temp = new Request(); temp.MaYC = request.MaYcx; temp.NgayLap = request.NgayLap;
                    DanhSachYeuCau.Add(temp);
                }
            }

            //DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0001", NgayLap = "Friday,06/12/2024"});
            //DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0002", NgayLap = "Friday,06/12/2024" });
            //DanhSachYeuCauNhap.Add(new ImportRequest() { MaYCN = "YCN0003", NgayLap = "Friday,06/12/2024" });

            //DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0001", NgayLap = "Friday,06/12/2024" });
            //DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0002", NgayLap = "Friday,06/12/2024" });
            //DanhSachYeuCauXuat.Add(new ExportRequest() { MaYCX = "YCN0003", NgayLap = "Friday,06/12/2024" });
        }
        #endregion
        public class Request
        {
            public bool Checked { get; set; }
            public string MaYC { get; set; }
            public DateTime? NgayLap { get; set; }
        }
    }
}
