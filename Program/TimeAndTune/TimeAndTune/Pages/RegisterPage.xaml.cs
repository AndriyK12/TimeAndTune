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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public void Continue_Click(object sender, RoutedEventArgs e)
        {
            // add register logic
            HomePage homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(homePage);
            }
        }
        public void onSignIn_Click(object sender, RoutedEventArgs e)
        {
            // add register logic
            LoginPage loginPage = new LoginPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(loginPage);
            }
        }
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtName.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtEmail.Text = "";
            }
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = "";
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

        private void txtConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password == "")
            {
                txtConfirmPassword.Password = "";
                txtConfirmPassword.Foreground = Brushes.Black;
                txtConfirmPasswordVisible.Text = txtConfirmPassword.Password;
                txtConfirmPasswordVisible.Text = "";
            }
        }

        private void btnShowConfirmPassword_Checked(object sender, RoutedEventArgs e)
        {

            // Показати пароль
            txtConfirmPassword.Visibility = Visibility.Collapsed;
            txtConfirmPasswordVisible.Visibility = Visibility.Visible;
            txtConfirmPasswordVisible.Text = txtConfirmPassword.Password;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        private void btnShowConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Приховати пароль
            txtConfirmPassword.Visibility = Visibility.Visible;
            txtConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }
    }
}
