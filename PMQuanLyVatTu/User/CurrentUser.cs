using PMQuanLyVatTu.Models;
using PMQuanLyVatTu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.User
{
    public class CurrentUser : BaseViewModel
    {
        private static CurrentUser _instance;
        public static CurrentUser Instance
        {
            get
            {
                if (_instance == null) { _instance = new CurrentUser(); }
                return _instance;
            }
            set { _instance = value; }
        }
        private string _maNv = "";
        public string MaNv
        {
            get { return _maNv; }
            set { _maNv = value; OnPropertyChanged(); }
        }
        private string _chucVu = "";
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; OnPropertyChanged(); }
        }
        private string _hoTen = "";
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; OnPropertyChanged(); }
        }
        private string _imageLocation = "";
        public string ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; OnPropertyChanged(); }
        }
        public void Update(Employee e)
        {
            if (e == null) return;
            MaNv = e.MaNv;
            ChucVu = e.ChucVu ?? "";
            HoTen = e.HoTen ?? "";
            ImageLocation = e.ImageLocation ?? "";
        } 
    }
}
