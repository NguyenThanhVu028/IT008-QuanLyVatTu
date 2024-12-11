using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyVatTu.User
{
    public class CurrentUser
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
            set { _maNv = value; }
        }
        private string _chucVu = "";
        public string ChucVu
        {
            get { return _chucVu; }
            set { _chucVu = value; }
        }
        private string _hoTen = "";
        public string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }
        private string _imageLocation = "";
        public string ImageLocation
        {
            get { return _imageLocation; }
            set { _imageLocation = value; }
        }
    }
}
