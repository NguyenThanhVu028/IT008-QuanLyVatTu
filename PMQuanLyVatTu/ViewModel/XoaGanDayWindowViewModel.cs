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
    public class XoaGanDayWindowViewModel:BaseViewModel
    {
        public XoaGanDayWindowViewModel(string loai)
        {
            LoaiDaXoa = loai;

            CloseCommand = new RelayCommand<Window>(Close);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            RefreshCommand = new RelayCommand<object>(Refresh);
            RecoverCommand = new RelayCommand<object>(Recover);
            DeleteButtonCommand = new RelayCommand<object>(DeleteButton);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
            DeleteAllCommand =  new RelayCommand<object>(DeleteAll);
            Refresh();
        }
        #region LoaiDaXoa
        private string _loaiDaXoa;
        public string LoaiDaXoa
        {
            get { return _loaiDaXoa; }
            set { _loaiDaXoa = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Không", "Mã đã xóa", "Thời gian xóa" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
        #endregion
        #region DanhSachDaXoa
        private ObservableCollection<DeletedItem> _danhSachDaXoa = new ObservableCollection<DeletedItem>();
        public ObservableCollection<DeletedItem> DanhSachDaXoa
        {
            get { return _danhSachDaXoa; }
            set { _danhSachDaXoa = value; }
        }
        private DeletedItem _selectedDaXoa;
        public DeletedItem SelectedDaXoa
        {
            get { return _selectedDaXoa; }
            set
            {
                _selectedDaXoa = value; OnPropertyChanged();
            }
        }
        #endregion
        #region SearchBar
        private string _selectedSearchFilter = "Không";
        public string SelectedSearchFilter
        {
            get { return _selectedSearchFilter; }
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); MessageBox.Show("Changed"); }
        }
        private string _searchString = "";
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); Refresh(); MessageBox.Show("Changed"); }
        }
        #endregion
        #region Command
        public ICommand CloseCommand { get; set; }
        void Close(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand RefreshCommand { get; set; }
        public void Refresh(object t = null)
        {
            DanhSachDaXoa.Clear();
            switch (LoaiDaXoa)
            {
                //Kiểm tra loại đã xóa và đổ dữ liệu tương ứng lên
                case "vt":
                    
                    break;
                case "nv":
                    
                    break;
                case "kh":
                    
                    break;
                case "ncc":
                    
                    break;
                case "kho":
                    
                    break;
                case "ycx":
                    
                    break;
                case "ycn":
                    
                    break;
                case "pn":
                    
                    break;
                case "px":
                    
                    break;
            }
            
        }
        public ICommand RecoverCommand { get; set; }
        void Recover(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn khôi phục mục đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    //Kiểm tra loại đã xóa và khôi phục trong database tương ứng
                    case "vt":
                        foreach(DeletedItem i in DanhSachDaXoa)
                        {
                            if(i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                Count++;
                            }
                        }
                        break;
                    case "nv":

                        break;
                    case "kh":

                        break;
                    case "ncc":

                        break;
                    case "kho":

                        break;
                    case "ycx":

                        break;
                    case "ycn":

                        break;
                    case "pn":

                        break;
                    case "px":

                        break;
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã khôi phục thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                Refresh();
            }
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    //Kiểm tra loại đã xóa và khôi phục trong database tương ứng
                    case "vt":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                Count++;
                            }
                        }
                        break;
                    case "nv":

                        break;
                    case "kh":

                        break;
                    case "ncc":

                        break;
                    case "kho":

                        break;
                    case "ycx":

                        break;
                    case "ycn":

                        break;
                    case "pn":

                        break;
                    case "px":

                        break;
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                Refresh();
            }
        }
        public ICommand DeleteAllCommand {  get; set; }
        void DeleteAll(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa tất cả?");
            msg.ShowDialog();

            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    //Kiểm tra loại đã xóa và xóa trong database tương ứng
                    case "vt":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            //Xóa
                        }
                        break;
                    case "nv":

                        break;
                    case "kh":

                        break;
                    case "ncc":

                        break;
                    case "kho":

                        break;
                    case "ycx":

                        break;
                    case "ycn":

                        break;
                    case "pn":

                        break;
                    case "px":

                        break;
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa tất cả thành công.");
                msg2.ShowDialog();
                Refresh();
            }
        }
        public ICommand DeleteButtonCommand { get; set; }
        void DeleteButton(object t)
        {
            //Xóa theo SelectedDaXoa;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?");
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                switch(LoaiDaXoa)
                {
                    case "vt":

                        break;
                    case "nv":

                        break;
                    case "kh":

                        break;
                    case "ncc":

                        break;
                    case "kho":

                        break;
                    case "ycx":

                        break;
                    case "ycn":

                        break;
                    case "pn":

                        break;
                    case "px":

                        break;
                }
                Refresh();
            }
        }
        #endregion
    }
    public class DeletedItem
    {
        public DeletedItem(bool ck = false, string madx = "", DateTime? tg = null)
        {
            Checked = ck; MaDaXoa = madx; ThoiGianXoa = tg;
            string s = "";
        }
        public bool Checked { get; set; }
        public string MaDaXoa { get; set; }
        public DateTime? ThoiGianXoa { get; set; }
    }
}
