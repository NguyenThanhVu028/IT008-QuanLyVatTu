using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinNhaCungCapWindowViewModel : BaseViewModel
    {
        public ThongTinNhaCungCapWindowViewModel(string mancc = null)
        {
            if(mancc != null) { EditMode = true; Title = "CHỈNH SỬA NHÀ CUNG CẤP"; LoadData(mancc); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM NHÀ CUNG CẤP"; }

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
        private string _maNCC = "";
        private string _tenNCC = "";
        private string _sDT = "";
        private string _email = "";
        private string _diaChi = "";
        public string MaNCC
        {
            get { return _maNCC; }
            set { _maNCC = value; OnPropertyChanged(); }
        }
        public string TenNCC
        {
            get { return _tenNCC; }
            set { _tenNCC = value; OnPropertyChanged(); }
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa nhà cung cấp đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var NCC = DataProvider.Instance.DB.Suppliers.Find(MaNCC);

                if (NCC != null)
                {
                    NCC.DaXoa = true;
                    NCC.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy nhà cung cấp để xóa!", false);
                }
            }
            t.Close();
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(Window t)
        {
            if (EditMode == true) //Nếu đang chế độ chỉnh sửa
            {
                EnableEditing = false;
                //Lưu thông tin vào database
                var existingSupplier = DataProvider.Instance.DB.Suppliers.Find(MaNCC);

                if (existingSupplier != null)
                {
                    existingSupplier.TenNcc = TenNCC;
                    existingSupplier.DiaChi = DiaChi;
                    existingSupplier.Email = Email;
                    existingSupplier.Sdt = SDT;
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                    msg.ShowDialog();
                }
            }
            else //Nếu trong chế độ thêm nhà cung cấp
            {
                if (string.IsNullOrWhiteSpace(MaNCC)) //Chưa nhập mã nhà cung cấp
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã nhà cung cấp.");
                    msg.ShowDialog();
                }
                else 
                {
                    var existingSuppliers = DataProvider.Instance.DB.Suppliers.Find(MaNCC);

                    if (existingSuppliers != null)
                    {
                        if (existingSuppliers.DaXoa == true) // Hợp lệ
                        {

                            existingSuppliers.DaXoa = false;
                            existingSuppliers.TenNcc = TenNCC;
                            existingSuppliers.Sdt = SDT;
                            existingSuppliers.Email = Email;
                            existingSuppliers.DiaChi = DiaChi;

                            DataProvider.Instance.DB.SaveChanges();
                            CustomMessage msgSuccess1 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhà cung cấp thành công.");
                            msgSuccess1.ShowDialog();
                            t.Close();
                            
                        }
                        else //Trùng mã nhà cung cấp
                        {
                            AlreadyExistsError msg1 = new AlreadyExistsError();
                            msg1.ShowDialog();
                            return;
                        }
                    }
                    else
                    {
                        var newSuppliers = new Supplier
                        {
                            MaNcc = MaNCC,
                            TenNcc = TenNCC,
                            DiaChi = DiaChi,
                            Email = Email,
                            Sdt = SDT,
                            DaXoa = false
                        };
                        DataProvider.Instance.DB.Suppliers.Add(newSuppliers);
                        DataProvider.Instance.DB.SaveChanges();

                        CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhà cung cấp thành công.");
                        msgSuccess.ShowDialog();
                        t.Close();
                    }
                }
            }
        }
        #endregion
        #region Function
        void LoadData(string mancc)
        {
            var NCC = DataProvider.Instance.DB.Suppliers.Find(mancc);
            if (NCC != null)
            {
                MaNCC = NCC.MaNcc;
                TenNCC = (NCC.TenNcc != null)?NCC.TenNcc : "";
                SDT = (NCC.Sdt != null) ? NCC.Sdt : "";
                Email = (NCC.Email != null) ? NCC.Email : "";
                DiaChi = (NCC.DiaChi != null) ? NCC.DiaChi : "";
            }
        }
        #endregion
    }
}
