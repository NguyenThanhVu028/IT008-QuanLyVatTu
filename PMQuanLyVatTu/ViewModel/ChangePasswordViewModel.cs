using PMQuanLyVatTu.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMQuanLyVatTu.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ChangePasswordViewModel()
        {
            ConfirmCommand = new RelayCommand<object>(Confirm);
            CancelCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        #region Info
        private string _mkCu = "";
        private string _mkMoi = "";
        private string _mkMoiAgain = "";
        public string MkCu
        {
            get { return _mkCu; }
            set { _mkCu = value; OnPropertyChanged(); }
        }
        public string MkMoi
        {
            get { return _mkMoi; }
            set { _mkMoi = value; OnPropertyChanged(); }
        }
        public string MkMoiAgain
        {
            get { return _mkMoiAgain; }
            set { _mkMoiAgain = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand ConfirmCommand { get; set; }
        void Confirm(object t)
        {
            //Chưa nhập dữ liệu
            if (false) 
            {
                InputError msg = new InputError();
                msg.ShowDialog();
            }
            //Sai mật khẩu cũ
            else if (false)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu cũ nhập vào không chính xác. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            //Mật khẩu mới trùng với mật khẩu cũ
            else if (false) 
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu mới phải khác với mật khẩu cũ. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            //Hợp lệ
            else
            {
                //Lưu xuống database
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đổi mật khẩu thành công.");
                msg.ShowDialog();
            }
        }
        public ICommand CancelCommand { get; set; }
        void CloseWindow(Window uc)
        {
            uc.Close();
        }
        public ICommand MoveWindowCommand { get; set; }
        void MoveWindow(Window window)
        {
            window.DragMove();
        }
        #endregion
    }
}
