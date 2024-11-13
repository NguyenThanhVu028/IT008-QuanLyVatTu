using System;
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
    /// <summary>
    /// Interaction logic for SelectionList.xaml
    /// </summary>
    public partial class SelectionList : UserControl
    {
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
            set { SetValue(SelectedValueProperty, value);}
        }
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(SelectionList), new PropertyMetadata(""));


        public SelectionList()
        {
            InitializeComponent();
        }
        void OutputFocused(object sender, RoutedEventArgs e)
        {
            DataChanged();
            Container.Visibility = Visibility.Visible;
        }
        void OutputUnFocused(object sender, RoutedEventArgs e)
        {
            DataChanged();
            Container.Visibility = Visibility.Collapsed;
        }
        void ItemClicked(object sender, RoutedEventArgs e)
        {
            string temp = (sender as Button).Content.ToString();
            Container.Visibility = Visibility.Collapsed;
            SelectedValue = temp;
            Output.Text = temp;
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
                temp.Content = s; temp.Click += ItemClicked;
                ContainerStack.Children.Add(temp);
            }
        }
    }
}
