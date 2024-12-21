using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinKhoWindowViewModel : BaseViewModel
    {
        public ThongTinKhoWindowViewModel(string makho = null)
        {
            if(makho != null) { EditMode = true; Title = "CHỈNH SỬA KHO"; LoadData(makho); }
            else { EnableEditing = true; EditMode = false; Title = "THÊM KHO"; }

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
        private string _maKho = "";
        private string _loaiVT = "";
        private string _diaChi = "";
        public string MaKho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); }
        }
        public string LoaiVT
        {
            get { return _loaiVT; }
            set { _loaiVT = value; OnPropertyChanged(); }
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
        ObservableCollection<string> _loaiVatTu = new ObservableCollection<string>() { "NVL", "CC", "TB" };
        public ObservableCollection<string> LoaiVatTu
        {
            get { return _loaiVatTu; }
            set { _loaiVatTu = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window) {
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa kho đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                var Kho = DataProvider.Instance.DB.Warehouses.Find(MaKho);

                if (Kho != null)
                {
                    Kho.DaXoa = true;
                    Kho.ThoiGianXoa = DateTime.Now;
                    DataProvider.Instance.DB.SaveChanges();
                }
                else
                {
                    msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Không tìm thấy kho để xóa!", false);
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
                var existingWarehouses = DataProvider.Instance.DB.Warehouses.Find(MaKho);

                if (existingWarehouses != null)
                {
                    existingWarehouses.LoaiVatTu = LoaiVT;
                    existingWarehouses.DiaChi = DiaChi;
                    DataProvider.Instance.DB.SaveChanges();

                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
                    msg.ShowDialog();
                }
            }
            else //Nếu trong chế độ thêm kho
            {
                if (string.IsNullOrWhiteSpace(MaKho)) //Chưa nhập mã nhà kho
                {
                    CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập mã kho.");
                    msg.ShowDialog();
                }
                else
                {
                    var existingWarehouses = DataProvider.Instance.DB.Warehouses.Find(MaKho);

                    if (existingWarehouses != null)
                    {
                        if (existingWarehouses.DaXoa == true) // Hợp lệ
                        {
                            existingWarehouses.DaXoa = false;
                            existingWarehouses.LoaiVatTu = LoaiVT;
                            existingWarehouses.DiaChi = DiaChi;
                            DataProvider.Instance.DB.SaveChanges();
                            CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhà kho thành công.");
                            msgSuccess.ShowDialog();
                            t.Close();
                        }
                        else //Trùng mã kho
                        {
                            AlreadyExistsError msg1 = new AlreadyExistsError();
                            msg1.ShowDialog();
                            return;
                        }
                    }
                    else
                    {
                        var newWarehouses = new Warehouse
                        {
                            MaKho = MaKho,
                            LoaiVatTu = LoaiVT,
                            DiaChi = DiaChi,
                            DaXoa = false
                        };
                        DataProvider.Instance.DB.Warehouses.Add(newWarehouses);
                        DataProvider.Instance.DB.SaveChanges();
                        CustomMessage msgSuccess = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Thêm nhà kho thành công.");
                        msgSuccess.ShowDialog();
                        t.Close();
                    }
                }
            }
        }
        #endregion
        #region Function
        void LoadData(string s)
        {
            var Kho = DataProvider.Instance.DB.Warehouses.Find(s);
            if (Kho != null)
            {
                MaKho = s;
                LoaiVT = (Kho.LoaiVatTu != null) ? Kho.LoaiVatTu : "";
                DiaChi = (Kho.DiaChi != null) ? Kho.DiaChi : "";
            }
        }
        #endregion
    }
}
