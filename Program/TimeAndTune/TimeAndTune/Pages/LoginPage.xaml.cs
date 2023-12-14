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
    using Serilog;
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
        private readonly ILogger logger = Log.ForContext<LoginPage>();

        public LoginPage()
        {
            logger.Information("Initializing LoginPage");
            InitializeComponent();
            logger.Information("LoginPage initialized successfully");
        }

        public void SignIn_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("SignIn_Click clicked");
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Password;

            DatabaseUserProvider userService = new DatabaseUserProvider();
            User user = userService.getUserByEmail(enteredEmail);
            string hashedPassword = user.Password;
            logger.Debug("Verifying password...");
            if (hashedPassword != null && PasswordManager.VerifyPassword(enteredPassword, hashedPassword))
            {
                logger.Debug("Password verified");
                MainWindow.ActiveUser = user;
                NavigateToHomePage(sender, e);
                logger.Debug("Signed In successfully");
            }
            else
            {
                txtPassword.Password = string.Empty;
                errorLoginEmail.Text = "Authentication failed. Please check your username and password";
                logger.Error("Wrong auth info");
            }
        }

        public void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to HomePage");
            LoginPageBack.NavigateToHomePage(sender, e);
            logger.Debug("Navigated to HomePage successfully");
        }

        public void onCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to RegisterPage");
            RegisterPage registerPage = new RegisterPage();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("mainFrame") is Frame mainFrame)
            {
                mainFrame.Navigate(registerPage);
                logger.Debug("Navigated to RegisterPage successfully");
            }
            else
            {
                logger.Error("Navigation to RegisterPage failed");
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
            logger.Debug("btnShowPassword_Checked clicked");
            LoginPageBack.btnShowPassword_Checked(sender, e, this);
            logger.Debug("btnShowPassword_Checked success");
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            logger.Debug("btnShowPassword_Unchecked clicked");
            LoginPageBack.btnShowPassword_Unchecked(sender, e, this);
            logger.Debug("btnShowPassword_Unchecked success");
        }
    }
}
