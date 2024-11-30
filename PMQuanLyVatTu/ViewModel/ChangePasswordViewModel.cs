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
        public ICommand ConfirmCommand { get; set; }
        void Confirm(object t)
        {
            if (false) //Chưa nhập dữ liệu
            {
                InputError msg = new InputError();
                msg.ShowDialog();
            }
            else if(false) //Sai mật khẩu cũ
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu cũ nhập vào không chính xác. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            else if(false) //Mật khẩu mới trùng với mật khẩu cũ
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu mới phải khác với mật khẩu cũ. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            else
            {
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
    }
}
