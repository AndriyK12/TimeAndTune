﻿namespace TimeAndTune.BLL
{
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
    using System.Xml.Linq;
    using EFCore;
    using EFCore.Service;
    using Serilog;
    using TimeAndTune.BLL;

    internal class NewTaskDialogBack : Window
    {
        private readonly static ILogger logger = Log.ForContext<NewTaskDialogBack>();
        public static void goBackToHomePage(object sender, RoutedEventArgs e, NewTaskDialog nw)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                nw.noNeedToCloseOnDeactivated = true;
                currentWindow.Close();
            }
        }

        public static void addNewTask_Click(object sender, RoutedEventArgs e, NewTaskDialog nw)
        {
            nw.errorTaskDate.Text = string.Empty;
            nw.errorTaskName.Text = string.Empty;
            string taskName = nw.txtTaskName.Text;
            string taskDescription = nw.txtDescription.Text;
            DateOnly taskExpectedTime;
            int taskPriority = 0;
            int userIdRef = MainWindow.ActiveUser.Userid;
            ComboBox priorComboBox = (ComboBox)nw.priorBtn.Template.FindName("priorComboBox", nw.priorBtn);
            if (priorComboBox.SelectedItem != null)
            {
                ComboBoxItem priorityComboBoxItem = (ComboBoxItem)priorComboBox.SelectedItem;
                nw.noNeedToCloseOnDeactivated = true;
                if (priorityComboBoxItem.Content.ToString() == "Normal")
                {
                    taskPriority = 1;
                }
                else if (priorityComboBoxItem.Content.ToString() == "Important")
                {
                    taskPriority = 2;
                }
                else
                {
                    taskPriority = 3;
                }
            }
            else
            {
                taskPriority = 1;
            }
            if (DateOnly.TryParseExact(nw.txtDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateOnly result))
            {
                taskExpectedTime = result;
                nw.noNeedToCloseOnDeactivated = true;
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                if (taskExpectedTime >= today)
                {
                    if (taskName == string.Empty)
                    {
                        nw.noNeedToCloseOnDeactivated = true;
                        nw.errorTaskName.Text = "Please enter task name!";
                        logger.Error("Wrong task name");
                        nw.noNeedToCloseOnDeactivated = false;
                    }
                    else
                    {
                        DatabaseTaskProvider taskService = new DatabaseTaskProvider();
                        taskService.addNewTask(taskName, taskDescription, taskExpectedTime, taskPriority, userIdRef);
                        logger.Debug("Added new task successfully");
                        nw.homePage.updateListView();
                        logger.Debug("Updated list of tasks successfully");
                        nw.Close();
                        logger.Debug("NewTaskDialog window deactivated");
                    }
                }
                else
                {
                    nw.noNeedToCloseOnDeactivated = true;
                    nw.errorTaskDate.Text = "You can not set expected finish date to past time!";
                    logger.Error("Finish date was set to the past");
                    nw.noNeedToCloseOnDeactivated = false;
                }

            }
            else
            {
                nw.noNeedToCloseOnDeactivated = true;
                nw.errorTaskDate.Text = "Date was set incorrectly! Please enter in format dd/mm/yyyy.";
                logger.Error("Date was set incorrectly");
                nw.noNeedToCloseOnDeactivated = false;
            }
        }

        public static void txtTaskName_TextChanged(object sender, TextChangedEventArgs e, NewTaskDialog nwd)
        {
            string newText = nwd.txtTaskName.Text;
        }

        public static void txtTaskName_GotFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (nwd.txtTaskName.Text == string.Empty)
            {
                nwd.txtTaskName.Text = string.Empty;
            }
        }

        public static void txtTaskName_LostFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (string.IsNullOrWhiteSpace(nwd.txtTaskName.Text))
            {
                nwd.txtTaskName.Text = string.Empty;
            }
        }

        public static void txtDate_TextChanged(object sender, TextChangedEventArgs e, NewTaskDialog nwd)
        {
            string newText = nwd.txtDate.Text;
        }

        public static void txtDate_GotFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (nwd.txtDate.Text == string.Empty)
            {
                nwd.txtDate.Text = string.Empty;
            }
        }

        public static void txtDate_LostFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (string.IsNullOrWhiteSpace(nwd.txtDate.Text))
            {
                nwd.txtDate.Text = string.Empty;
            }
        }

        public static void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e, NewTaskDialog nwd)
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

        public static void txtDescription_TextChanged(object sender, TextChangedEventArgs e, NewTaskDialog nwd)
        {
            string newText = nwd.txtDescription.Text;
        }

        public static void txtDescription_GotFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (nwd.txtDescription.Text == string.Empty)
            {
                nwd.txtDescription.Text = string.Empty;
            }
        }

        public static void txtDescription_LostFocus(object sender, RoutedEventArgs e, NewTaskDialog nwd)
        {
            if (string.IsNullOrWhiteSpace(nwd.txtDescription.Text))
            {
                nwd.txtDescription.Text = string.Empty;
            }
        }
    }
}
