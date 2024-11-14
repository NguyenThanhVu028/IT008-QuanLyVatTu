using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.ViewModel
{
    public class VatTuViewModel:BaseViewModel
    {
        public VatTuViewModel() { }
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã vật tư", "Tên vật tư", "Loại vật tư", "Đơn vị tính", "Mã nhà cung cấp", "Mã kho chứa"};
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
    }
}
