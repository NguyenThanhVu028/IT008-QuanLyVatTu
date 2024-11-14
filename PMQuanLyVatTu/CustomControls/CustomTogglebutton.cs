using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMQuanLyVatTu.CustomControls
{
    public class CustomTogglebutton : ToggleButton
    {
        static CustomTogglebutton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTogglebutton), new FrameworkPropertyMetadata(typeof(CustomTogglebutton)));
        }

        public Thickness BorderThick
        {
            get { return (Thickness)GetValue(BorderThickProperty); }
            set { SetValue(BorderThickProperty, value); }
        }
        public static readonly DependencyProperty BorderThickProperty =
            DependencyProperty.Register("BorderThick", typeof(Thickness), typeof(CustomTogglebutton), new PropertyMetadata(new Thickness(0)));
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(SolidColorBrush), typeof(CustomTogglebutton), new PropertyMetadata(Brushes.Aqua));
        public SolidColorBrush BorderBackground
        {
            get { return (SolidColorBrush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(SolidColorBrush), typeof(CustomTogglebutton), new PropertyMetadata(Brushes.Transparent));
        public CornerRadius RadiusOfCorner
        {
            get { return (CornerRadius)GetValue(RadiusOfCornerProperty); }
            set { SetValue(RadiusOfCornerProperty, value); }
        }
        public static readonly DependencyProperty RadiusOfCornerProperty =
            DependencyProperty.Register("RadiusOfCorner", typeof(CornerRadius), typeof(CustomTogglebutton), new PropertyMetadata(new CornerRadius(0)));


        public double SliderWidth
        {
            get { return (double)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }
        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.Register("SliderWidth", typeof(double), typeof(CustomTogglebutton), new PropertyMetadata(10.0));


        public double SliderHeight
        {
            get { return (double)GetValue(SliderHeightProperty); }
            set { SetValue(SliderHeightProperty, value); }
        }
        public static readonly DependencyProperty SliderHeightProperty =
            DependencyProperty.Register("SliderHeight", typeof(double), typeof(CustomTogglebutton), new PropertyMetadata(10.0));
        public SolidColorBrush SliderColor
        {
            get { return (SolidColorBrush)GetValue(SliderColorProperty); }
            set { SetValue(SliderColorProperty, value); }
        }
        public static readonly DependencyProperty SliderColorProperty =
            DependencyProperty.Register("SliderColor", typeof(SolidColorBrush), typeof(CustomTogglebutton), new PropertyMetadata(Brushes.Aqua));
    }
}
