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
    public class AccountButton : Button
    {
        static AccountButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AccountButton), new FrameworkPropertyMetadata(typeof(AccountButton)));
        }
        #region ImageLocation
        public string ImageLocation
        {
            get { return (string)GetValue(ImageLocationProperty); }
            set { SetValue(ImageLocationProperty, value); }
        }
        public static readonly DependencyProperty ImageLocationProperty =
            DependencyProperty.Register("ImageLocation", typeof(string), typeof(AccountButton), new PropertyMetadata("/Material/Images/null_image.jpg"));
        #endregion

    }
}
