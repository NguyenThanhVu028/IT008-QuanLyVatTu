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
using System.Windows.Shapes;

namespace PMQuanLyVatTu.ErrorMessage
{
    /// <summary>
    /// Interaction logic for LoginError.xaml
    /// </summary>
    public partial class LoginError : Window
    {
        public LoginError()
        {
            InitializeComponent();
        }
        void TryAgainClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void MoveWindow(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}
