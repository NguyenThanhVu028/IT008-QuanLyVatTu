using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CustomTextbox : TextBox
    {
        static CustomTextbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextbox), new FrameworkPropertyMetadata(typeof(CustomTextbox)));
        }
        #region IsHidden
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(CustomTextbox), new PropertyMetadata(true));


        #endregion
        #region WaterMark
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(CustomTextbox), new PropertyMetadata("Nhập"));
        #endregion
        #region BackgroundColor
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.White));
        #endregion
        #region BackgroundColorHighlight
        public SolidColorBrush BackgroundColorHighlight
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorHighlightProperty); }
            set { SetValue(BackgroundColorHighlightProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorHighlightProperty =
            DependencyProperty.Register("BackgroundColorHighlight", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.White));
        #endregion
        #region BackgroundColorClicked
        public SolidColorBrush BackgroundColorClicked
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorClickedProperty); }
            set { SetValue(BackgroundColorClickedProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorClickedProperty =
            DependencyProperty.Register("BackgroundColorClicked", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.White));
        #endregion
        #region BorderColor
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.Transparent));
        #endregion
        #region BorderColorHighlight
        public SolidColorBrush BorderColorHighlight
        {
            get { return (SolidColorBrush)GetValue(BorderColorHighlightProperty); }
            set { SetValue(BorderColorHighlightProperty, value); }
        }
        public static readonly DependencyProperty BorderColorHighlightProperty =
            DependencyProperty.Register("BorderColorHighlight", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderColorClicked
        public SolidColorBrush BorderColorClicked
        {
            get { return (SolidColorBrush)GetValue(BorderColorClickedProperty); }
            set { SetValue(BorderColorClickedProperty, value); }
        }
        public static readonly DependencyProperty BorderColorClickedProperty =
            DependencyProperty.Register("BorderColorClicked", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.Transparent));
        #endregion
        #region BorderThick
        public Thickness BorderThick
        {
            get { return (Thickness)GetValue(BorderThickProperty); }
            set { SetValue(BorderThickProperty, value); }
        }
        public static readonly DependencyProperty BorderThickProperty =
            DependencyProperty.Register("BorderThick", typeof(Thickness), typeof(CustomTextbox), new PropertyMetadata(new Thickness(0)));
        #endregion
        #region TextColor
        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(SolidColorBrush), typeof(CustomTextbox), new PropertyMetadata(Brushes.Black));
        #endregion
        #region TextSize
        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(double), typeof(CustomTextbox), new PropertyMetadata(15.0));
        #endregion
        #region RadiusOfCorner
        public CornerRadius RadiusOfCorner
        {
            get { return (CornerRadius)GetValue(RadiusOfCornerProperty); }
            set { SetValue(RadiusOfCornerProperty, value); }
        }
        public static readonly DependencyProperty RadiusOfCornerProperty =
            DependencyProperty.Register("RadiusOfCorner", typeof(CornerRadius), typeof(CustomTextbox), new PropertyMetadata(new CornerRadius(0)));
        #endregion
    }
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Collapsed) return false;
            else return true;
        }
    }
}
