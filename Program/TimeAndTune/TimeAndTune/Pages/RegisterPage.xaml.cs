using EFCore;
using EFCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TimeAndTune.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        DatabaseUserProvider userService = new DatabaseUserProvider();
        public bool comparePasswords(string passw1, string passw2)
        {
            return passw1 == passw2;
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
        static bool IsEmail(string input)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        public void SighUp_Click(object sender, RoutedEventArgs e)
        {
            errorRegisterEmailInput.Text = "";
            errorRegisterPasswordsDontMatch.Text = "";
            errorRegisterAllFields.Text = "";
            string enteredName = txtName.Text;
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Password;
            string passwordConfirmation = txtConfirmPassword.Password;
            if (!IsEmail(enteredEmail))
            {
                errorRegisterEmailInput.Text = "Please enter valid email!";
            }
            else if (userService.isUserAlreadyExist(enteredEmail))
            {
                errorRegisterEmailInput.Text = "User with this email already exists!";
            }
            else if (!comparePasswords(enteredPassword,passwordConfirmation)){
                txtPassword.Password = "";
                txtConfirmPassword.Password = "";
                errorRegisterPasswordsDontMatch.Text = "Passwords do not match!";
            }
            else 
            { 
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword);
                if (enteredName == "" || enteredEmail == "" || enteredPassword == "")
                {
                    errorRegisterAllFields.Text = "Please enter every field!";
                }
                else
                {
                    
                    userService.addNewUser(enteredName, enteredEmail, hashedPassword);
                    User user = userService.getUserByEmail(enteredEmail);
                    MainWindow.ActiveUser = user;
                    NavigateToHomePage(sender, e);
                }
            }
        }
        public void onSignIn_Click(object sender, RoutedEventArgs e)
        {
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
                txtName.Text = "";
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
            ToggleButton toggleButton = sender as ToggleButton;
            Image passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
            passwordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Приховати пароль
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
            ToggleButton toggleButton = sender as ToggleButton;
            Image passwordVisibility = toggleButton.Template.FindName("passwordVisibility", toggleButton) as Image;
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
            ToggleButton toggleButton = sender as ToggleButton;
            Image confPasswordVisibility = toggleButton.Template.FindName("confPasswordVisibility", toggleButton) as Image;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/hidePassword_image.png", UriKind.Relative));
        }

        private void btnShowConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Приховати пароль
            txtConfirmPassword.Visibility = Visibility.Visible;
            txtConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            ToggleButton toggleButton = sender as ToggleButton;
            Image confPasswordVisibility = toggleButton.Template.FindName("confPasswordVisibility", toggleButton) as Image;
            confPasswordVisibility.Source = new BitmapImage(new Uri("/Pages/showPassword_image.png", UriKind.Relative));
        }
    }
}
