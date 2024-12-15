using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;
using PMQuanLyVatTu.ErrorMessage;
using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.User;
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
            bool level1, level2, level3, level4;
            Excute(out level1, out level2, out level3, out level4);

            // Chưa nhập dữ liệu
            if (level1)
            {
                InputError msg = new InputError();
                msg.ShowDialog();
            }
            // Sai mật khẩu cũ
            else if (level2)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu cũ nhập vào không chính xác. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            // Mật khẩu mới trùng với mật khẩu cũ
            else if (level3)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu mới phải khác với mật khẩu cũ. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            // Mật khẩu mới khác với mật khẩu nhập lại
            else if (level4)
            {
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu mới khác với mật khẩu nhập lại. Vui lòng nhập lại.");
                msg.ShowDialog();
            }
            // Hợp lệ
            else
            {
                // Lưu xuống database
                string manv = CurrentUser.Instance.MaNv;
                // Tìm bằng linQ do trường MaNv không phải là khóa chính nên không thể dùng hàm Find()
                var account = DataProvider.Instance.DB.Accounts.FirstOrDefault(a => a.MaNv == manv);
                if (account != null)
                {
                    account.MatKhau = MkMoi;
                    DataProvider.Instance.DB.SaveChanges();
                }
                CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đổi mật khẩu thành công.");
                msg.ShowDialog();
            }
            
        }

        private void Excute(out bool lv1, out bool lv2, out bool lv3, out bool lv4)
        {
            // Khởi tạo giá trị mặc định cho các biến
            lv1 = false;
            lv2 = false;
            lv3 = false;
            lv4 = false;
            // Kiểm tra dữ liệu đầu vào
            if (_mkCu == "" || _mkMoi == "" || _mkMoiAgain == "")
            {
                lv1 = true; // Chưa nhập đủ dữ liệu
                return;
            }

            string manv = CurrentUser.Instance.MaNv;
           // Tìm bằng linQ do trường MaNv không phải là khóa chính nên không thể dùng hàm Find()
            var currentPass = DataProvider.Instance.DB.Accounts.FirstOrDefault(a => a.MaNv == manv)?.MatKhau;

            // Kiểm tra mật khẩu cũ
            if (_mkCu != currentPass)
            {
                lv2 = true; // Sai mật khẩu cũ
            }

            // Kiểm tra mật khẩu mới trùng mật khẩu cũ
            if (_mkMoi == currentPass)
            {
                lv3 = true;
            }

            if(_mkMoi != _mkMoiAgain)
            {
                lv4 = true; 
            }
            //string manv = CurrentUser.Instance.MaNv;
            //var account = DataProvider.Instance.DB.Accounts.Find(manv);
            //account.MatKhau = MkMoi;
        }

        //void Confirm(object t)
        //{
        //    bool flag = false;
        //    if (_mkCu != "" && _mkMoi != "" && _mkMoiAgain != "")
        //    {
        //        flag = false
        //    }
        //    //Chưa nhập dữ liệu
        //    if (false)
        //    {
        //        InputError msg = new InputError();
        //        msg.ShowDialog();
        //    }
        //    //Sai mật khẩu cũ
        //    else if (false)
        //    {
        //        CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu cũ nhập vào không chính xác. Vui lòng nhập lại.");
        //        msg.ShowDialog();
        //    }
        //    //Mật khẩu mới trùng với mật khẩu cũ
        //    else if (false)
        //    {
        //        CustomMessage msg = new CustomMessage("/Material/Images/Icons/wrong.png", "LỖI", "Mật khẩu mới phải khác với mật khẩu cũ. Vui lòng nhập lại.");
        //        msg.ShowDialog();
        //    }
        //    //Hợp lệ
        //    else
        //    {
        //        //Lưu xuống database
        //        CustomMessage msg = new CustomMessage("/Material/Images/Icons/success.png", "THÀNH CÔNG", "Đổi mật khẩu thành công.");
        //        msg.ShowDialog();
        //    }
        //}
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
