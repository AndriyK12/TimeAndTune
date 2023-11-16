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
using System.Windows.Shapes;
using TimeAndTune.Pages;

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
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
        public void exit_Click(object sender, RoutedEventArgs e)
        {
            // add exit logic
            LoginPage loginPage = new LoginPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(loginPage);
            }
            isClosedByUser = true;
            Close();
        }
        public UserInfoWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 23;
            usernameText.Text = MainWindow.ActiveUser.Username;
            emailText.Text = MainWindow.ActiveUser.Email;
            coinsAmountText.Text = $"{MainWindow.ActiveUser.Coinsamount}";
        }
    }
}
