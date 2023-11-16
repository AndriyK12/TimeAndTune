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
using EFCore;
using EFCore.Service;

namespace TimeAndTune.Pages
{
    public class PasswordManager
    {
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Password;

            DatabaseUserProvider userService = new DatabaseUserProvider();
            User user = userService.getUserByEmail(enteredEmail);
            string hashedPassword = user.Password;
            if (hashedPassword != null && PasswordManager.VerifyPassword(enteredPassword, hashedPassword))
            {
                NavigateToHomePage(sender, e);
            }
            else
            {
                MessageBox.Show("Authentication failed. Please check your username and password.");
            }
        }

        public void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(homePage);
            }
        }

        public void onCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(registerPage);
            }
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
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Приховати пароль
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }
    }
}
