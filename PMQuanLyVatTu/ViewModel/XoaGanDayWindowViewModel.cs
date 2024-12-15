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
    public class XoaGanDayWindowViewModel : BaseViewModel
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
            DeleteAllCommand = new RelayCommand<object>(DeleteAll);
            Refresh();
        }
        #region LoaiDaXoa
        private string _loaiDaXoa = "";
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
            set { _selectedSearchFilter = value; OnPropertyChanged(); Refresh(); }
        }
        private string _searchString = "";
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); Refresh(); }
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
                    var ListVTFromDB = DataProvider.Instance.DB.Supplies.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListVTFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaVt != null)
                                    if (item.MaVt.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaVt, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaVt, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaVt, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "nv":
                    var ListNVFromDB = DataProvider.Instance.DB.Employees.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListNVFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaNv != null)
                                    if (item.MaNv.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaNv, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaNv, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaNv, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "kh":
                    var ListKHFromDB = DataProvider.Instance.DB.Customers.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListKHFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaKh != null)
                                    if (item.MaKh.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaKh, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaKh, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaKh, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "ncc":
                    var ListNCCFromDB = DataProvider.Instance.DB.Suppliers.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListNCCFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaNcc != null)
                                    if (item.MaNcc.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaNcc, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaNcc, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaNcc, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "kho":
                    var ListKHOFromDB = DataProvider.Instance.DB.Warehouses.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListKHOFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaKho != null)
                                    if (item.MaKho.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaKho, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaKho, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaKho, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "ycx":
                    var ListYCXFromDB = DataProvider.Instance.DB.ExportRequests.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListYCXFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaYcx != null)
                                    if (item.MaYcx.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcx, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcx, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcx, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "ycn":
                    var ListYCNFromDB = DataProvider.Instance.DB.ImportRequests.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListYCNFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaYcn != null)
                                    if (item.MaYcn.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcn, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcn, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaYcn, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "pn":
                    var ListPNFromDB = DataProvider.Instance.DB.GoodsReceivedNotes.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListPNFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaPn != null)
                                    if (item.MaPn.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaPn, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaPn, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaPn, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
                case "px":
                    var ListPXFromDB = DataProvider.Instance.DB.GoodsDeliveryNotes.Where(c => c.DaXoa == true).ToList();
                    foreach (var item in ListPXFromDB)
                    {
                        switch (SelectedSearchFilter)
                        {
                            case "Mã đã xóa":
                                if (item.MaPx != null)
                                    if (item.MaPx.Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaPx, item.ThoiGianXoa));
                                break;
                            case "Thời gian xóa":
                                if (item.ThoiGianXoa != null)
                                    if (item.ThoiGianXoa.ToString().Contains(SearchString)) DanhSachDaXoa.Add(new DeletedItem(true, item.MaPx, item.ThoiGianXoa));
                                break;
                            case "Không":
                                DanhSachDaXoa.Add(new DeletedItem(true, item.MaPx, item.ThoiGianXoa));
                                break;
                        }

                    }
                    break;
            }

        }
        public ICommand RecoverCommand { get; set; }
        void Recover(object t)
        {
            int Count = 0;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn khôi phục mục đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    //Kiểm tra loại đã xóa và khôi phục trong database tương ứng
                    case "vt":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.Supplies.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "nv":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.Employees.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "kh":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.Customers.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "ncc":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.Suppliers.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "kho":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.Warehouses.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "ycx":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.ExportRequests.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "ycn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.ImportRequests.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "pn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.GoodsReceivedNotes.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
                        break;
                    case "px":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            if (i.Checked == true)
                            {
                                //Khôi phục bằng MaDaXoa, sửa DaXoa thành 0;
                                var supplie = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(i.MaDaXoa);
                                if (supplie != null)
                                {
                                    supplie.DaXoa = false;
                                    Count++;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                            }
                        }
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
                                var dele = DataProvider.Instance.DB.Supplies.Find(i.MaDaXoa);
                                if (dele != null)
                                    DataProvider.Instance.DB.Supplies.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "nv":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.Employees.Find(i.MaDaXoa);
                                if (dele != null)
                                    DataProvider.Instance.DB.Employees.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "kh":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.Customers.Find(i.MaDaXoa);
                                if (dele != null)
                                    DataProvider.Instance.DB.Customers.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ncc":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.Suppliers.Find(i.MaDaXoa);
                                if (dele != null)
                                    DataProvider.Instance.DB.Suppliers.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "kho":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.Warehouses.Find(i.MaDaXoa);
                                if (dele != null)
                                    DataProvider.Instance.DB.Warehouses.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ycx":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.ExportRequests.Find(i.MaDaXoa);

                                var listdele = DataProvider.Instance.DB.ExportRequestInfos.Where(e => e.MaYcx == i.MaDaXoa).ToList();

                                if (listdele != null)
                                    DataProvider.Instance.DB.ExportRequestInfos.RemoveRange(listdele);

                                if (dele != null)
                                    DataProvider.Instance.DB.ExportRequests.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ycn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.ImportRequests.Find(i.MaDaXoa);

                                if (dele != null)
                                    DataProvider.Instance.DB.ImportRequests.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "pn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.GoodsReceivedNotes.Find(i.MaDaXoa);

                                var listdele = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(e => e.MaPn == i.MaDaXoa).ToList();

                                if(listdele != null)
                                    DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(listdele);

                                if (dele != null)
                                    DataProvider.Instance.DB.GoodsReceivedNotes.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "px":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            MessageBox.Show(i.Checked + i.MaDaXoa);
                            if (i.Checked == true)
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(i.MaDaXoa);

                                var listdele = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(e => e.MaPx == i.MaDaXoa).ToList();

                                if (listdele != null)
                                    DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(listdele);

                                if (dele != null)
                                    DataProvider.Instance.DB.GoodsDeliveryNotes.Remove(dele);
                                Count++;
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                }
                CustomMessage msg2 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã xóa thành công " + Count.ToString() + " mục.");
                msg2.ShowDialog();
                Refresh();
            }
        }
        public ICommand DeleteAllCommand { get; set; }
        void DeleteAll(object t)
        {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa tất cả?",true);
            msg.ShowDialog();

            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    //Kiểm tra loại đã xóa và xóa trong database tương ứng
                    case "vt":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            var dele = DataProvider.Instance.DB.Supplies.Find(i.MaDaXoa);
                            if (dele != null)
                                DataProvider.Instance.DB.Supplies.Remove(dele);
                            
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "nv":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            var dele = DataProvider.Instance.DB.Employees.Find(i.MaDaXoa);
                            if (dele != null)
                                DataProvider.Instance.DB.Employees.Remove(dele);

                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "kh":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            var dele = DataProvider.Instance.DB.Customers.Find(i.MaDaXoa);
                            if (dele != null)
                                DataProvider.Instance.DB.Customers.Remove(dele);

                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ncc":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            var dele = DataProvider.Instance.DB.Suppliers.Find(i.MaDaXoa);
                            if (dele != null)
                                DataProvider.Instance.DB.Suppliers.Remove(dele);

                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "kho":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            var dele = DataProvider.Instance.DB.Warehouses.Find(i.MaDaXoa);
                            if (dele != null)
                                DataProvider.Instance.DB.Warehouses.Remove(dele);

                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ycx":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            //Xóa
                            var dele = DataProvider.Instance.DB.ExportRequests.Find(i.MaDaXoa);

                            var listdele = DataProvider.Instance.DB.ExportRequestInfos.Where(e => e.MaYcx == i.MaDaXoa).ToList();

                            if (listdele != null)
                                DataProvider.Instance.DB.ExportRequestInfos.RemoveRange(listdele);

                            if (dele != null)
                                DataProvider.Instance.DB.ExportRequests.Remove(dele);
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "ycn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.ImportRequests.Find(i.MaDaXoa);

                                if (dele != null)
                                    DataProvider.Instance.DB.ImportRequests.Remove(dele);
                               
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "pn":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.GoodsReceivedNotes.Find(i.MaDaXoa);

                                var listdele = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(e => e.MaPn == i.MaDaXoa).ToList();

                                if (listdele != null)
                                    DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(listdele);

                                if (dele != null)
                                    DataProvider.Instance.DB.GoodsReceivedNotes.Remove(dele);
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
                        break;
                    case "px":
                        foreach (DeletedItem i in DanhSachDaXoa)
                        {
                            {
                                //Xóa
                                var dele = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(i.MaDaXoa);

                                var listdele = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(e => e.MaPx == i.MaDaXoa).ToList();

                                if (listdele != null)
                                    DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(listdele);

                                if (dele != null)
                                    DataProvider.Instance.DB.GoodsDeliveryNotes.Remove(dele);
                            }
                        }
                        DataProvider.Instance.DB.SaveChanges();
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
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/question.png", "THÔNG BÁO", "Bạn có muốn xóa mục đã chọn?", true);
            msg.ShowDialog();
            if (msg.ReturnValue == true)
            {
                switch (LoaiDaXoa)
                {
                    case "vt":
                        var mavt = SelectedDaXoa.MaDaXoa;
                        var vt = DataProvider.Instance.DB.Supplies.Find(mavt);
                        if(vt != null)
                        {
                            DataProvider.Instance.DB.Supplies.Remove(vt);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "nv":
                        var manv = SelectedDaXoa.MaDaXoa;
                        var nv = DataProvider.Instance.DB.Employees.Find(manv);
                        if (nv != null)
                        {
                            DataProvider.Instance.DB.Employees.Remove(nv);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "kh":
                        var makh = SelectedDaXoa.MaDaXoa;
                        var kh = DataProvider.Instance.DB.Customers.Find(makh);
                        if (kh != null)
                        {
                            DataProvider.Instance.DB.Customers.Remove(kh);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "ncc":
                        var mancc = SelectedDaXoa.MaDaXoa;
                        var ncc = DataProvider.Instance.DB.Suppliers.Find(mancc);
                        if (ncc != null)
                        {
                            DataProvider.Instance.DB.Suppliers.Remove(ncc);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "kho":
                        var makho = SelectedDaXoa.MaDaXoa;
                        var kho = DataProvider.Instance.DB.Warehouses.Find(makho);
                        if (kho != null)
                        {
                            DataProvider.Instance.DB.Warehouses.Remove(kho);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "ycx":
                        var maycx = SelectedDaXoa.MaDaXoa;
                        var ycx = DataProvider.Instance.DB.ExportRequests.Find(maycx);
                        var ycxinfo = DataProvider.Instance.DB.ExportRequestInfos.Where(e=> e.MaYcx == maycx).ToList();
                        if (ycx != null)
                        {
                            DataProvider.Instance.DB.ExportRequestInfos.RemoveRange(ycxinfo);
                            DataProvider.Instance.DB.ExportRequests.Remove(ycx);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "ycn":
                        var maycn = SelectedDaXoa.MaDaXoa;
                        var ycn = DataProvider.Instance.DB.ImportRequests.Find(maycn);
                        if (ycn != null)
                        {
                            DataProvider.Instance.DB.ImportRequests.Remove(ycn);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "pn":
                        var mapn = SelectedDaXoa.MaDaXoa;
                        var pn = DataProvider.Instance.DB.GoodsReceivedNotes.Find(mapn);
                        var pninfo = DataProvider.Instance.DB.GoodsReceivedNoteInfos.Where(e => e.MaPn == mapn).ToList();
                        if (pn != null)
                        {
                            DataProvider.Instance.DB.GoodsReceivedNoteInfos.RemoveRange(pninfo);
                            DataProvider.Instance.DB.GoodsReceivedNotes.Remove(pn);
                            DataProvider.Instance.DB.SaveChanges();
                        }
                        break;
                    case "px":
                        var mapx = SelectedDaXoa.MaDaXoa;
                        var px = DataProvider.Instance.DB.GoodsDeliveryNotes.Find(mapx);
                        var pxinfos = DataProvider.Instance.DB.GoodsDeliveryNoteInfos.Where(e => e.MaPx == mapx).ToList();
                        if (px != null)
                        {
                            DataProvider.Instance.DB.GoodsDeliveryNoteInfos.RemoveRange(pxinfos);
                            DataProvider.Instance.DB.GoodsDeliveryNotes.Remove(px);
                            DataProvider.Instance.DB.SaveChanges();
                        }
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
