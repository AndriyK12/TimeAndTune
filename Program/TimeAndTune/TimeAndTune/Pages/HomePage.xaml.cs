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
    /// Interaction logic for HomePage.xaml
    /// </summary>


    public partial class HomePage : Page
    {
        public void openCreateNewTaskDialog_Click(object sender, RoutedEventArgs e)
        {
            NewTaskDialog dialog = new NewTaskDialog();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            dialog.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            dialog.ShowDialog();
        }
        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            NavWindow nav = new NavWindow();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            nav.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            nav.ShowDialog();
        }
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
