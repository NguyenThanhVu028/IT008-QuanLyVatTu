using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class YeuCauMuaHangViewModel : BaseViewModel
    {
        public YeuCauMuaHangViewModel()
        {
            RefreshCommand = new RelayCommand<object>(Refresh);
            Refresh();
        }
        #region Data for SelectionList
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã yêu cầu mua", "Mã nhân viên", "Mã khách hàng", "Ngày lập yêu cầu", "Trạng thái" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
        #endregion
        #region SearchBar
        private string _selectedSearchFilter = "Không";
        public string SelectedSearchFilter
        {
            get { return _selectedSearchFilter; }
            set { _selectedSearchFilter = value; OnPropertyChanged(); }
        }
        private string _searchString;
        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(); Refresh(); }
        }
        #endregion
        #region DanhSachYeuCauXuatHang
        #endregion
        #region Command
        public ICommand RefreshCommand { get; set; }
        void Refresh(object t = null)
        {

        }
        #endregion
        public class ExportRequest
        {

        }
    }
}
