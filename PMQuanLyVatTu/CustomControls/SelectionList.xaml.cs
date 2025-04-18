﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class SelectionList : UserControl
    {
        public double BoxHeight
        {
            get { return (double)GetValue(BoxHeightProperty); }
            set { SetValue(BoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoxHeightProperty =
            DependencyProperty.Register("BoxHeight", typeof(double), typeof(SelectionList), new PropertyMetadata(40.0));
        public double SizeOfFont
        {
            get { return (double)GetValue(SizeOfFontProperty); }
            set { SetValue(SizeOfFontProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SizeOfFont.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeOfFontProperty =
            DependencyProperty.Register("SizeOfFont", typeof(double), typeof(SelectionList), new PropertyMetadata(15.0));
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(SelectionList), new PropertyMetadata("Chọn"));
        public bool IsAllowed
        {
            get { return (bool)GetValue(IsAllowedProperty); }
            set { SetValue(IsAllowedProperty, value); }
        }
        public static readonly DependencyProperty IsAllowedProperty =
            DependencyProperty.Register("IsAllowed", typeof(bool), typeof(SelectionList), new PropertyMetadata(true));
        public ObservableCollection<string> DataList
        {
            get { return (ObservableCollection<string>)GetValue(DataListProperty); }
            set { SetValue(DataListProperty, value); }
        }
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(ObservableCollection<string>), typeof(SelectionList), new PropertyMetadata(new ObservableCollection<string> { "option1", "option2" }));
        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); Output.Text = value; }
        }
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(SelectionList), new PropertyMetadata(""));

        public SelectionList()
        {
            InitializeComponent();
            Output.DataContext = this;
        }
        void OutputFocused(object sender, RoutedEventArgs e)
        {
            DataChanged();
            //Container.Visibility = Visibility.Visible;
        }
        void OutputUnFocused(object sender, RoutedEventArgs e)
        {
            DataChanged();
            //Container.Visibility = Visibility.Collapsed;
        }
        void ItemClicked(object sender, RoutedEventArgs e)
        {
            string temp = (sender as Button).Content.ToString();
            //Container.Visibility = Visibility.Collapsed;
            SelectedValue = temp;
            Output.FontSize = SizeOfFont;
            //Output.Text = temp;
        }
        void TextChangedEvent(object sender, TextChangedEventArgs e)
        {
            SelectedValue = Output.Text;
        }
        void DataChanged()
        {
            ContainerStack.Children.Clear();
            foreach(string s in DataList)
            {
                Button temp = new Button();
                temp.HorizontalAlignment = HorizontalAlignment.Stretch;
                temp.VerticalAlignment = VerticalAlignment.Stretch;
                temp.Content = s;
                temp.Click += ItemClicked;
                ContainerStack.Children.Add(temp);
            }
        }
    }
}
