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
using TimeAndTune.BLL;

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>


    public partial class HomePage : Page
    {
        public void openCreateNewTaskDialog_Click(object sender, RoutedEventArgs e)
        {
            HomePageBack.openCreateNewTaskDialog_Click(sender, e);
        }
        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            HomePageBack.openNavigation_Click(sender, e);
        }
        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            HomePageBack.openUserInfo_Click(sender, e);
        }
        private void Today_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Week_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            //add some logic
        }
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
