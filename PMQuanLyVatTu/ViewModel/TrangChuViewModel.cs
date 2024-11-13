using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PMQuanLyVatTu.ViewModel
{
    public class TrangChuViewModel: BaseViewModel
    {
        private string _now;
        private string _imgIcon;
        public string TimeNow
        {
            get
            {
                return _now;
            }
            set
            {
                _now = value;
                OnPropertyChanged();
            }
        }
        public string ImgIcon
        {
            get
            {
                return _imgIcon;
            }
            set
            {
                _imgIcon = value; OnPropertyChanged();
            }
        }
        public TrangChuViewModel()
        {
            _now = DateTime.Now.ToShortTimeString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += TimerTick;
            timer.Start();
        }
        void TimerTick(object sender, EventArgs e)
        {
            TimeNow = DateTime.Now.ToShortTimeString();
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 6) ImgIcon = "/Material/Images/Icons/sun_rise.png";
            else if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 18) ImgIcon = "/Material/Images/Icons/noon.png";
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 19) ImgIcon = "/Material/Images/Icons/sun_set.png";
            else ImgIcon = "/Material/Images/Icons/moon.png";
        }
    }
}
