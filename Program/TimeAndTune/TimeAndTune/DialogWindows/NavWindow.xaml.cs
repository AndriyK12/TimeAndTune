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
    /// Interaction logic for NavWindow.xaml
    /// </summary>
    public partial class NavWindow : Window
    {
        public bool isClosedByUser = false;
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
            NavWindowBack.navigateHome_Click(sender, e, this);
        }
        public void navigateStatistics_Click(object sender, RoutedEventArgs e)
        {
            NavWindowBack.navigateStatistics_Click(sender, e, this);
        }
        public void navigateFocus_Click(object sender, RoutedEventArgs e)
        {
            NavWindowBack.navigateFocus_Click(sender, e, this);
        }
        public NavWindow()
        {
            InitializeComponent();
            Left = 0;
            Top = 23;
        }
    }
}
