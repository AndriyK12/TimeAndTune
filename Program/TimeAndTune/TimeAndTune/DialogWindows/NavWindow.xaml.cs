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
    /// Interaction logic for NavWindow.xaml
    /// </summary>
    public partial class NavWindow : Window
    {
        private bool isClosedByUser = false;
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!isClosedByUser)
            {
                Close();
            }
        }
        public void navigateHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(homePage);
            }
            isClosedByUser = true;
            Close();
        }
        public void navigateStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPage statisticsPage = new StatisticsPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(statisticsPage);
            }
            isClosedByUser = true;
            Close();
        }
        public void navigateFocus_Click(object sender, RoutedEventArgs e)
        {
            FocusPage focusPage = new FocusPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(focusPage);
            }
            isClosedByUser = true;
            Close();
        }
        public NavWindow()
        {
            InitializeComponent();
            Left = 0;
            Top = 23;
        }
    }
}
