﻿namespace TimeAndTune.Pages
{
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
    using EFCore;
    using EFCore.Service;
    using Serilog;
    using TimeAndTune.BLL;

    public partial class RegisterPage : Page
    {
        private readonly ILogger logger = Log.ForContext<RegisterPage>();
        DatabaseUserProvider userService = new DatabaseUserProvider();

        public static bool comparePasswords(string passw1, string passw2)
        {
            return passw1 == passw2;
        }

        public void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to HomePage");
            RegisterPageBack.NavigateToHomePage(sender, e);
            logger.Debug("Navigated to HomePage successfully");
        }

        public static bool IsEmail(string input)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        public void SighUp_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("SighUp_Click clicked");
            errorRegisterEmailInput.Text = string.Empty;
            errorRegisterPasswordsDontMatch.Text = string.Empty;
            errorRegisterAllFields.Text = string.Empty;
            string enteredName = txtName.Text;
            string enteredEmail = txtEmail.Text;
            string enteredPassword = txtPassword.Password;
            string passwordConfirmation = txtConfirmPassword.Password;
            if (!IsEmail(enteredEmail))
            {
                errorRegisterEmailInput.Text = "Please enter valid email!";
                logger.Error("Entered invalid email");
            }
            else if (userService.isUserAlreadyExist(enteredEmail))
            {
                errorRegisterEmailInput.Text = "User with this email already exists!";
                logger.Error("Entered already existing email");
            }
            else if (!comparePasswords(enteredPassword, passwordConfirmation))
            {
                txtPassword.Password = string.Empty;
                txtConfirmPassword.Password = string.Empty;
                errorRegisterPasswordsDontMatch.Text = "Passwords do not match!";
                logger.Error("Entered passwords do not match");
            }
            else 
            { 
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword);
                if (enteredName == string.Empty || enteredEmail == string.Empty || enteredPassword == string.Empty)
                {
                    errorRegisterAllFields.Text = "Please enter every field!";
                    logger.Error("Not every field was entered");
                }
                else
                {
                    userService.addNewUser(enteredName, enteredEmail, hashedPassword);
                    logger.Debug("Signed up successfully");
                    User user = userService.getUserByEmail(enteredEmail);
                    MainWindow.ActiveUser = user;
                    NavigateToHomePage(sender, e);
                }
            }
        }

        public void onSignIn_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("onSignIn_Click clicked");
            RegisterPageBack.onSignIn_Click(sender, e);
            logger.Debug("onSignIn_Click completed");
        }

        public RegisterPage()
        {
            logger.Information("Initializing RegisterPage");
            InitializeComponent();
            logger.Information("RegisterPage initialized successfully");
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterPageBack.txtName_TextChanged(sender, e, this);
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtName_GotFocus(sender, e, this);
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtName_LostFocus(sender, e, this);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterPageBack.txtEmail_TextChanged(sender, e, this);
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtEmail_GotFocus(sender, e, this);
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtEmail_LostFocus(sender, e, this);
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtPassword_GotFocus(sender, e, this);
        }

        private void btnShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            logger.Debug("btnShowPassword_Checked clicked");
            RegisterPageBack.btnShowPassword_Checked(sender, e, this);
            logger.Debug("btnShowPassword_Checked success");
        }

        private void btnShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            logger.Debug("btnShowPassword_Unchecked clicked");
            RegisterPageBack.btnShowPassword_Unchecked(sender, e, this);
            logger.Debug("btnShowPassword_Unchecked success");
        }

        private void txtConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            RegisterPageBack.txtConfirmPassword_GotFocus(sender, e, this);
        }

        private void btnShowConfirmPassword_Checked(object sender, RoutedEventArgs e)
        {
            logger.Debug("btnShowConfirmPassword_Checked clicked");
            RegisterPageBack.btnShowConfirmPassword_Checked(sender, e, this);
            logger.Debug("btnShowConfirmPassword_Checked success");
        }

        private void btnShowConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            logger.Debug("btnShowConfirmPassword_Unchecked clicked");
            RegisterPageBack.btnShowConfirmPassword_Unchecked(sender, e, this);
            logger.Debug("btnShowConfirmPassword_Unchecked success");
        }
    }
}
