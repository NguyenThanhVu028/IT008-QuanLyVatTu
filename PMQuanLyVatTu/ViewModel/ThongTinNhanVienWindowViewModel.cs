using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            SaveInfoCommand = new RelayCommand<Window>(SaveInfo);
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
        private ObservableCollection<string> _chucVu = new ObservableCollection<string>() { "Quản Lý", "Kế Toán", "Nhập Xuất", "Tiếp Nhận" };
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
                        account.ThoiGianXoa = DateTime.Now;
                    }

                    if (nv != null)
                    {
                        nv.DaXoa = true;
                        nv.ThoiGianXoa = DateTime.Now;
                    }
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    CustomMessage message = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không thể xóa nhân viên hiện tại.");
                    message.ShowDialog();
                    return;
                }
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(Window t)
        {
            if (MaNV.IsNullOrEmpty()) //Chưa nhập mã nhân viên
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã nhân viên.");
                msg.ShowDialog();
                return;
            }
            if (CVu.IsNullOrEmpty())
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn chức vụ.");
                msg.ShowDialog();
                return;
            }

            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                var nv = DataProvider.Instance.DB.Employees.Find(MaNV);
                if(nv == null)
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Nhân viên đã không còn trên hệ thống.");
                    msg1.ShowDialog();
                    return;
                }

                nv.HoTen = HoTen;
                nv.ChucVu = (CVu);
                try { nv.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy"); }
                catch { nv.NgaySinh = DateOnly.FromDateTime(DateTime.Now); }
                nv.GioiTinh = GTinh;
                nv.DiaChi = DiaChi;
                nv.Sdt = SDT;
                nv.Email = Email;
                nv.Luong = Luong;
                nv.ImageLocation = ImageLocation;

                if (TenDangNhap.IsNullOrEmpty()) //Để trống thì xóa TK của NV này
                {
                    var acc = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == nv.MaNv && e.DaXoa == false).ToList();
                    if (acc != null)
                    {
                        foreach(var item in acc)
                        {
                            item.DaXoa = true; item.ThoiGianXoa = DateTime.Now;
                        }     
                    }
                }
                else
                {
                    var acc = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == nv.MaNv && e.DaXoa == false).FirstOrDefault();
                    var result = DataProvider.Instance.DB.Accounts.Find(TenDangNhap);
                    if(result != null)
                    {
                        if(result.DaXoa == true)
                        {
                            if(result.MaNv == MaNV)
                            {
                                result.DaXoa = false;
                                result.MatKhau = MatKhau;
                                result.MaNv = MaNV;
                                result.Email = Email;
                            }
                            else
                            {
                                DataProvider.Instance.DB.Accounts.Remove(result);
                                if (acc == null)
                                {
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
                                    acc.TenDn = TenDangNhap;
                                    acc.MatKhau = MatKhau;
                                    acc.Email = Email;
                                    acc.DaXoa = false;
                                }
                            }
                        }
                        else if(result.DaXoa == false)
                        {
                            if(result.MaNv == MaNV)
                            {
                                result.MatKhau = MatKhau;
                                DataProvider.Instance.DB.SaveChanges();
                            }
                            else
                            {
                                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Tên đăng nhập này đã tồn tại trên hệ thống.");
                                msg1.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        if(acc == null)
                        {
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
                            acc.TenDn = TenDangNhap;
                            acc.MatKhau = MatKhau;
                            acc.Email = Email;
                            acc.DaXoa = false;
                        }
                    }
                }
                DataProvider.Instance.DB.SaveChanges();

                if (CurrentUser.Instance.MaNv == MaNV)
                {
                    CurrentUser.Instance.Update(nv);
                }
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                EnableEditing = false;
                msg.ShowDialog();
            }

            else //Nếu trong chế độ thêm nhân viên
            {
                var NV = DataProvider.Instance.DB.Employees.Find(MaNV);
                if (NV != null) //Trùng mã nhân viên
                {
                    if(NV.DaXoa == true)
                    {
                        NV.DaXoa = false;
                        NV.ImageLocation = ImageLocation;
                        NV.HoTen = HoTen;
                        NV.ChucVu = (CVu);
                        NV.GioiTinh = GTinh;
                        try { NV.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy"); }
                        catch { NV.NgaySinh = DateOnly.FromDateTime(DateTime.Now); }
                        NV.DiaChi = DiaChi;
                        NV.Sdt = SDT;
                        NV.Email = Email;
                        NV.Luong = Luong;

                        if (!TenDangNhap.IsNullOrEmpty())
                        {
                            var result = DataProvider.Instance.DB.Accounts.Find(TenDangNhap);
                            if (result != null)
                            {
                                if (result.DaXoa == true)
                                {
                                    if (result.MaNv == MaNV)
                                    {
                                        result.DaXoa = false;
                                        result.MatKhau = MatKhau;
                                        result.MaNv = MaNV;
                                        result.Email = Email;
                                    }
                                    else
                                    {
                                        DataProvider.Instance.DB.Accounts.Remove(result);
                                        var newacc = new Account();
                                        newacc.TenDn = TenDangNhap;
                                        newacc.MatKhau = MatKhau;
                                        newacc.MaNv = MaNV;
                                        newacc.Email = Email;
                                        newacc.DaXoa = false;
                                        DataProvider.Instance.DB.Accounts.Add(newacc);
                                    }

                                }
                                else if (result.DaXoa == false)
                                {
                                    if (result.MaNv == MaNV)
                                    {
                                        result.MatKhau = MatKhau;
                                        DataProvider.Instance.DB.SaveChanges();
                                    }
                                    else
                                    {
                                        CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Tên đăng nhập này đã tồn tại trên hệ thống.");
                                        msg1.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                var newacc = new Account();
                                newacc.TenDn = TenDangNhap;
                                newacc.MatKhau = MatKhau;
                                newacc.MaNv = MaNV;
                                newacc.Email = Email;
                                newacc.DaXoa = false;
                                DataProvider.Instance.DB.Accounts.Add(newacc);
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhân viên thành công.");
                        msg.ShowDialog();
                        t.Close();
                    }
                    else
                    {
                        AlreadyExistsError msg = new AlreadyExistsError();
                        msg.ShowDialog();
                        return;
                    }
                }
                else // Hợp lệ
                {
                    var newNV = new Employee();

                    newNV.MaNv = MaNV;
                    newNV.HoTen = HoTen;
                    newNV.ChucVu = (CVu);
                    newNV.GioiTinh = GTinh;
                    newNV.DiaChi = DiaChi;
                    newNV.Sdt = SDT;
                    newNV.Email = Email;
                    newNV.Luong = Luong;
                    newNV.ImageLocation = ImageLocation;
                    newNV.DaXoa = false;
                    try { newNV.NgaySinh = DateOnly.ParseExact(NgaySinh, "ddd/dd/MM/yyyy"); }
                    catch { newNV.NgaySinh = DateOnly.FromDateTime(DateTime.Now); }

                    DataProvider.Instance.DB.Employees.Add(newNV);

                    if (!TenDangNhap.IsNullOrEmpty())
                    {
                        var result = DataProvider.Instance.DB.Accounts.Find(TenDangNhap);
                        if (result != null)
                        {
                            if (result.DaXoa == true || result.MaNv == MaNV)
                            {
                                if (result.MaNv == MaNV)
                                {
                                    result.DaXoa = false;
                                    result.MatKhau = MatKhau;
                                    result.MaNv = MaNV;
                                    result.Email = Email;
                                }
                                else
                                {
                                    DataProvider.Instance.DB.Accounts.Remove(result);
                                    var newacc = new Account();
                                    newacc.TenDn = TenDangNhap;
                                    newacc.MatKhau = MatKhau;
                                    newacc.MaNv = MaNV;
                                    newacc.Email = Email;
                                    newacc.DaXoa = false;
                                    DataProvider.Instance.DB.Accounts.Add(newacc);
                                }
                            }
                            else if (result.DaXoa == false)
                            {
                                if (result.MaNv == MaNV)
                                {
                                    result.MatKhau = MatKhau;
                                    DataProvider.Instance.DB.SaveChanges();
                                }
                                else
                                {
                                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Tên đăng nhập này đã tồn tại trên hệ thống.");
                                    msg1.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            var newacc = new Account();
                            newacc.TenDn = TenDangNhap;
                            newacc.MatKhau = MatKhau;
                            newacc.MaNv = MaNV;
                            newacc.Email = Email;
                            newacc.DaXoa = false;
                            DataProvider.Instance.DB.Accounts.Add(newacc);
                        }
                    }

                    DataProvider.Instance.DB.SaveChanges();
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhân viên thành công.");
                    msg.ShowDialog();
                    t.Close();
                }
            }
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
        string ConvertChucVu(string cv)
        {
            switch (cv)
            {
                case "QL":
                    return "Quản Lý";
                case "KT":
                    return "Kế Toán";
                case "NX":
                    return "Nhập Xuất";
                case "TN":
                    return "Tiếp nhận";
                default:
                    return "";
            }
        }
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
                HoTen = NV.HoTen ?? "";
                CVu = NV.ChucVu ?? "";
                GTinh = NV.GioiTinh ?? "";
                NgaySinh = ((DateOnly)NV.NgaySinh).ToDateTime(TimeOnly.MinValue).ToString("ddd/dd/MM/yyyy");
                DiaChi = NV.DiaChi ?? "";
                SDT = NV.Sdt ?? "";
                Email = NV.Email ?? "";
                Luong = (NV.Luong ?? 0);
                ImageLocation = NV.ImageLocation ?? "";
                var acc = DataProvider.Instance.DB.Accounts.Where(e => e.MaNv == manv && e.DaXoa == false).FirstOrDefault();
                if (acc != null)
                {
                    TenDangNhap = acc.TenDn;
                    MatKhau = acc.MatKhau ?? "";
                }
                else
                {
                    TenDangNhap = String.Empty;
                    MatKhau = String.Empty;
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
