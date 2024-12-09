using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using System.Net.Mail;

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
            string TDN="", MK="";
            if(InputEmail == "") //Nếu chưa nhập email
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Vui lòng nhập email chứng thực.");
                msg.ShowDialog();
                return;
            }
            bool Check = false;
            var ListFromDB = DataProvider.Instance.DB.Accounts.ToList();
            foreach (var item in ListFromDB)
            {
                if(item.Email == InputEmail)
                {
                    TDN = item.TenDn; MK = item.MatKhau;
                    Check = true;
                }
            }
            if (Check)
            {
                    var Client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new System.Net.NetworkCredential("taikhoanhoctap142@gmail.com", "Nguyenthanhvu142857"),
                        EnableSsl = true
                    };
                    MailMessage msg = new MailMessage("taikhoanhoctap142@gmail.com", "23521811@gm.uit.edu.vn");
                    msg.Subject = "Lấy lại tên đăng nhập và mật khẩu.";
                    msg.Body = "Bạn đã lấy lại thành công tên đăng nhập và mật khẩu.\n Tên đăng nhập: " + TDN + "\n Mật khẩu: " + MK;

                    Client.Send(msg);
                
                CustomMessage msg1 = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Tên đăng nhập và mật khẩu đã được gửi đến email: " + InputEmail);
                msg1.ShowDialog();
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
