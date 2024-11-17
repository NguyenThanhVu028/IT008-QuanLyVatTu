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
    public class ThongTinNhanVienWindowViewModel : BaseViewModel
    {
        public ThongTinNhanVienWindowViewModel()
        {
            CloseCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            ChangeAvatarCommand = new RelayCommand<object>(ChangeAvatar);
        }
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() {"Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _chucVu = new ObservableCollection<string>() { "QL", "KT", "NX", "TN" };
        public ObservableCollection<string> ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        public ICommand CloseCommand { get; set; }
        void CloseWindow(Window window)
        {
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand EditInfoCommand { get; set; }
        void EditInfo(object t) {
            MessageBox.Show("EditInfoCommand Executed");
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            MessageBox.Show("SaveInfoCommand Executed");
        }
        public ICommand ChangeAvatarCommand { get; set; }
        void ChangeAvatar(object t)
        {
            MessageBox.Show("Change image location");
        }
    }
}
