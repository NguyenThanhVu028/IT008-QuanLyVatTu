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
    public class ThongTinVatTuWindowViewModel : BaseViewModel
    {
        ObservableCollection<string> _nhaCungCap = new ObservableCollection<string>() {"NCC1", "NCC2", "NCC3", "NCC4" };
        public ObservableCollection<string> NhaCungCap
        {
            get { return _nhaCungCap; }
            set { _nhaCungCap = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _loaiVatTu = new ObservableCollection<string>() {"NVL", "CC", "TB" };
        public ObservableCollection<string> LoaiVatTu
        {
            get { return _loaiVatTu; }
            set { _loaiVatTu = value; OnPropertyChanged(); }
        }
        ObservableCollection<string> _khoChua = new ObservableCollection<string>() { "KHO1", "KHO2", "KHO3" };
        public ObservableCollection<string> KhoChua
        {
            get { return _khoChua; }
            set { _khoChua = value; OnPropertyChanged(); }
        }
        public ICommand CloseCommand { get; set; }
        public ThongTinVatTuWindowViewModel()
        {
            CloseCommand = new RelayCommand<Window>(CloseWin);
        }
        void CloseWin(Window window)
        {
            window.Close();
        }
    }
}
