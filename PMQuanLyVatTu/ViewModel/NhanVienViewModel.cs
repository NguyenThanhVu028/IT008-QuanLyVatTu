using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() {"Mã nhân viên", "Họ và tên", "Chức vụ", "Giới tính", "Chức vụ", "Ngày sinh", "Số điện thoại", "Email", "Địa chỉ", "Lương" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
    }
}
