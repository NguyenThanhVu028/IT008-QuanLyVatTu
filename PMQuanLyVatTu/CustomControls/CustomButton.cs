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
    public class CustomButton : Button
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }
        #region DisplayText
        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(CustomButton), new PropertyMetadata("Null"));
        #endregion
        #region TextSize
        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(double), typeof(CustomButton), new PropertyMetadata(0.0));
        #endregion
        #region BorderColor
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderColorHighlight
        public SolidColorBrush BorderColorHighlight
        {
            get { return (SolidColorBrush)GetValue(BorderColorHighlightProperty); }
            set { SetValue(BorderColorHighlightProperty, value); }
        }
        public static readonly DependencyProperty BorderColorHighlightProperty =
            DependencyProperty.Register("BorderColorHighlight", typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderBackground
        public SolidColorBrush BorderBackground
        {
            get { return (SolidColorBrush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderBackgroundClicked
        public SolidColorBrush BorderBackgroundClicked
        {
            get { return (SolidColorBrush)GetValue(BorderBackgroundClickedProperty); }
            set { SetValue(BorderBackgroundClickedProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundClickedProperty =
            DependencyProperty.Register("BorderBackgroundClicked", typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderThick
        public Thickness BorderThick
        {
            get { return (Thickness)GetValue(BorderThickProperty); }
            set { SetValue(BorderThickProperty, value); }
        }
        public static readonly DependencyProperty BorderThickProperty =
            DependencyProperty.Register("BorderThick", typeof(Thickness), typeof(CustomButton), new PropertyMetadata(new Thickness(0)));
        #endregion
        #region RadiusOfCorner
        public CornerRadius RadiusOfCorner
        {
            get { return (CornerRadius)GetValue(RadiusOfCornerProperty); }
            set { SetValue(RadiusOfCornerProperty, value); }
        }
        public static readonly DependencyProperty RadiusOfCornerProperty =
            DependencyProperty.Register("RadiusOfCorner", typeof(CornerRadius), typeof(CustomButton), new PropertyMetadata(new CornerRadius(0)));
        #endregion
    }
}
