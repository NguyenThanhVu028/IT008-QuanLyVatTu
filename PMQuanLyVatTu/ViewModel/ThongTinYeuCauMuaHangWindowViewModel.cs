using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ThongTinYeuCauMuaHangWindowViewModel:BaseViewModel
    {
        public ThongTinYeuCauMuaHangWindowViewModel() {
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            AddCommand = new RelayCommand<object>(Add);
            DeleteSelectedCommand = new RelayCommand<object>(DeleteSelected);
        }
        public ICommand CloseWindowCommand { get; set; }
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
        void EditInfo(object t)
        {
            MessageBox.Show("EditInfoCommand Executed");
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            MessageBox.Show("SaveInfoCommand Executed");
        }
        public ICommand AddCommand { get; set; }
        void Add(object t)
        {
            MessageBox.Show("AddCommand Executed");
        }
        public ICommand DeleteSelectedCommand { get; set; }
        void DeleteSelected(object t)
        {
            MessageBox.Show("DeleteSelectedCommand Executed");
        }
    }
}
