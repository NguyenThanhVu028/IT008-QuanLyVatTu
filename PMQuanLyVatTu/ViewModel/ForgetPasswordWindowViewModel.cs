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
        private string _inputEmail="";
        public string InputEmail
        {
            get { return _inputEmail; }
            set { _inputEmail = value; OnPropertyChanged(); }
        }
        public ICommand SendToEmailCommand { get; set; }
        void SendToEmail(object t)
        {
            if (false) //Kiểm tra mail đã được đăng kí chưa
            {
                //Gửi đến Email
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Tên đăng nhập và mật khẩu đã được gửi đến email: " + InputEmail);
                msg.ShowDialog();
            }
            else if(InputEmail == "") //Nếu chưa nhập email
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập email chứng thực.");
                msg.ShowDialog();
            }
            else
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "EMAIL CHƯA ĐƯỢC ĐĂNG KÍ", "Email bạn vừa nhập chưa được đăng kí, vui lòng liên hệ người quản lý để được đăng kí.");
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
