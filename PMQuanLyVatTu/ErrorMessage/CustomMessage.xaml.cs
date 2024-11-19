using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

namespace PMQuanLyVatTu.ErrorMessage
{
    /// <summary>
    /// Interaction logic for CustomMessage.xaml
    /// </summary>
    public partial class CustomMessage : Window, INotifyPropertyChanged
    {
        #region ImageLocation
        private string _imageLocation;
        public string ImageLocation
        {
            get
            {
                return _imageLocation;
            }
            set
            {
                _imageLocation = value; OnPropertyChanged();
            }
        }
        #endregion
        #region MainTitle
        private string _title;
        public string MainTitle
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value; OnPropertyChanged();
            }
        }
        #endregion
        #region MainMessage
        private string _message;
        public string MainMessage
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value; OnPropertyChanged();
            }
        }
        #endregion
        #region ReturnValue
        private bool _returnValue = false;
        public bool ReturnValue
        {
            get { return _returnValue; }
            set
            {
                _returnValue = value; OnPropertyChanged();
            }
        }
        #endregion
        public CustomMessage(string img = "/Material/Images/null_image.jpg", string title = "Default Title", string mess = "Default Message")
        {
            InitializeComponent();
            this.DataContext = this;
            ImageLocation = img;
            MainTitle = title;
            MainMessage = mess;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        void CancelClick(object sender, RoutedEventArgs e)
        {
            ReturnValue = false;
            this.Close();
        }
        void OKClick(object sender, RoutedEventArgs e)
        {
            ReturnValue= true;
            this.Close();
        }
    }
}
