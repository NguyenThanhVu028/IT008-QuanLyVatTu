using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMQuanLyVatTu.CustomControls
{
    public class CircleImage : Button
    {
        
        static CircleImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleImage), new FrameworkPropertyMetadata(typeof(CircleImage)));
        }
        #region ImageLocation
                public string ImageLocation
                {
                    get { return (string)GetValue(ImageLocationProperty); }
                    set { SetValue(ImageLocationProperty, value); }
                }
                public static readonly DependencyProperty ImageLocationProperty =
                    DependencyProperty.Register("ImageLocation", typeof(string), typeof(CircleImage), new PropertyMetadata("/Material/Images/null_image.jpg"));
                #endregion
        #region WaterMark
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(CircleImage), new PropertyMetadata("/Material/Images/null_image.jpg"));
        #endregion
        #region BorderColor
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CircleImage), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderThick
        public double BorderThick
        {
            get { return (double)GetValue(BorderThickProperty); }
            set { SetValue(BorderThickProperty, value); }
        }
        public static readonly DependencyProperty BorderThickProperty =
            DependencyProperty.Register("BorderThick", typeof(double), typeof(CircleImage), new PropertyMetadata(20.0));
        #endregion
    }
}
