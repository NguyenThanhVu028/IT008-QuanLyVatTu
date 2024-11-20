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
    public class CustomCheckbox : CheckBox
    {
        static CustomCheckbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomCheckbox), new FrameworkPropertyMetadata(typeof(CustomCheckbox)));
        }
    }
}
