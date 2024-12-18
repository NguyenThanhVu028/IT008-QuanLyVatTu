using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinNhanVienWindowViewModel : BaseViewModel
    {
        public ThongTinNhanVienWindowViewModel(string manv = null)
        {
            if (manv != null) { EditMode = true; Title = "CHỈNH SỬA NHÂN VIÊN"; LoadData(manv); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM NHÂN VIÊN"; }

            CloseCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            DeleteButtonCommand = new RelayCommand<Window>(DeleteButton);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            ChangeAvatarCommand = new RelayCommand<object>(ChangeAvatar);
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
        private string _maNV = "";
        private string _hoVaTen = "";
        private string _cVu = "";
        private string _gTinh = "";
        private string _ngaySinh = "";
        private string _diaChi = "";
        private string _sDT = "";
        private string _email = "";
        private int _luong = 0;
        private string _tenDangNhap;
        private string _matKhau;
        private string _imageLocation;
        public string MaNV
        {
            get { return _maNV; }
            set { _maNV = value; OnPropertyChanged(); }
        }
        public string HoTen
        {
            get { return _hoVaTen; }
            set { _hoVaTen = value; OnPropertyChanged(); }
        }
        public string CVu
        {
            get { return _cVu; }
            set { _cVu = value; OnPropertyChanged(); }
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
        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; OnPropertyChanged(); }
        }
        public string SDT
        {
            get { return _sDT; }
            set { _sDT = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public int Luong
        {
            get { return _luong; }
            set { _luong = value; OnPropertyChanged(); }
        }
        public string TenDangNhap
        {
            get { return _tenDangNhap; }
            set { _tenDangNhap = value; OnPropertyChanged(); }
        }
        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; OnPropertyChanged(); }
        }
        public string ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; OnPropertyChanged(); }
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
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() { "Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _chucVu = new ObservableCollection<string>() { "QL", "KT", "NX", "TN" };
        public ObservableCollection<string> ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseCommand { get; set; }
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa nhân viên đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            { //Chấp nhận xóa
                EnableEditing = false;
                //Xóa
                if (CurrentUser.Instance.MaNv != MaNV)
                {
                    var nv = DataProvider.Instance.DB.Employees.Find(MaNV);
                    var account = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == MaNV).FirstOrDefault();
                    if (account != null)
                    {

                        account.DaXoa = true;
                    }

                    if (nv != null)
                    {
                        nv.DaXoa = true;
                    }
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    CustomMessage message = new CustomMessage("/Material/Images/Icons/question.png", "LỖI", "Không thể xóa nhân viên hiện tại.");
                    message.ShowDialog();
                }

                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            //Lưu ý chuyển đổi từ string NgaySinh thành NgaySinh của employee thì dùng employee.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy"); và đặt trong khối try-catch

            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                var nv = DataProvider.Instance.DB.Employees.Find(MaNV);
                EnableEditing = false;
                if (nv != null)
                {

                    if (nv.ChucVu == "")
                    {
                        CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Chức vụ không được để trống.");
                        msg1.ShowDialog();
                        EnableEditing = true;
                        return;
                    }
                    else
                    {

                        var acc = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == nv.MaNv).FirstOrDefault();
                        if (acc != null)
                        {
                            var result = DataProvider.Instance.DB.Accounts.Where(e => e.TenDn == TenDangNhap).FirstOrDefault();
                            if (result == null)   // mật khẩu mới này chưa có người dùng
                            {
                                //acc.TenDn = TenDangNhap;
                                //acc.MatKhau = MatKhau;
                                //acc.Email = Email;

                                DataProvider.Instance.DB.Accounts.Remove(acc);
                                var newacc = new Account();
                                newacc.TenDn = TenDangNhap;
                                newacc.MatKhau = MatKhau;
                                newacc.MaNv = MaNV;
                                newacc.Email = Email;
                                newacc.DaXoa = false;

                                DataProvider.Instance.DB.Accounts.Add(newacc);
                            }
                            else
                            {
                                if (result.MaNv == MaNV)
                                {
                                    //acc.TenDn = TenDangNhap;
                                    //acc.MatKhau = MatKhau;
                                    //acc.Email = Email;

                                    DataProvider.Instance.DB.Accounts.Remove(acc);
                                    var newacc = new Account();
                                    newacc.TenDn = TenDangNhap;
                                    newacc.MatKhau = MatKhau;
                                    newacc.MaNv = MaNV;
                                    newacc.Email = Email;
                                    newacc.DaXoa = false;

                                    DataProvider.Instance.DB.Accounts.Add(newacc);
                                }
                                else
                                {
                                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Tên đăng nhập này đã tồn tại.");
                                    msg1.ShowDialog();
                                    EnableEditing = true;
                                    return;
                                }
                            }

                        }
                        else
                        {

                            if (TenDangNhap != "" && MatKhau != "")
                            {
                                var result = DataProvider.Instance.DB.Accounts.Where(e => e.TenDn == TenDangNhap).FirstOrDefault();
                                if (result == null)   // mật khẩu mới này chưa có người dùng
                                {
                                    //acc.TenDn = TenDangNhap;
                                    //acc.MatKhau = MatKhau;
                                    //acc.Email = Email;


                                    var newacc = new Account();
                                    newacc.TenDn = TenDangNhap;
                                    newacc.MatKhau = MatKhau;
                                    newacc.MaNv = MaNV;
                                    newacc.Email = Email;
                                    newacc.DaXoa = false;

                                    DataProvider.Instance.DB.Accounts.Add(newacc);
                                }
                                else
                                {

                                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Tên đăng nhập này đã tồn tại.");
                                    msg1.ShowDialog();
                                    EnableEditing = true;
                                    return;

                                }
                            }
                        }
                        if (HoTen == "")
                        {
                            CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Họ tên không được để trống.");
                            msg1.ShowDialog();
                            EnableEditing = true;
                            return;
                        }
                        nv.HoTen = HoTen;
                        nv.ChucVu = CVu;
                        if (!string.IsNullOrWhiteSpace(NgaySinh))
                        {
                            nv.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy");
                        }
                        else
                        {
                            nv.NgaySinh = DateOnly.MinValue; // Gán giá trị mặc định
                        }
                        nv.GioiTinh = GTinh;
                        nv.DiaChi = DiaChi;
                        nv.Sdt = SDT;
                        nv.Email = Email;
                        nv.Luong = Luong;

                    }
                    DataProvider.Instance.DB.SaveChanges();
                    // Check là nếu đang sửa thông tin của current thì update thông tin cho current
                    if (CurrentUser.Instance.MaNv == MaNV)
                    {
                        CurrentUser.Instance.Update(nv);
                    }
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                    msg.ShowDialog();
                }
            }
            else //Nếu trong chế độ thêm nhân viên
            {
                if (MaNV == "") //Chưa nhập mã nhân viên
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã nhân viên.");
                    msg.ShowDialog();
                }
                else if (CompareAndExecute()) //Trùng mã nhân viên
                {
                    AlreadyExistsError msg = new AlreadyExistsError();
                    msg.ShowDialog();
                }
                else // Hợp lệ
                {
                    EnableEditing = false;
                    //Lưu thông tin từ Info vào database
                    var check = AddNew();
                    if (check)
                    {
                        CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhân viên thành công.");
                        msg.ShowDialog();
                        (t as Window).Close();
                    }
                    else
                    {
                        EnableEditing = true;
                        return;
                    }

                }
            }

        }
        bool CompareAndExecute()
        {
            var nv = DataProvider.Instance.DB.Employees.Find(MaNV);
            if (nv == null)
            {
                return false;
            }
            else
            {
                if (nv.DaXoa == true)
                {
                    DataProvider.Instance.DB.Employees.Remove(nv);
                    return false;

                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        bool AddNew()
        {
            var newNV = new Employee();
            var NewAcc = new Account();
            if (MaNV.Length > 6)
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Mã nhân viên không hợp lệ.");
                msg1.ShowDialog();
                return false;
            }
            if (HoTen == "")
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Họ tên không được để trống.");
                msg1.ShowDialog();
                return false;
            }
            if (CVu == "")
            {
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Chức vụ không được để trống.");
                msg1.ShowDialog();
                return false;
            }
            var result = DataProvider.Instance.DB.Accounts.Where(e => e.TenDn == TenDangNhap).FirstOrDefault();  // kiểm tra tên đăng nhập 
            if (result == null)   // chưa tồn tại có 2 khả năng là : 1. tên đăng nhập chưa có ai dùng   2. chưa nhập tên đăng nhập
            {
                if (TenDangNhap != null && MatKhau != null)  //kiểm tra xem có cho thông tin đăng nhập chưa
                {
                    MessageBox.Show(TenDangNhap);
                    NewAcc.TenDn = TenDangNhap;
                    NewAcc.MatKhau = MatKhau;
                    NewAcc.MaNv = MaNV;
                    NewAcc.Email = Email;
                    NewAcc.DaXoa = false;

                    DataProvider.Instance.DB.Accounts.Add(NewAcc);
                }

            }
            else   // nếu tồn tại tên đăng nhập rồi
            {

                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "LỖI", "Tên đăng nhập này đã tồn tại.");
                msg1.ShowDialog();
                return false;

            }
            newNV.MaNv = MaNV;
            newNV.HoTen = HoTen;
            newNV.ChucVu = CVu;
            if (!string.IsNullOrWhiteSpace(NgaySinh))
            {
                newNV.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy");
            }
            else
            {
                newNV.NgaySinh = DateOnly.MinValue; // Gán giá trị mặc định
            }

            newNV.GioiTinh = GTinh;
            newNV.DiaChi = DiaChi;
            newNV.Sdt = SDT;
            newNV.Email = Email;
            newNV.Luong = Luong;
            newNV.ImageLocation = ImageLocation;
            newNV.DaXoa = false;

            DataProvider.Instance.DB.Employees.Add(newNV);
            DataProvider.Instance.DB.SaveChanges();
            return true;
        }
        public ICommand ChangeAvatarCommand { get; set; }
        void ChangeAvatar(object t)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG|*.png|JPG|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageLocation = openFileDialog.FileName;
                var nv = DataProvider.Instance.DB.Employees.Find(MaNV);
                if (nv != null)
                {
                    nv.ImageLocation = ImageLocation;
                }

            }
        }
        #endregion
        #region Function
        void LoadData(string manv)
        {
            //Dùng manv để load dữ liệu từ database
            //Lưu ý, để đổ NgaySinh từ DateOnly? thành string với đúng định dạng để dễ Parse lúc lưu thì dùng:
            //NgaySinh = ((DateOnly)employee.NgaySinh).ToDateTime(TimeOnly.MinValue).ToString("ddd/dd/MM/yyyy");
            //Nhớ kiểm tra ngày sinh khác null
            var NV = DataProvider.Instance.DB.Employees.Find(manv);
            if (NV != null)
            {
                MaNV = manv;
                HoTen = NV.HoTen;
                CVu = NV.ChucVu;
                GTinh = NV.GioiTinh;
                NgaySinh = ((DateOnly)NV.NgaySinh).ToDateTime(TimeOnly.MinValue).ToString("ddd/dd/MM/yyyy");
                DiaChi = NV.DiaChi;
                SDT = NV.Sdt;
                Email = NV.Email;
                Luong = (int)NV.Luong;
                ImageLocation = NV.ImageLocation;
                var acc = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == manv).FirstOrDefault();
                if (acc != null)
                {
                    TenDangNhap = acc.TenDn;
                    MatKhau = acc.MatKhau;

                }
                else
                {
                    TenDangNhap = "";
                    MatKhau = "";

                }
            }
            else
            {
                MaNV = manv;
                HoTen = "";
                CVu = "";
                GTinh = "";
                NgaySinh = "";
                DiaChi = "";
                SDT = "";
                Email = "";
                Luong = 0;
                TenDangNhap = "";
                MatKhau = "";
                ImageLocation = "";
            }
        }
        #endregion
    }
}
