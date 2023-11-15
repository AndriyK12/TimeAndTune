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

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            NavWindow nav = new NavWindow();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            nav.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            nav.Show();
        }
        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userWnd = new UserInfoWindow();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            userWnd.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            userWnd.Show();
        }
        private void Week_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Year_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            //add some logic
        }
        public StatisticsPage()
        {
            InitializeComponent();
        }
    }
}
