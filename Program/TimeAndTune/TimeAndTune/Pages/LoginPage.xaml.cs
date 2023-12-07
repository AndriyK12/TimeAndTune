namespace TimeAndTune.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.BLL;

    public class PasswordManager
    {
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }

    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Password;

            DatabaseUserProvider userService = new DatabaseUserProvider();
            User user = userService.getUserByEmail(enteredEmail);
            string hashedPassword = user.Password;
            if (hashedPassword != null && PasswordManager.VerifyPassword(enteredPassword, hashedPassword))
            {
                MainWindow.ActiveUser = user;
                NavigateToHomePage(sender, e);
            }
            else
            {
                txtPassword.Password = string.Empty;
                errorLoginEmail.Text = "Authentication failed. Please check your username and password.";
            }
        }

        public void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            LoginPageBack.NavigateToHomePage(sender, e);
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
            LoginPageBack.txtEmail_TextChanged(sender, e, this);
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginPageBack.txtEmail_GotFocus(sender, e, this);
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            LoginPageBack.txtEmail_LostFocus(sender, e, this);
        }

        private void txtPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            LoginPageBack.txtPassword_TextChanged(sender, e, this);
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginPageBack.txtPassword_GotFocus(sender, e, this);
        }

        private void btnShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            LoginPageBack.btnShowPassword_Checked(sender, e, this);
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            LoginPageBack.btnShowPassword_Unchecked(sender, e, this);
        }
    }
}
