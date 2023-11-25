using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TimeAndTune.DialogWindows;
using EFCore.Service;

namespace TimeAndTune.BLL
{
    internal class StatisticsPageBack : Page
    {
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
        public static void updateProgressBar(int completedTasks, int overallTasks, StatisticsPage sp)
        {
            float percentage = ((float)completedTasks / (float)overallTasks);
            sp.progressPercentage.Text = (percentage * 100).ToString() + "%";
            sp.progressThumb.Height = 578 * percentage;
        }
        public static void Week_Click(object sender, RoutedEventArgs e, StatisticsPage sp)
        {
            sp.WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            sp.MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.WeekHistogram.Visibility = Visibility.Visible;
            sp.MonthHistogram.Visibility = Visibility.Hidden;
            sp.YearHistogram.Visibility = Visibility.Hidden;
            updateProgressBar(sp.completedWeekTasksAmount, sp.overallWeekTasksAmount, sp);
        }
        public static void Month_Click(object sender, RoutedEventArgs e, StatisticsPage sp)
        {
            sp.WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            sp.YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.WeekHistogram.Visibility = Visibility.Hidden;
            sp.MonthHistogram.Visibility = Visibility.Visible;
            sp.YearHistogram.Visibility = Visibility.Hidden;
            updateProgressBar(sp.completedMonthTasksAmount, sp.overallMonthTasksAmount, sp);
        }
        public static void Year_Click(object sender, RoutedEventArgs e, StatisticsPage sp)
        {
            sp.WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            sp.YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            sp.WeekHistogram.Visibility = Visibility.Hidden;
            sp.MonthHistogram.Visibility = Visibility.Hidden;
            sp.YearHistogram.Visibility = Visibility.Visible;
            updateProgressBar(sp.completedYearTasksAmount, sp.overallYearTasksAmount, sp);
        }
    }
}
