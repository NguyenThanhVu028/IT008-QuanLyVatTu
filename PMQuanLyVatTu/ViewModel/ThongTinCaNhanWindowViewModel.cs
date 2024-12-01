using PMQuanLyVatTu.ErrorMessage;
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
        public ThongTinCaNhanWindowViewModel(string MaNV)
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
            EditInfoCommand = new RelayCommand<object>(EditInfo);
            SaveInfoCommand = new RelayCommand<object>(SaveInfo);
            ChangePasswordCommand = new RelayCommand<object>(ChangePassword);
        }
        #region EnableEditing
        private bool _enableEditing = false;
        public bool EnableEditing
        {
            get { return _enableEditing; }
            set { _enableEditing = value; OnPropertyChanged(); }
        }
        #endregion
        #region Data for SelectionList
        private ObservableCollection<string> _gioiTinh = new ObservableCollection<string>() { "Nam", "Nữ" };
        public ObservableCollection<string> GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand CloseWindowCommand { get; set; }
        void CloseWindow(Window window)
        {
            EnableEditing = false;
            window.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public ICommand EditInfoCommand { get; set; }
        void EditInfo(object t) {
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã vào chế độ chỉnh sửa thông tin.");
            msg.ShowDialog();
            if(msg.ReturnValue == true) EnableEditing = true;
        }
        public ICommand SaveInfoCommand { get; set; }
        void SaveInfo(object t)
        {
            EnableEditing = false;
            CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đã lưu thông tin chỉnh sửa.");
            msg.ShowDialog();
        }
        public ICommand ChangePasswordCommand { get; set; }
        void ChangePassword(object t)
        {
            ChangePasswordWindows changePasswordWindows = new ChangePasswordWindows();
            changePasswordWindows.ShowDialog();
        }
        #endregion
        class Employee
        {

        }
    }
}
