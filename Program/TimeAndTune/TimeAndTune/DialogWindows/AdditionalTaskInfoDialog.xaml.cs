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
using System.Windows.Threading;
using EFCore;
using EFCore.Service;

namespace TimeAndTune.DialogWindows
{

    public partial class AdditionalTaskInfoDialog : Window
    {
        public int taskId;
        bool playPauseButtonWasPressed = false;

        private DispatcherTimer timer;
        private TimeSpan elapsed;

        private HomePage homePage;

        public AdditionalTaskInfoDialog(HomePage homePage, int sendedTaskID)
        {
            InitializeComponent();

            DatabaseTaskProvider databaseTaskProvider = new DatabaseTaskProvider();
            EFCore.Task? task = databaseTaskProvider.getTaskById(sendedTaskID);

            taskId = sendedTaskID;
            this.homePage = homePage;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;

            string? performingTime = MainWindow.getTaskPerformingTime(sendedTaskID);
            string backgroundPerformingTime = MainWindow.getTaskBackgroundPerformingTime(sendedTaskID);

            if (performingTime != "00:00:00")
            {
                elapsed = parseTimeFromString(performingTime).Duration() +
                    parseTimeFromString(backgroundPerformingTime).Duration();
                TimerTextBlock.Text = elapsed.ToString();
                TimerTextBlock.Visibility = Visibility.Visible;

                if (backgroundPerformingTime != "00:00:00")
                {
                    timer.Start();
                    playPauseButtonWasPressed = true;
                    PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/pauseTimer.png", UriKind.Relative));
                    MainWindow.deleteTaskBackgroundPerformingTime(sendedTaskID);
                }
            }
            
            if (task != null && task.Completed == true)
            {
                additionalInfoCheckmark.Visibility = Visibility.Visible;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsed = elapsed.Add(TimeSpan.FromSeconds(1));

            TimerTextBlock.Text = elapsed.ToString(@"hh\:mm\:ss");
        }

        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, parseTimeFromString(TimerTextBlock.Text), false);
            
            if (currentWindow != null)
            {
                MainWindow.addTaskPerformingTime(taskId, TimerTextBlock.Text);

                if (playPauseButtonWasPressed)
                {
                    MainWindow.addTaskBackgroundPerformingTime(taskId);
                }
                timer.Stop();

                currentWindow.Close();
            }
        }
        public void update_Click(object sender, RoutedEventArgs e)
        {
            string newName = txtTaskName.Text;
            string newDesc = txtDescription.Text;
            string newDate = txtDate.Text;

            ComboBox? priorityComboBox = priorityButton.Template
                .FindName("priorityComboBox", priorityButton) as ComboBox;
            int newPrority = priorityComboBox.SelectedIndex;
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            EFCore.Task? newTask =  dataBaseTaskProvider.getTaskById(taskId);
            
            dataBaseTaskProvider.updateTaskById(taskId, newName, newDesc, newDate, newPrority);

            MainWindow.addTaskPerformingTime(taskId, TimerTextBlock.Text);
            timer.Stop();

            homePage.updateListView();

            Close();
        }
        /*protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!isClosedByUser && !noNeedToClose)
            {
                Close();
            }
        }*/
        
        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtTaskName.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTaskName.Text == "")
            {
                txtTaskName.Text = "";
            }
        }

        private void txtTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                txtTaskName.Text = "";
            }
        }

        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtDate.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDate.Text == "")
            {
                txtDate.Text = "";
            }
        }

        private void txtDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDate.Text))
            {
                txtDate.Text = "";
            }
        }

        private void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

                //if (caretIndex == 0 && (e.Text[0] > '1' || (e.Text[0] == '1' && currentText.Length > 0 && currentText[0] > '1')))
                //{
                //    e.Handled = true; // Забороняємо введення значень більше 12 для місяця
                //}
            }
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtDescription.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text == "")
            {
                txtDescription.Text = "";
            }
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "";
            }
        }

        private void playTimerClick(object sender, RoutedEventArgs e)
        {
            if (!playPauseButtonWasPressed && !timer.IsEnabled)
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/pauseTimer.png", UriKind.Relative));
                TimerTextBlock.Visibility = Visibility.Visible;
                playPauseButtonWasPressed = true;
                timer.Start();
            }
            else
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/DialogWindows/playTimer.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
                timer.Stop();
            }
        }
        private void FinishClick(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, parseTimeFromString(TimerTextBlock.Text), true);
            homePage.updateListView();

            MainWindow.deleteTaskPerformingDate(taskId);
        }

        private TimeSpan parseTimeFromString(string time)
        {
            string[] parts = time.Split(":");
            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2].Substring(0,2)));
        }
    }
}
