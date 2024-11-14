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
    public class ThongTinCaNhanWindowViewModel : BaseViewModel
    {
        public ThongTinCaNhanWindowViewModel()
        {
            CloseCommand = new RelayCommand<Window>(CloseWindow);
        }
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() { "Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        public ICommand CloseCommand { get; set; }
        void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
