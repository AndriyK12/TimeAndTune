namespace TimeAndTune.DialogWindows
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
    using System.Windows.Threading;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.BLL;

    public partial class AdditionalTaskInfoDialog : Window
    {
        public int taskId;
        bool playPauseButtonWasPressed = false;

        private DispatcherTimer timer;
        private TimeSpan elapsed;

        private HomePage homePage;

        private EFCore.Task? currentTask;

        public AdditionalTaskInfoDialog(HomePage homePage, int sendedTaskID)
        {
            InitializeComponent();

            DatabaseTaskProvider databaseTaskProvider = new DatabaseTaskProvider();
            this.currentTask = databaseTaskProvider.getTaskById(sendedTaskID);

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

            if (currentTask != null && currentTask.Completed == true)
            {
                additionalInfoCheckmark.Visibility = Visibility.Visible;
            }
        }

        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            
            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, parseTimeFromString(TimerTextBlock.Text), currentTask.Completed);
            
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
            EFCore.Task? newTask = dataBaseTaskProvider.getTaskById(taskId);

            dataBaseTaskProvider.updateTaskById(taskId, newName, newDesc, newDate, newPrority);

            MainWindow.addTaskPerformingTime(taskId, TimerTextBlock.Text);
            timer.Stop();

            homePage.updateListView();

            Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsed = elapsed.Add(TimeSpan.FromSeconds(1));

            TimerTextBlock.Text = elapsed.ToString(@"hh\:mm\:ss");
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

        private void playTimerClick(object sender, RoutedEventArgs e)
        {
            if (currentTask.Completed == true)
            {
                return;
            }

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

            if (currentTask.Completed == true)
            {
                return;
            }

            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            int hoursSpent = parseTimeFromString(TimerTextBlock.Text).Hours;
            if (!dataBaseTaskProvider.getCompleted(currentTask))
            {
                DatabaseUserProvider userProvider = new DatabaseUserProvider();
                userProvider.addCoinsForAUserById(MainWindow.ActiveUser.Userid, (10 + hoursSpent));
                MainWindow.ActiveUser = userProvider.getUserByEmail(MainWindow.ActiveUser.Email);
            }

            dataBaseTaskProvider.updateTaskExecutiontimeById(taskId, parseTimeFromString(TimerTextBlock.Text), true);
            homePage.updateListView();
            MainWindow.deleteTaskPerformingDate(taskId);
        }

        private TimeSpan parseTimeFromString(string time)
        {
            string[] parts = time.Split(":");
            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2].Substring(0, 2)));
        }
    }
}
