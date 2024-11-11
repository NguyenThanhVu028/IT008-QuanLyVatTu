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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMQuanLyVatTu.CustomControls
{
    public class NavigationButton : RadioButton
    {
        static NavigationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationButton), new FrameworkPropertyMetadata(typeof(NavigationButton)));
        }
        #region ImageLocation
        public string ImageLocation
        {
            get { return (string)GetValue(ImageLocationProperty); }
            set { SetValue(ImageLocationProperty, value); }
        }
        public static readonly DependencyProperty ImageLocationProperty =
            DependencyProperty.Register("ImageLocation", typeof(string), typeof(NavigationButton), new PropertyMetadata("/Material/Images/null_image.jpg"));
        #endregion
        #region DisplayText
        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(NavigationButton), new PropertyMetadata("Null"));
        #endregion
        #region TextColor
        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(SolidColorBrush), typeof(NavigationButton), new PropertyMetadata(Brushes.Aqua));


        #endregion
        #region TextSize
        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(double), typeof(NavigationButton), new PropertyMetadata(0.0));


        #endregion
        #region BackgroundColor
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(NavigationButton), new PropertyMetadata(Brushes.Black));
        #endregion
        #region BorderColor
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(NavigationButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderThick
        public Thickness BorderThick
        {
            get { return (Thickness)GetValue(BorderThickProperty); }
            set { SetValue(BorderThickProperty, value); }
        }
        public static readonly DependencyProperty BorderThickProperty =
            DependencyProperty.Register("BorderThick", typeof(Thickness), typeof(NavigationButton), new PropertyMetadata(new Thickness(0)));
        #endregion
        #region RadiusOfCorner
        public CornerRadius RadiusOfCorner
        {
            get { return (CornerRadius)GetValue(RadiusOfCornerProperty); }
            set { SetValue(RadiusOfCornerProperty, value); }
        }
        public static readonly DependencyProperty RadiusOfCornerProperty =
            DependencyProperty.Register("RadiusOfCorner", typeof(CornerRadius), typeof(NavigationButton), new PropertyMetadata(new CornerRadius(0)));
        #endregion
    }
}
