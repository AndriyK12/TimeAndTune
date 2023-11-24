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

namespace TimeAndTune.BLL
{
    internal class NavWindowBack : Window
    {
        public static void navigateHome_Click(object sender, RoutedEventArgs e, NavWindow nw)
        {
            HomePage homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(homePage);
            }
            nw.isClosedByUser = true;
            nw.Close();
        }
        public static void navigateStatistics_Click(object sender, RoutedEventArgs e, NavWindow nw)
        {
            StatisticsPage statisticsPage = new StatisticsPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(statisticsPage);
            }
            nw.isClosedByUser = true;
            nw.Close();
        }
        public static void navigateFocus_Click(object sender, RoutedEventArgs e, NavWindow nw)
        {
            FocusPage focusPage = new FocusPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(focusPage);
            }
            nw.isClosedByUser = true;
            nw.Close();
        }
    }
}
