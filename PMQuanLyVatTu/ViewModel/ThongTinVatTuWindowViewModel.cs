using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Win32;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinVatTuWindowViewModel : BaseViewModel
    {
        public ThongTinVatTuWindowViewModel(string mavt = null)
        {
            if (mavt != null)
            {
                EditMode = true;
                Title = "CHỈNH SỬA VẬT TƯ";
                LoadData(mavt);
            }
            else
            {
                EditMode = false;
                EnableEditing = true;
                Title = "THÊM VẬT TƯ";
            }
            LoadNhaCungCap(); LoadKho();

            CloseCommand = new RelayCommand<Window>(CloseWin);
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
        private string _maVT = "";
        private string _tenVatTu = "";
        private string _loaiVT = "";
        private string _donViTinh = "";
        private string _maNCC = "";
        private string _maKho = "";
        private int _giaNhap = 0;
        private int _giaXuat = 0;
        private int _soLuongTonKho = 0;
        private string _imageLocation = "";
        public string MaVT
        {
            get { return _maVT; }
            set { _maVT = value; OnPropertyChanged(); }
        }
        public string TenVatTu
        {
            get { return _tenVatTu; }
            set { _tenVatTu = value; OnPropertyChanged(); }
        }
        public string LoaiVT
        {
            get { return _loaiVT; }
            set { _loaiVT = value; OnPropertyChanged(); }
        }
        public string DonViTinh
        {
            get { return _donViTinh; }
            set { _donViTinh = value; OnPropertyChanged(); }
        }
        public string MaNCC
        {
            get { return _maNCC; }
            set { _maNCC = value; OnPropertyChanged(); }
        }
        public string MaKho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); }
        }
        public int GiaNhap
        {
            get { return _giaNhap; }
            set { _giaNhap = value; OnPropertyChanged(); }
        }
        public int GiaXuat
        {
            get { return _giaXuat; }
            set { _giaXuat = value; OnPropertyChanged(); }
        }
        public int SoLuongTonKho
        {
            get { return _soLuongTonKho; }
            set { _soLuongTonKho = value; OnPropertyChanged(); }
        }
        public string ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; OnPropertyChanged(); }
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
        ObservableCollection<string> _nhaCungCap = new ObservableCollection<string>();
        public ObservableCollection<string> NhaCungCap
        {
            get { return _nhaCungCap; }
            set { _nhaCungCap = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _loaiVatTu = new ObservableCollection<string>() { "NVL", "CC", "TB" };
        public ObservableCollection<string> LoaiVatTu
        {
            get { return _loaiVatTu; }
            set { _loaiVatTu = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _khoChua = new ObservableCollection<string>();
        public ObservableCollection<string> KhoChua
        {
            get { return _khoChua; }
            set { _khoChua = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseCommand { get; set; }
        void CloseWin(Window window)
        {
            EnableEditing = false;
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa vật tư đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                EnableEditing = false;
                //Dùng MaVT để xóa trong database
                var VT = DataProvider.Instance.DB.Supplies.Find(MaVT);
                if (VT != null)
                {
                    VT.DaXoa = true;
                    VT.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                t.Close();
            }
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            CustomMessage msg3 = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn lưu vật tư?", true);
            msg3.ShowDialog();
            if (msg3.ReturnValue == false) return;

            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                //Lưu thông tin trên database theo MaVT
                var VT = DataProvider.Instance.DB.Supplies.Find(MaVT);
                if (VT != null)
                {
                    VT.LoaiVatTu = LoaiVT;
                    VT.MaNcc= MaNCC;
                    VT.MaKho= MaKho;
                    VT.TenVatTu = TenVatTu;
                    VT.DonViTinh = DonViTinh;
                    VT.GiaNhap = GiaNhap;
                    VT.GiaXuat = GiaXuat;
                    VT.SoLuongTonKho = SoLuongTonKho;
                    DataProvider.Instance.DB.SaveChanges();
                }
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                msg.ShowDialog();
            }
            else //Nếu trong chế độ thêm vật tư
            {
                if (MaVT == "") //Chưa nhập mã vật tư
                {
                    CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã vật tư.");
                    msg1.ShowDialog();
                    return;
                }
                if (CompareAndExecute()) //Trùng mã vật tư
                {
                    AlreadyExistsError msg2 = new AlreadyExistsError();
                    msg2.ShowDialog();
                    return;
                }
                // Hợp lệ

                Execute();
                EnableEditing = false;
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm vật tư thành công.");
                msg.ShowDialog();
                (t as Window).Close();
            }
        }
        private bool CompareAndExecute()
        {
            var VT = DataProvider.Instance.DB.Supplies.Find(MaVT);
            if (VT == null)
            {

               return false;
            }
            else
            {
                if(VT.DaXoa == true)
                {
                    DataProvider.Instance.DB.Supplies.Remove(VT);
                    var newVT = new Supply();
                    newVT.LoaiVatTu = LoaiVT;
                    newVT.MaNcc = MaNCC;
                    newVT.MaKho = MaKho;
                    newVT.TenVatTu = TenVatTu;
                    newVT.DonViTinh = DonViTinh;
                    newVT.GiaNhap = GiaNhap;
                    newVT.GiaXuat = GiaXuat;
                    newVT.SoLuongTonKho = SoLuongTonKho;
                    DataProvider.Instance.DB.Supplies.Add(newVT);
                    DataProvider.Instance.DB.SaveChanges();
                    return false;
                }
              
            }
            return true;
        }
        private void Execute()
        {
            var newVT = new Supply();
            newVT.LoaiVatTu = LoaiVT;
            newVT.MaNcc = MaNCC;
            newVT.MaKho = MaKho;
            newVT.TenVatTu = TenVatTu;
            newVT.DonViTinh = DonViTinh;
            newVT.GiaNhap = GiaNhap;
            newVT.GiaXuat = GiaXuat;
            newVT.SoLuongTonKho = SoLuongTonKho;
            DataProvider.Instance.DB.Supplies.Add(newVT);
            DataProvider.Instance.DB.SaveChanges();
        }
        public ICommand ChangeAvatarCommand { get; set; }
        void ChangeAvatar(object t)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG|*.png|JPG|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageLocation = openFileDialog.FileName;
            }
        }
        #endregion
        #region Function
        void LoadData(string mavt)
        {
            //Dùng mavt để load dữ liệu từ database, dùng các property trong "Info" để hiện thông tin
            MaVT = mavt;
            var VT = DataProvider.Instance.DB.Supplies.Find(mavt);
            if (VT != null)
            {
                TenVatTu = VT.TenVatTu;
                LoaiVT =VT.LoaiVatTu;
                DonViTinh = VT.DonViTinh;
                MaNCC = VT.MaNcc;
                MaKho =VT.MaKho;
                GiaNhap = (int)VT.GiaNhap;
                GiaXuat = (int)VT.GiaXuat;
                SoLuongTonKho = (int)VT.SoLuongTonKho;
                ImageLocation = VT.ImageLocation;
            }
            else
            {
                TenVatTu = null;
                LoaiVT = null;
                DonViTinh = null;
                MaNCC = null;
                MaKho = null;
                GiaNhap = 0;
                GiaXuat = 0;
                SoLuongTonKho = 0;
                ImageLocation = null;
            }

        }
        void LoadNhaCungCap()
        {
            NhaCungCap.Clear();
            var ListFromDB = DataProvider.Instance.DB.Suppliers.Where(p => p.DaXoa == false).ToList();
            foreach (var list in ListFromDB)
            {
                NhaCungCap.Add(list.MaNcc);
            }
        }
        void LoadKho()
        {
            KhoChua.Clear();
            var ListFromDB = DataProvider.Instance.DB.Warehouses.Where(p => p.DaXoa == false).ToList();
            foreach (var list in ListFromDB)
            {
                KhoChua.Add(list.MaKho);
            }
        }
        #endregion
    }
}
