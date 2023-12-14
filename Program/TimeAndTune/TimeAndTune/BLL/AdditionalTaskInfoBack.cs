// <copyright file="AdditionalTaskInfoBack.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimeAndTune.BLL
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
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
    using System.Windows.Threading;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.DialogWindows;

    internal class AdditionalTaskInfoBack : Window
    {
        public static int taskId;
        static bool playPauseButtonWasPressed = false;
        private static DispatcherTimer timer;
        private static TimeSpan elapsed;
        private static HomePage homePage;
        private static EFCore.Task? currentTask;
        private static AdditionalTaskInfoDialog atid;

        private readonly static ILogger logger = Log.ForContext<AdditionalTaskInfoBack>();

        public static void setAllVariables(
            int taskId_, 
            HomePage homePage_,
            AdditionalTaskInfoDialog atid_)
        {
            taskId = taskId_;
            homePage = homePage_;
            atid = atid_;

            DatabaseTaskProvider databaseTaskProvider = new DatabaseTaskProvider();
            currentTask = databaseTaskProvider.getTaskById(taskId);

            string? performingTime = MainWindow.getTaskPerformingTime(taskId);
            string backgroundPerformingTime = MainWindow.getTaskBackgroundPerformingTime(taskId);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            if (performingTime != "00:00:00")
            {
                elapsed = AdditionalTaskInfoBack.parseTimeFromString(performingTime).Duration() +
                    AdditionalTaskInfoBack.parseTimeFromString(backgroundPerformingTime).Duration();
                atid.TimerTextBlock.Text = elapsed.ToString();
                atid.TimerTextBlock.Visibility = Visibility.Visible;

                if (backgroundPerformingTime != "00:00:00")
                {
                    timer.Start();
                    playPauseButtonWasPressed = true;
                    atid.PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/pauseTimer.png", UriKind.Relative));
                    MainWindow.deleteTaskBackgroundPerformingTime(taskId);
                }
            }

            if (currentTask != null && currentTask.Completed == true)
            {
                atid.additionalInfoCheckmark.Visibility = Visibility.Visible;
            }
        }

        public static void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();

            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, parseTimeFromString(atid.TimerTextBlock.Text), currentTask.Completed);

            if (currentWindow != null)
            {
                MainWindow.addTaskPerformingTime(taskId, atid.TimerTextBlock.Text);

                if (playPauseButtonWasPressed)
                {
                    MainWindow.addTaskBackgroundPerformingTime(taskId);
                }

                timer.Stop();

                currentWindow.Close();
            }
        }

        public static void playTimerClick(object sender, RoutedEventArgs e)
        {
            if (currentTask.Completed == true)
            {
                return;
            }

            if (!playPauseButtonWasPressed && !timer.IsEnabled)
            {
                atid.PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/pauseTimer.png", UriKind.Relative));
                atid.TimerTextBlock.Visibility = Visibility.Visible;
                playPauseButtonWasPressed = true;
                timer.Start();
            }
            else
            {
                atid.PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/playTimer.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
                timer.Stop();
            }
        }

        public static void update_Click(object sender, RoutedEventArgs e)
        {
            string newName = atid.txtTaskName.Text;
            string newDesc = atid.txtDescription.Text;
            string newDate = atid.txtDate.Text;

            ComboBox? priorityComboBox = atid.priorityButton.Template
                .FindName("priorityComboBox", atid.priorityButton) as ComboBox;
            int newPrority = priorityComboBox.SelectedIndex;
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            EFCore.Task? newTask = dataBaseTaskProvider.getTaskById(taskId);

            dataBaseTaskProvider.updateTaskById(taskId, newName, newDesc, newDate, newPrority);

            MainWindow.addTaskPerformingTime(taskId, atid.TimerTextBlock.Text);
            timer.Stop();

            homePage.updateListView();
            logger.Debug("update_Click clicked");
            atid.Close();
            logger.Debug("Task updated successfully");
        }
        
        public static void Timer_Tick(object sender, EventArgs e)
        {
            elapsed = elapsed.Add(TimeSpan.FromSeconds(1));

            atid.TimerTextBlock.Text = elapsed.ToString(@"hh\:mm\:ss");
        }

        public static void FinishClick(object sender, EventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                currentWindow.Close();
            }

            if (currentTask.Completed == true)
            {
                return;
            }

            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            int hoursSpent = AdditionalTaskInfoBack.parseTimeFromString(atid.TimerTextBlock.Text).Hours;
            if (!dataBaseTaskProvider.getCompleted(currentTask))
            {
                DatabaseUserProvider userProvider = new DatabaseUserProvider();
                userProvider.addCoinsForAUserById(MainWindow.ActiveUser.Userid, (10 + hoursSpent));
                MainWindow.ActiveUser = userProvider.getUserByEmail(MainWindow.ActiveUser.Email);
            }

            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, AdditionalTaskInfoBack.parseTimeFromString(atid.TimerTextBlock.Text), true);
            homePage.updateListView();
            MainWindow.deleteTaskPerformingDate(taskId);
        }

        public static void txtTaskName_TextChanged(object sender, TextChangedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newText = atid.txtTaskName.Text;
        }

        public static void txtTaskName_GotFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (atid.txtTaskName.Text == string.Empty)
            {
                atid.txtTaskName.Text = string.Empty;
            }
        }

        public static void txtTaskName_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtTaskName.Text))
            {
                atid.txtTaskName.Text = string.Empty;
            }
        }

        public static void txtDate_TextChanged(object sender, TextChangedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            string newText = atid.txtDate.Text;
        }

        public static void txtDate_GotFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (atid.txtDate.Text == string.Empty)
            {
                atid.txtDate.Text = string.Empty;
            }
        }

        public static void txtDate_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtDate.Text))
            {
                atid.txtDate.Text = string.Empty;
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
            if (atid.txtDescription.Text == string.Empty)
            {
                atid.txtDescription.Text = string.Empty;
            }
        }

        public static void txtDescription_LostFocus(object sender, RoutedEventArgs e, AdditionalTaskInfoDialog atid)
        {
            if (string.IsNullOrWhiteSpace(atid.txtDescription.Text))
            {
                atid.txtDescription.Text = string.Empty;
            }
        }

        public static TimeSpan parseTimeFromString(string time)
        {
            string[] parts = time.Split(":");
            logger.Debug("Parsing time data from string");
            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2].Substring(0, 2)));
        }
    }
}
