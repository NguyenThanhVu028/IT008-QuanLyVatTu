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
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        #region ImageLocation
        public string ImageLocation
        {
            get { return (string)GetValue(ImageLocationProperty); }
            set { SetValue(ImageLocationProperty, value); }
        }
        public static readonly DependencyProperty ImageLocationProperty =
            DependencyProperty.Register("ImageLocation", typeof(string), typeof(ImageButton), new PropertyMetadata("/Material/Images/null_image.jpg"));
        #endregion
        #region BorderBackgroundHighlight
        public SolidColorBrush BorderBackgroundHightlight
        {
            get { return (SolidColorBrush)GetValue(BorderBackgroundHightlightProperty); }
            set { SetValue(BorderBackgroundHightlightProperty, value); }
        }
        public static readonly DependencyProperty BorderBackgroundHightlightProperty =
            DependencyProperty.Register("BorderBackgroundHightlight", typeof(SolidColorBrush), typeof(CustomButton), new PropertyMetadata(Brushes.Aqua));
        #endregion
        #region BorderBackgroundClicked


        public SolidColorBrush BorderBackgroundClicked
        {
            get { return (SolidColorBrush)GetValue(BorderBackgroundClickedProperty); }
            set { SetValue(BorderBackgroundClickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBackgroundClicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBackgroundClickedProperty =
            DependencyProperty.Register("BorderBackgroundClicked", typeof(SolidColorBrush), typeof(ImageButton), new PropertyMetadata(Brushes.LightBlue));


        #endregion
    }
}
