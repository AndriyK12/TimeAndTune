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

namespace TimeAndTune.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtEmail.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "";
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "";
            }
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            string newText = txtPassword.Password;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPassword.Password = "";
                txtPassword.Foreground = Brushes.Black;
                txtPasswordVisible.Text = txtPassword.Password;
                txtPasswordVisible.Text = "";
            }
        }

        private void btnShowPassword_Checked(object sender, RoutedEventArgs e)
        {

            // Показати пароль
            txtPassword.Visibility = Visibility.Collapsed;
            txtPasswordVisible.Visibility = Visibility.Visible;
            txtPasswordVisible.Text = txtPassword.Password;
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Приховати пароль
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
        }
    }
}
