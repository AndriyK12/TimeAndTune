using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TimeAndTune.DialogWindows;
using EFCore.Service;
using EFCore;
using TimeAndTune.Pages;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace TimeAndTune.BLL
{
    class LoginPageBack : Page
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

        public static void txtEmail_TextChanged(object sender, TextChangedEventArgs e, LoginPage lp)
        {
            string newText = lp.txtEmail.Text;
            Console.WriteLine("Text changed: " + newText);
        }

        public static void txtEmail_GotFocus(object sender, RoutedEventArgs e, LoginPage lp)
        {
            if (lp.txtEmail.Text == "")
            {
                lp.txtEmail.Text = "";
            }
        }

        public static void txtEmail_LostFocus(object sender, RoutedEventArgs e, LoginPage lp)
        {
            if (string.IsNullOrWhiteSpace(lp.txtEmail.Text))
            {
                lp.txtEmail.Text = "";
            }
        }

        public static void txtPassword_TextChanged(object sender, RoutedEventArgs e, LoginPage lp)
        {
            string newText = lp.txtPassword.Password;
            Console.WriteLine("Text changed: " + newText);
        }

        public static void txtPassword_GotFocus(object sender, RoutedEventArgs e, LoginPage lp)
        {
            if (lp.txtPassword.Password == "")
            {
                lp.txtPassword.Password = "";
                lp.txtPassword.Foreground = Brushes.Black;
                lp.txtPasswordVisible.Text = lp.txtPassword.Password;
                lp.txtPasswordVisible.Text = "";
            }
        }

        public static void btnShowPassword_Checked(object sender, RoutedEventArgs e, LoginPage lp)
        {
            // Показати пароль
            lp.txtPassword.Visibility = Visibility.Collapsed;
            lp.txtPasswordVisible.Visibility = Visibility.Visible;
            lp.txtPasswordVisible.Text = lp.txtPassword.Password;
            ToggleButton toggleButton = sender as ToggleButton;
            Image passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        public static void btnShowPassword_Unchecked(object sender, RoutedEventArgs e, LoginPage lp)
        {
            // Приховати пароль
            lp.txtPassword.Visibility = Visibility.Visible;
            lp.txtPasswordVisible.Visibility = Visibility.Collapsed;
            ToggleButton toggleButton = sender as ToggleButton;
            Image passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }
    }
}
