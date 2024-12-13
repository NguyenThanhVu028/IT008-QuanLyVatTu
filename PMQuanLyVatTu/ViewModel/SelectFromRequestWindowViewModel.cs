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
        public SelectFromRequestWindowViewModel(bool isYCN = false, string ma1 = "", string ma2 = "")
        {
            IsYCN = isYCN;
            LoadData(ma1, ma2);

            ConfirmCommand = new RelayCommand<Window>(Confirm);
            CancelCommand = new RelayCommand<Window>(Cancel);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        #region Info
        private string _ma1 = "";
        private string _ma2 = "";
        public string Ma1
        {
            get { return _ma1; }
            set { _ma1 = value; OnPropertyChanged(); }
        }
        public string Ma2
        {
            get { return _ma2; }
            set { _ma2 = value; OnPropertyChanged(); }
        }
        #endregion
        #region IsYCN
        private bool _isYCN = false;
        public bool IsYCN
        {
            get { return _isYCN; }
            set { _isYCN = value; OnPropertyChanged(); }
        }
        #endregion
        #region ReturnValue
        private bool _returnValue = false;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachYeuCau
        private ObservableCollection<Request> _danhSachYeuCau = new ObservableCollection<Request>();
        public ObservableCollection<Request> DanhSachYeuCau
        {
            get { return _danhSachYeuCau; }
            set { _danhSachYeuCau = value; OnPropertyChanged(); }
        }
        private Request _selectedYeuCau;
        public Request SelectedYeuCau
        {
            get { return _selectedYeuCau; }
            set { _selectedYeuCau = value; OnPropertyChanged(); }
        }
        private string _selectedYeuCauValue = "";
        public string SelectedYeuCauValue
        {
            get { return _selectedYeuCauValue; }
            set { _selectedYeuCauValue = value; OnPropertyChanged(); }
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
            if (SelectedYeuCau == null)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn một yêu cầu.", false);
                msg1.ShowDialog();
                return;
            }
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn lưu mục đã chọn?", true);
            msg.ShowDialog();
            if(msg.ReturnValue == true)
            {
                if (IsYCN)
                {
                    var YC = DataProvider.Instance.DB.ImportRequests.Find(SelectedYeuCau.MaYC);
                    if (YC != null)
                    {
                        var item = DataProvider.Instance.DB.Supplies.Find(YC.MaVt);
                        if(item != null)
                        {
                            Ma1 = item.MaNcc; Ma2 = item.MaKho;
                        }
                    }
                }
                else
                {
                    var Request = DataProvider.Instance.DB.ExportRequests.Find(SelectedYeuCau.MaYC);
                    if(Request != null)
                    {
                        Ma1 = Request.MaKh; Ma2 = Request.KhoXuat;
                    }
                }
                ReturnValue = true;
                SelectedYeuCauValue = SelectedYeuCau.MaYC;
                t.Close();
            }
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window t)
        {
            SelectedYeuCauValue = "";
            ReturnValue = false;
            t.Close();
        }
        #endregion
        #region Function
        void LoadData(string ma1, string ma2)
        {
            DanhSachYeuCau.Clear();
            if (IsYCN)
            {
                var ListYC = DataProvider.Instance.DB.ImportRequests.Where(p=>p.DaXoa == false).ToList();
                foreach(var request in ListYC)
                {
                    var item = DataProvider.Instance.DB.Supplies.Find(request.MaVt);
                    if(item != null)
                    {
                        if(item.MaNcc.Contains(ma1) && item.MaKho.Contains(ma2))
                        {
                            Request temp = new Request(); temp.MaYC = request.MaYcn; temp.NgayLap = request.NgayLap;
                            DanhSachYeuCau.Add(temp);
                        }
                    }
                }
            }
            else
            {
                var ListFromDB = DataProvider.Instance.DB.ExportRequests.Where(p=>p.DaXoa == false).ToList();
                foreach(var item in ListFromDB)
                {
                    if (item.MaKh.Contains(ma1) && item.KhoXuat.Contains(ma2))
                    {
                        Request temp = new Request(); temp.MaYC = item.MaYcx; temp.NgayLap = item.NgayLap;
                        DanhSachYeuCau.Add(temp);
                    }
                }
                //var ListFromDB = DataProvider.Instance.DB.ExportRequests.ToList();
                //foreach (var request in ListFromDB)
                //{
                //    if (request.DaXoa == false)
                //    {
                //        Request temp = new Request(); temp.MaYC = request.MaYcx; temp.NgayLap = request.NgayLap;
                //        DanhSachYeuCau.Add(temp);
                //        //if (request.MaKh.Contains(ma1) && request.KhoXuat.Contains(ma2))
                //        //{
                //        //    Request temp = new Request(); temp.MaYC = request.MaYcx; temp.NgayLap = request.NgayLap;
                //        //    DanhSachYeuCau.Add(temp); break;
                //        //}
                //        //foreach (var item in ListSupplyFromDB)
                //        //{
                //        //    if (item.DaXoa == false && item.MaVt == request.MaVt && item.MaNcc.Contains(ma1) && item.MaKho.Contains(ma2))
                //        //    {
                //        //        Request temp = new Request(); temp.MaYC = request.MaYcn; temp.NgayLap = request.NgayLap;
                //        //        DanhSachYeuCau.Add(temp); break;
                //        //    }
                //        //}
                //    }
                //}
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
