﻿namespace TimeAndTune
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using EFCore.Service;
    using TimeAndTune.BLL;
    using TimeAndTune.DialogWindows;
    using Task = EFCore.Task;

    public class CustomTaskStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = int.Parse(value.ToString());
            if (status == 3)
            {
                return "#001AFF";
            } 
            else if (status == 2)
            {
                return "#00A3FF";
            }
            else
            {
                return "#D0FFFF";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomTaskStatusToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value.ToString();
            if (status == "True")
            {
                return "/Pages/checkmark.png";
            } 
            else
            {
                return "/Pages/uncheckmark.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomTaskFinishTimeToRectangleColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly finishTime = DateOnly.Parse(value.ToString());
            if (finishTime < currentDate)
            {
                return "#82260c";
            }
            else
            {
                return "#454545";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class HomePage : Page
    {
        private readonly ILogger logger = Log.ForContext<HomePage>();
        private string filterState = "today";
        private int userId;

        public HomePage()
        {
            logger.Information("Initializing HomePage");
            InitializeComponent();
            logger.Information("StatisticPage initialized successfully");
            userId = MainWindow.ActiveUser.Userid;
            updateListView();
        }
        
        public void updateListView()
        {
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            List<Task> items = new List<EFCore.Task>();
            switch (filterState)
            {
                case "today":
                    items = dataBaseTaskProvider.getAllTasksByDayUsingUserId(userId);
                    break;
                case "week":
                    items = dataBaseTaskProvider.getAllTasksByWeekUsingUserId(userId);
                    break;
                case "month":
                    items = dataBaseTaskProvider.getAllTasksByMonthUsingUserId(userId);
                    break;
                case "allTime":
                    items = dataBaseTaskProvider.getAllSpecificTaskByUserId(userId);
                    break;
                default:
                    items = dataBaseTaskProvider.getAllTasksByDayUsingUserId(userId);
                    break;
            }
            TaskListView.ItemsSource = items;
            logger.Debug("ListView was updated");
        }

        public void openCreateNewTaskDialog_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openCreateNewTaskDialog_Click clicked");
            HomePageBack.openCreateNewTaskDialog_Click(sender, e, this);
        }

        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openNavigation_Click clicked");
            HomePageBack.openNavigation_Click(sender, e);
        }

        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openUserInfo_Click clicked");
            HomePageBack.openUserInfo_Click(sender, e);
        }

        private void Today_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            AllTimeRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            filterState = "today";
            logger.Debug("Button focus chanched to " + filterState);
            updateListView();
        }

        private void Week_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            AllTimeRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            filterState = "week";
            logger.Debug("Button focus chanched to " + filterState);
            updateListView();
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            AllTimeRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            filterState = "month";
            logger.Debug("Button focus chanched to " + filterState);
            updateListView();
        }

        private void AllTime_Click(object sender, RoutedEventArgs e)
        {
            TodayRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            AllTimeRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            filterState = "allTime";
            logger.Debug("Button focus chanched to " + filterState);
            updateListView();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            DatabaseTaskProvider dataBaseTaskProvider = new DatabaseTaskProvider();
            if (item != null && item.DataContext is Task task)
            {
                HomePageBack.openTaskInfo_Click(sender, e, task, this, task.Taskid);
            }
        }
    }
}
