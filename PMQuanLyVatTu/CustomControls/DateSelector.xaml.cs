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
    public partial class DateSelector : UserControl
    {
        public bool IsAllowed
        {
            get { return (bool)GetValue(IsAllowedProperty); }
            set { SetValue(IsAllowedProperty, value); }
        }
        public static readonly DependencyProperty IsAllowedProperty =
            DependencyProperty.Register("IsAllowed", typeof(bool), typeof(DateSelector), new PropertyMetadata(true));
        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(DateSelector), new PropertyMetadata(DateTime.Now.ToString("ddd/dd/MM/yyyy")));
        public DateSelector()
        {
            InitializeComponent();
            SelectedValue = (DateTime.Now).ToShortDateString();
            Output.DataContext = this;
        }
        void SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            DateTime temp = (DateTime)InnerCalender.SelectedDate;
            SelectedValue = temp.ToString("ddd/dd/MM/yyyy");
        }
    }
}
