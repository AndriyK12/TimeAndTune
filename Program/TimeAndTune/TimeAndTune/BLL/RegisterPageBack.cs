namespace TimeAndTune.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Xml.Linq;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.DialogWindows;
    using TimeAndTune.Pages;

    public class RegisterPageBack : Page
    {
        public static void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(homePage);
            }
        }

        public static void onSignIn_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(loginPage);
            }
        }

        public static void txtName_TextChanged(object sender, TextChangedEventArgs e, RegisterPage rp)
        {
            string newText = rp.txtName.Text;
            Console.WriteLine("Text changed: " + newText);
        }

        public static void txtName_GotFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (rp.txtName.Text == string.Empty)
            {
                rp.txtName.Text = string.Empty;
            }
        }

        public static void txtName_LostFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (string.IsNullOrWhiteSpace(rp.txtName.Text))
            {
                rp.txtName.Text = string.Empty;
            }
        }

        public static void txtEmail_TextChanged(object sender, TextChangedEventArgs e, RegisterPage rp)
        {
            string newText = rp.txtEmail.Text;
            Console.WriteLine("Text changed: " + newText);
        }

        public static void txtEmail_GotFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (rp.txtEmail.Text == string.Empty)
            {
                rp.txtEmail.Text = string.Empty;
            }
        }

        public static void txtEmail_LostFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (string.IsNullOrWhiteSpace(rp.txtEmail.Text))
            {
                rp.txtEmail.Text = string.Empty;
            }
        }

        public static void txtPassword_GotFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (rp.txtPassword.Password == string.Empty)
            {
                rp.txtPassword.Password = string.Empty;
                rp.txtPassword.Foreground = Brushes.Black;
                rp.txtPasswordVisible.Text = rp.txtPassword.Password;
                rp.txtPasswordVisible.Text = string.Empty;
            }
        }

        public static void btnShowPassword_Checked(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            // Показати пароль
            rp.txtPassword.Visibility = Visibility.Collapsed;
            rp.txtPasswordVisible.Visibility = Visibility.Visible;
            rp.txtPasswordVisible.Text = rp.txtPassword.Password;
            ToggleButton? toggleButton = sender as ToggleButton;
            Image? passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        public static void btnShowPassword_Unchecked(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            // Приховати пароль
            rp.txtPassword.Visibility = Visibility.Visible;
            rp.txtPasswordVisible.Visibility = Visibility.Collapsed;
            ToggleButton? toggleButton = sender as ToggleButton;
            Image? passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }

        public static void txtConfirmPassword_GotFocus(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            if (rp.txtConfirmPassword.Password == string.Empty)
            {
                rp.txtConfirmPassword.Password = string.Empty;
                rp.txtConfirmPassword.Foreground = Brushes.Black;
                rp.txtConfirmPasswordVisible.Text = rp.txtConfirmPassword.Password;
                rp.txtConfirmPasswordVisible.Text = string.Empty;
            }
        }

        public static void btnShowConfirmPassword_Checked(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            // Показати пароль
            rp.txtConfirmPassword.Visibility = Visibility.Collapsed;
            rp.txtConfirmPasswordVisible.Visibility = Visibility.Visible;
            rp.txtConfirmPasswordVisible.Text = rp.txtConfirmPassword.Password;
            ToggleButton? toggleButton = sender as ToggleButton;
            Image? confPasswordVisibility = toggleButton.Template.FindName("confPasswordVisibility", toggleButton) as Image;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        public static void btnShowConfirmPassword_Unchecked(object sender, RoutedEventArgs e, RegisterPage rp)
        {
            // Приховати пароль
            rp.txtConfirmPassword.Visibility = Visibility.Visible;
            rp.txtConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            ToggleButton? toggleButton = sender as ToggleButton;
            Image? confPasswordVisibility = toggleButton.Template.FindName("confPasswordVisibility", toggleButton) as Image;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }
    }
}
