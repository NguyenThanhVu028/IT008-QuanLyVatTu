using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.ViewModel
{
    public class NhaCungCapViewModel : BaseViewModel
    {
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã nhà cung cấp", "Tên nhà cung cấp", "Địa chỉ", "Số điện thoại", "Email" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
    }
}
