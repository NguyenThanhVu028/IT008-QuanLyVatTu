using Microsoft.IdentityModel.Tokens;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinYeuCauNhapHangWindowViewModel : BaseViewModel
    {
        public ThongTinYeuCauNhapHangWindowViewModel(string maycn = null) 
        {
            if (maycn != null) { EditMode = true; Title = "CHỈNH SỬA YÊU CẦU NHẬP"; LoadData(maycn);}
            else { EnableEditing = true; EditMode = false; Title = "THÊM YÊU CẦU NHẬP"; }

            LoadNhanVien();
            LoadVatTu();

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<Window>(SaveInfo);
        }
        #region Title
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maYCN = "";
        private string _maNV = CurrentUser.Instance.MaNv;
        private string _maVT = "";
        private string _ngayLap = DateTime.Now.ToString("dd/ddd/MM/yyyy");
        private int _soLuong = 0;
        private string _ghiChu = "";
        public string MaYCN
        {
            get { return _maYCN; }
            set { _maYCN = value; OnPropertyChanged(); }
        }
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string MaVT
        {
            get { return _maVT; }
            set { _maVT = value; OnPropertyChanged(); }
        }
        public string NgayLap
        {
            get { return _ngayLap; }
            set { _ngayLap = value; OnPropertyChanged(); }
        }
        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }
        public string GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; OnPropertyChanged(); }
        }
        #endregion
        #region EditMode
        private bool _editMode = false;
        public bool EditMode
        {
            get { return _editMode; }
            set { _editMode = value; OnPropertyChanged(); }
        }
        #endregion
        #region EnableEditing
        private bool _enableEditing = false;
        public bool EnableEditing
        {
            get { return _enableEditing; }
            set { _enableEditing = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _nhanVien = new ObservableCollection<string>();
        public ObservableCollection<string> NhanVien
        {
            get { return _nhanVien; }
            set { _nhanVien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _vatTu = new ObservableCollection<string>();
        public ObservableCollection<string> VatTu
        {
            get { return _vatTu; }
            set { _vatTu = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand EditInfoCommand { get; set; }
        void EditInfo(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã vào chế độ chỉnh sửa thông tin.");
            msg.ShowDialog();
            if (msg.ReturnValue == true) EnableEditing = true;
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa yêu cầu đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                EnableEditing = false;
                var YCN = DataProvider.Instance.DB.ImportRequests.Find(MaYCN);

                if (YCN != null)
                {
                    YCN.DaXoa = true;
                    YCN.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy yêu cầu nhập để xóa!", false);
                }
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(Window t)
        {
            if (MaYCN.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã yêu cầu nhập.");
                msg.ShowDialog();
                return;
            }
            if (MaYCN.Length > 7)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mã yêu cầu nhập không được vượt quá 7 ký tự.", false);
                msg1.ShowDialog();
                return;
            }
            if (MaNV.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã nhân viên.");
                msg.ShowDialog();
                return;
            }
            if (MaVT.IsNullOrEmpty())
            {
                var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã vật tư.");
                msg.ShowDialog();
                return;
            }
            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                //Lưu thông tin vào database
                var YCN = DataProvider.Instance.DB.ImportRequests.Find(MaYCN);

                if (YCN != null)
                {
                    YCN.MaVt = MaVT;
                    YCN.MaNv = MaNV;
                    YCN.SoLuong = SoLuong;
                    YCN.GhiChu = GhiChu;
                    try
                    {
                        YCN.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        YCN.NgayLap = DateTime.Now;
                    }
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                    msg.ShowDialog();
                    EnableEditing = false;
                }
                else
                {
                    var msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Yêu cầu đã không còn trên hệ thống.");
                    msg.ShowDialog();
                    return;
                }
                DataProvider.Instance.DB.SaveChanges(true);
            }
            else //Nếu trong chế độ thêm YCN
            {
                var YCN = DataProvider.Instance.DB.ImportRequests.Find(MaYCN);

                if (YCN != null)
                {
                    if (YCN.DaXoa == true)
                    {
                        YCN.MaVt = MaVT;
                        YCN.MaNv = MaNV;
                        YCN.SoLuong = SoLuong;
                        YCN.GhiChu = GhiChu;
                        YCN.DaXoa = false;
                        YCN.TrangThai = "Chưa tiếp nhận";
                        try
                        {
                            YCN.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        catch
                        {
                            YCN.NgayLap = DateTime.Now;
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm yêu cầu nhập thành công.");
                        msgSuccess.ShowDialog();
                    }
                    else //Trùng mã YCN
                    {
                        AlreadyExistsError msg1 = new AlreadyExistsError();
                        msg1.ShowDialog();
                        return;
                    }
                }
                else
                {
                    ImportRequest NewYCN = new ImportRequest
                    {
                        MaYcn = MaYCN,
                        MaNv = MaNV,
                        MaVt = MaVT,
                        SoLuong = SoLuong,
                        GhiChu = GhiChu,
                        TrangThai = "Chưa tiếp nhận",
                        DaXoa = false
                    };
                    try
                    {
                        NewYCN.NgayLap = DateTime.ParseExact(NgayLap, "ddd/dd/MM/yyyy", CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        NewYCN.NgayLap = DateTime.Now;
                    }
                    DataProvider.Instance.DB.ImportRequests.Add(NewYCN);
                    DataProvider.Instance.DB.SaveChanges();
                    CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm yêu cầu nhập thành công.");
                    msgSuccess.ShowDialog();
                    t.Close();
                }
            }
            EnableEditing = false;
        }
        #endregion
        #region Function
        void LoadNhanVien()
        {
            NhanVien.Clear();
            var ListFromDB = DataProvider.Instance.DB.Employees.Where(p => p.DaXoa == false).ToList();
            foreach(var item in ListFromDB)
            {
                if(item.ChucVu == "Kế Toán" && item.DaXoa == false) NhanVien.Add(item.MaNv);
            }
        }
        void LoadVatTu()
        {
            VatTu.Clear();
            var ListFromDB = DataProvider.Instance.DB.Supplies.Where(p => p.DaXoa == false).ToList();
            foreach(var item in ListFromDB)
            {
                if(item.DaXoa == false) VatTu.Add(item.MaVt);
            }
        }
        void LoadData(string maycn)
        {
            var YCN = DataProvider.Instance.DB.ImportRequests.Find(maycn);
            if (YCN != null)
            {
                MaYCN = maycn;
                MaVT = (YCN.MaVt != null)?YCN.MaVt : "";
                MaNV = (YCN.MaNv != null)? YCN.MaNv : "";
                SoLuong = (YCN.SoLuong != null)? YCN.SoLuong.Value : 0;
                NgayLap = (YCN.NgayLap != null) ? YCN.NgayLap.Value.ToString("ddd dd/MM/yyyy") : "";
                GhiChu = (YCN.GhiChu != null) ? YCN.GhiChu : "";
            }
        }
        #endregion
    }
}
