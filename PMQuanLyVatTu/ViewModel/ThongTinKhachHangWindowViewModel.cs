using Microsoft.EntityFrameworkCore.Storage;
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
    public class ThongTinKhachHangWindowViewModel : BaseViewModel
    {
        public ThongTinKhachHangWindowViewModel(string makh = null)
        {
            if(makh != null) { EditMode = true; Title = "Chỉnh sửa khách hàng"; LoadData(makh); }
            else { EnableEditing = true; EditMode = false; Title = "Thêm khách hàng"; }

            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
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
        private string _maKH = "";
        private string _hoTen = "";
        private string _gTinh = "";
        private string _ngaySinh = "";
        private string _email = "";
        private string _sdt = "";
        private string _diaChi = "";
        public string MaKH
        {
            get { return _maKH; }
            set { _maKH = value; OnPropertyChanged(); }
        }
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; OnPropertyChanged(); }
        }
        public string GTinh
        {
            get { return _gTinh; }
            set { _gTinh = value; OnPropertyChanged(); }
        }
        public string NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public string SDT
        {
            get { return _sdt; }
            set { _sdt = value; OnPropertyChanged(); }
        }
        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() { "Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
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
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(Window t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa khách hàng đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            { //Chấp nhận xóa
                EnableEditing = false;
                //Xóa
                var kh = DataProvider.Instance.DB.Customers.Find(MaKH);
                if(kh != null)
                {
                    kh.DaXoa = true;
                    DataProvider.Instance.DB.SaveChanges();
                }
                t.Close();
            }
        }
        void EditInfo(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã vào chế độ chỉnh sửa thông tin.");
            msg.ShowDialog();
            if (msg.ReturnValue == true) EnableEditing = true;
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                var KH = DataProvider.Instance.DB.Customers.Find(MaKH);
                if (KH != null)
                {
                    KH.HoTen = HoTen;
                    KH.GioiTinh = GTinh;
                    KH.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy");
                    KH.Sdt = SDT;
                    KH.Email = Email;
                    KH.DiaChi = DiaChi;
                    DataProvider.Instance.DB.SaveChanges();
                }
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm nhân viên
            {
                if (MaKH == "") //Chưa nhập mã nhân viên
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã khách hàng.");
                    msg1.ShowDialog();
                    return;
                }
                if (CompareAndExecute()) //Trùng mã nhân viên
                {
                    AlreadyExistsError msg2 = new AlreadyExistsError();
                    msg2.ShowDialog();
                    return;
                }
                // Hợp lệ
                Addnew();
                EnableEditing = false;
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm khách hàng thành công.");
                msg.ShowDialog();
                (t as Window).Close();
            }
        }
        bool CompareAndExecute()
        {
            var kh = DataProvider.Instance.DB.Customers.Find(MaKH);
            if (kh == null)
            {
                return false;
            }
            else
            {
                if(kh.DaXoa == true)
                {
                    DataProvider.Instance.DB.Customers.Remove(kh);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            return true;
        }
        void Addnew()
        {
            var newKH = new Customer();
            newKH.MaKh = MaKH;
            newKH.HoTen = HoTen;
            newKH.GioiTinh = GTinh;
            newKH.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy");
            newKH.Sdt = SDT;
            newKH.Email = Email;
            newKH.DiaChi = DiaChi;
            newKH.DaXoa = false;
            DataProvider.Instance.DB.Customers.Add(newKH);
            DataProvider.Instance.DB.SaveChanges();
        }
        #endregion
        #region Function
        void LoadData(string makh)
        {
            MaKH = makh;
            var KH = DataProvider.Instance.DB.Customers.Find(makh);
            if (KH != null)
            {
                HoTen = KH.HoTen;
                GTinh = KH.GioiTinh;
                NgaySinh = ((DateOnly)KH.NgaySinh).ToDateTime(TimeOnly.MinValue).ToString("ddd/dd/MM/yyyy");
                Email = KH.Email;
                SDT = KH.Sdt;
                DiaChi = KH.DiaChi;
            }
            else
            {
                makh = "";
                HoTen = "";
                GTinh = "";
                NgaySinh = "";
                Email = "";
                SDT = "";
                DiaChi = "";
            }
            
        }
        #endregion
    }
}
