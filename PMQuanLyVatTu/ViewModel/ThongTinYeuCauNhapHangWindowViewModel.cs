using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
        }
        #region Title
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        #endregion
        #region Info
        private string _maYCN = "";
        private string _maNV;
        private string _maVT;
        private string _ngayLap;
        private int _soLuong;
        private string _ghiChu;
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
        private bool _editMode;
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
            { //Chấp nhận xóa
                EnableEditing = false;
                //Xóa
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.", false);
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm 
            {
                if (MaYCN == "") //Chưa nhập mã 
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã yêu cầu nhập hàng.");
                    msg.ShowDialog();
                }
                else if (true) //Trùng mã 
                {
                    AlreadyExistsError msg = new AlreadyExistsError();
                    msg.ShowDialog();
                }
                else // Hợp lệ
                {
                    EnableEditing = false;
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm yêu cầu thành công.", false);
                    msg.ShowDialog();
                    (t as Window).Close();
                }
            }
        }
        #endregion
        #region Function
        void LoadNhanVien()
        {
            var ListFromDB = DataProvider.Instance.DB.Employees.ToList();
            foreach(var item in ListFromDB)
            {
                if(item.ChucVu == "Kế Toán" && item.DaXoa == false) NhanVien.Add(item.MaNv);
            }
        }
        void LoadVatTu()
        {
            var ListFromDB = DataProvider.Instance.DB.Supplies.ToList();
            foreach(var item in ListFromDB)
            {
                if(item.DaXoa == false) VatTu.Add(item.MaVt);
            }
        }
        void LoadData(string maycn)
        {
            MaYCN = maycn;
            MaNV = "NV0001";
            MaVT = "VT0005";
            NgayLap = "03/12/2024";
            GhiChu = "Xuất đủ hàng và giao đi trước ngày 07/12/2024";
        }
        #endregion
    }
}
