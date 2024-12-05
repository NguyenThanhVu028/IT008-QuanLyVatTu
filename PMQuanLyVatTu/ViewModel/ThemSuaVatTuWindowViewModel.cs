using PMQuanLyVatTu.ErrorMessage;
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
    public class ThemSuaVatTuWindowViewModel : BaseViewModel
    {
        public ThemSuaVatTuWindowViewModel(string makho = null)
        {
            if(makho != null) Makho = makho;
            //Danh sach vat tu lay theo ma kho
            DanhSachVatTu.Add("VT0001"); DanhSachVatTu.Add("VT0002"); DanhSachVatTu.Add("VT0003"); DanhSachVatTu.Add("VT0004");

            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            CancelCommand = new RelayCommand<Window>(Cancel);
            ConfirmCommand = new RelayCommand<Window>(Confirm);
        }
        #region Info
        private string _maKho;
        public string Makho
        {
            get { return _maKho; }
            set { _maKho = value; OnPropertyChanged(); }
        }
        private string _maVT="";
        public string MaVT
        {
            get { return _maVT; }
            set { _maVT = value; OnPropertyChanged(); }
        }
        private int _soLuong;
        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }
        private double _chietKhau;
        public double ChietKhau
        {
            get { return _chietKhau; }
            set { _chietKhau = value; OnPropertyChanged(); }
        }
        private double _vAT;
        public double VAT
        {
            get { return _vAT; }
            set { _vAT = value; OnPropertyChanged(); }
        }
        #endregion
        #region ReturnValue
        private bool _returnValue = false;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue  = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _danhSachVatTu = new ObservableCollection<string>();
        public ObservableCollection<string> DanhSachVatTu
        {
            get { return _danhSachVatTu; }
            set { _danhSachVatTu = value; }
        }
        #endregion
        #region Command
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand CancelCommand { get; set; }
        void Cancel(Window window)
        {
            window.Close();
        }
        public ICommand ConfirmCommand { get; set; }
        void Confirm(Window t) {
            if(MaVT == "")
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng chọn mã vật tư.", false);
                msg.ShowDialog();
            }
            else
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin.", false);
                msg.ShowDialog();
                ReturnValue = true;
                t.Close();
            }
        }
        #endregion
    }
}
