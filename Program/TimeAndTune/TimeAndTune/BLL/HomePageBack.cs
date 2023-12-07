namespace TimeAndTune.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using EFCore.Service;
    using TimeAndTune.DialogWindows;

    internal class HomePageBack : Page
    {
        public static void openCreateNewTaskDialog_Click(object sender, RoutedEventArgs e, HomePage homePage)
        {
            NewTaskDialog dialog = new NewTaskDialog(homePage);
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            dialog.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            dialog.Show();
        }

        public static void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            NavWindow nav = new NavWindow();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            nav.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            nav.Show();
        }

        public static void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userWnd = new UserInfoWindow();
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            userWnd.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            userWnd.Show();
        }

        public static void openTaskInfo_Click(object sender, RoutedEventArgs e, EFCore.Task t, HomePage homePage, int taskId)
        {
            AdditionalTaskInfoDialog taskInfoWnd = new AdditionalTaskInfoDialog(homePage, taskId);
            taskInfoWnd.taskId = t.Taskid;
            Window mainWnd = Window.GetWindow((DependencyObject)sender);
            mainWnd.Opacity = 0.3;
            taskInfoWnd.Activated += (s, args) =>
            {
                taskInfoWnd.txtTaskName.Text = t.Name;
                taskInfoWnd.txtDescription.Text = t.Description;
                taskInfoWnd.txtDate.Text = t.Expectedfinishtime.ToString();
                ComboBox? priorityComboBox = taskInfoWnd.priorityButton.Template
                .FindName("priorityComboBox", taskInfoWnd.priorityButton) as ComboBox;
                if (priorityComboBox != null)
                {
                    priorityComboBox.SelectedIndex = t.Priority - 1;
                }
            };
            taskInfoWnd.Closed += (s, args) =>
            {
                mainWnd.Opacity = 1.0;
            };
            taskInfoWnd.ShowDialog();
        }
    }
}
