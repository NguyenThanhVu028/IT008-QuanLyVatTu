﻿using System;
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
    public partial class CustomPasswordbox : UserControl
    {
        #region Password
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordbox), new PropertyMetadata(""));
        #endregion
        void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = CustomPasswordBox.Password;
            if(Password!="") WaterMark.Visibility = Visibility.Collapsed;
            else WaterMark.Visibility = Visibility.Visible;
        }
        public CustomPasswordbox()
        {
            InitializeComponent();
        }
    }
}
