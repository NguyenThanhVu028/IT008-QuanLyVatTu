using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace PMQuanLyVatTu.ViewModel
{
    public class TrangChuViewModel: BaseViewModel
    {
        //#region TimeOfDay section
        //private string _now;
        //private string _imgIcon;
        //private LinearGradientBrush _timeOfDayColor;
        //public string TimeNow
        //{
        //    get
        //    {
        //        return _now;
        //    }
        //    set
        //    {
        //        _now = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string DateNow
        //{
        //    get { return _now; }
        //    set { _now = value; OnPropertyChanged(); }
        //}
        //public LinearGradientBrush TimeOfDayColor
        //{
        //    get => _timeOfDayColor;
        //    set
        //    {
        //        _timeOfDayColor = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string ImgIcon
        //{
        //    get
        //    {
        //        return _imgIcon;
        //    }
        //    set
        //    {
        //        _imgIcon = value; OnPropertyChanged();
        //    }
        //}
        //void TimerTick(object sender, EventArgs e)
        //{
        //    TimeNow = DateTime.Now.ToShortTimeString();
        //    DateNow = DateTime.Now.ToShortDateString();
        //    if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 6)
        //    {
        //        ImgIcon = "/Material/Images/Icons/sun_rise.png";
        //        LinearGradientBrush GradBrush = new LinearGradientBrush();
        //        GradBrush.StartPoint = new System.Windows.Point(0.5, 0);
        //        GradBrush.EndPoint = new System.Windows.Point(0.5, 1);
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF5600FF")), 0.17));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFFFAF00")), 0.85));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFF0FFA0")), 1));
        //        GradBrush.Opacity = 0.3;
        //        TimeOfDayColor = GradBrush;
        //    }
        //    else if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 17)
        //    {
        //        ImgIcon = "/Material/Images/Icons/noon.png";
        //        LinearGradientBrush GradBrush = new LinearGradientBrush();
        //        GradBrush.StartPoint = new System.Windows.Point(0.5, 0);
        //        GradBrush.EndPoint = new System.Windows.Point(0.5, 1);
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF0062FF")), 0));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF00BEFF")), 0.54));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFA0E7FF")), 1));
        //        GradBrush.Opacity = 0.7;
        //        TimeOfDayColor = GradBrush;
        //    }
        //    else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 18)
        //    {
        //        ImgIcon = "/Material/Images/Icons/sun_set.png";
        //        LinearGradientBrush GradBrush = new LinearGradientBrush();
        //        GradBrush.StartPoint = new System.Windows.Point(0.5, 0);
        //        GradBrush.EndPoint = new System.Windows.Point(0.5, 1);
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF5600FF")), 0.17));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFFFAF00")), 0.85));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFF0FFA0")), 1));
        //        GradBrush.Opacity = 0.7;
        //        TimeOfDayColor = GradBrush;
        //    }
        //    else
        //    {
        //        ImgIcon = "/Material/Images/Icons/moon.png";
        //        LinearGradientBrush GradBrush = new LinearGradientBrush();
        //        GradBrush.StartPoint = new System.Windows.Point(0.5, 0);
        //        GradBrush.EndPoint = new System.Windows.Point(0.5, 1);
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF000C7E")), 0.135));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FF8400FF")), 0.85));
        //        GradBrush.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#FFBB00FF")), 1));
        //        GradBrush.Opacity = 0.7;
        //        TimeOfDayColor = GradBrush;
        //    }
        //}
        //#endregion
        //#region Welcome section
        //private string _displayUsername;
        //public string DisplayUsername
        //{
        //    get { return _displayUsername; }
        //    set { _displayUsername = value; OnPropertyChanged(); }
        //}
        //#endregion
        public TrangChuViewModel()
        {
        }
    }
}
