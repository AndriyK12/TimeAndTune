﻿using System;
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
using EFCore.Service;
using TimeAndTune.BLL;

namespace TimeAndTune.DialogWindows
{
    /// <summary>
    /// Interaction logic for AdditionalTaskInfoDialog.xaml
    /// </summary>

    public partial class AdditionalTaskInfoDialog : Window
    {
        public int taskId;
        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.goBackToHomePage(sender, e);
        }
        public void update_Click(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.update_Click(sender, e, this);
        }
        public AdditionalTaskInfoDialog()
        {
            InitializeComponent();

        }

        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdditionalTaskInfoBack.txtTaskName_TextChanged(sender, e, this);
        }
        private void txtTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtTaskName_GotFocus(sender, e, this);
        }

        private void txtTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtTaskName_LostFocus(sender, e, this);
        }

        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDate_TextChanged(sender, e, this);
        }
        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDate_GotFocus(sender, e, this);
        }

        private void txtDate_LostFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDate_LostFocus(sender, e, this);
        }

        private void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            AdditionalTaskInfoBack.txtDate_PreviewTextInput(sender, e);
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDescription_TextChanged(sender, e, this);
        }
        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDescription_GotFocus(sender, e, this);
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            AdditionalTaskInfoBack.txtDescription_LostFocus(sender, e, this);
        }
    }
}
