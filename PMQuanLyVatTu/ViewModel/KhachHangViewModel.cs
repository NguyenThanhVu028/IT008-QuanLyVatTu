using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() {"Mã khách hàng", "Họ và tên", "Số điện thoại", "Giới tính", "Ngày sinh" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
    }
}
