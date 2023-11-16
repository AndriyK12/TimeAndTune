using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TimeAndTune.BLL
{
    internal class HomePageBack : Page
    {
        public static void openCreateNewTaskDialog_Click(object sender, RoutedEventArgs e)
        {
            NewTaskDialog dialog = new NewTaskDialog();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            dialog.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            dialog.Show();
        }
        public static void openNavigation_Click(object sender, RoutedEventArgs e)
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
        public static void openUserInfo_Click(object sender, RoutedEventArgs e)
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
    }
}
