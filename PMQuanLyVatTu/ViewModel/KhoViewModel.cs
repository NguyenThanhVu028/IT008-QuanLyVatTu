using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.ViewModel
{
    public class KhoViewModel : BaseViewModel
    {
        private ObservableCollection<string> _searchFilter = new ObservableCollection<string>() { "Mã kho", "Loại vật tư", "Địa chỉ" };
        public ObservableCollection<string> SearchFilter
        {
            get { return _searchFilter; }
            set { _searchFilter = value; OnPropertyChanged(); }
        }
    }
}
