using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PMQuanLyVatTu.ErrorMessage;

namespace PMQuanLyVatTu.ViewModel
{
    public class ForgetPasswordWindowViewModel : BaseViewModel
    {
        public ForgetPasswordWindowViewModel() {
            SendToEmailCommand = new RelayCommand<object>(SendToEmail);
            CancelCommand = new RelayCommand<Window>(CloseWindow);
            MoveWindowCommand = new RelayCommand<Window>(MoveWindow);
        }
        public ICommand SendToEmailCommand { get; set; }
        void SendToEmail(object t)
        {
            if (true) //Kiểm tra mail đã được đăng kí chưa
            {
                //...
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Tên đăng nhập và mật khẩu đã được gửi đến email");
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
