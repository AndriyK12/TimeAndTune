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
using System.Windows.Shapes;
using EFCore;
using EFCore.Service;
using TimeAndTune.DialogWindows;

namespace TimeAndTune.BLL
{
    internal class AdditionalTaskInfoBack : Window
    {
        public static void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }
        public static void update_Click(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newName = atid.txtTaskName.Text;
            string newDesc = atid.txtDescription.Text;
            string newDate = atid.txtDate.Text;
            ComboBox? priorityComboBox = atid.priorityButton.Template
                .FindName("priorityComboBox", atid.priorityButton) as ComboBox;
            int newPrority = priorityComboBox.SelectedIndex;
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            EFCore.Task? newTask = dataBaseTaskProvider.getTaskById(atid.taskId);
            dataBaseTaskProvider.updateTaskById(atid.taskId, newName, newDesc, newDate, newPrority);
            atid.Close();
        }
        public static void txtTaskName_TextChanged(object sender, TextChangedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newText = atid.txtTaskName.Text;
        }
        public static void txtTaskName_GotFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (atid.txtTaskName.Text == "")
            {
                atid.txtTaskName.Text = "";
            }
        }
        public static void txtTaskName_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtTaskName.Text))
            {
                atid.txtTaskName.Text = "";
            }
        }
        public static void txtDate_TextChanged(object sender, TextChangedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newText = atid.txtDate.Text;
        }
        public static void txtDate_GotFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (atid.txtDate.Text == "")
            {
                atid.txtDate.Text = "";
            }
        }
        public static void txtDate_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtDate.Text))
            {
                atid.txtDate.Text = "";
            }
        }
        public static void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != '/' && e.Text[0] != '.' && e.Text[0] != '-')
            {
                e.Handled = true;
            }
            else
            {
                TextBox textBox = (TextBox)sender;
                string currentText = textBox.Text;
                int caretIndex = textBox.CaretIndex;

                if ((caretIndex == 2 || caretIndex == 5) && caretIndex < 10)
                {
                    textBox.Text = currentText + "/";
                    textBox.CaretIndex = caretIndex + 1;
                }
                if (textBox.Text.Length >= 10)
                {
                    e.Handled = true;
                }
            }
        }
        public static void txtDescription_TextChanged(object sender, TextChangedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newText = atid.txtDescription.Text;
        }
        public static void txtDescription_GotFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (atid.txtDescription.Text == "")
            {
                atid.txtDescription.Text = "";
            }
        }
        public static void txtDescription_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtDescription.Text))
            {
                atid.txtDescription.Text = "";
            }
        }
    }
}
