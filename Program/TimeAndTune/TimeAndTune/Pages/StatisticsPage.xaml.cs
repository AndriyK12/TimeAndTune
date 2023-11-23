using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using EFCore;
using EFCore.Service;

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public void openNavigation_Click(object sender, RoutedEventArgs e)
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
        public void openUserInfo_Click(object sender, RoutedEventArgs e)
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
        private void Week_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            //add some logic
        }

        private void Year_Click(object sender, RoutedEventArgs e)
        {
            WeekRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            MonthRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#353535"));
            YearRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7A7373"));
            //add some logic
        }
        public StatisticsPage()
        {
            InitializeComponent();

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            int currentDayOfWeek = (int)currentDate.DayOfWeek;
            int daysToMonday = currentDayOfWeek - (int)DayOfWeek.Monday;
            DateOnly startOfWeek = currentDate.AddDays(-daysToMonday);
            DateOnly[] weekDates = new DateOnly[7];
            for (int i = 0; i < 7; i++)
            {
                weekDates[i] = startOfWeek.AddDays(i);
            }

            var completedTasks = new List<int>();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            DatabaseUserProvider userService = new DatabaseUserProvider();
            foreach (DateOnly date in weekDates)
            {
                completedTasks.Add(taskService.GetAmountOfCompletedTasksByDate(date, userService.getUserID(MainWindow.ActiveUser)));
            }

           /* var completedTasks = new List<int>();

            for (int i = 6; i >= 0; i--)
            {
                completedTasks.Add(i + 1); // Кількість завдань від 1 до 7
            }*/

            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x7F, 0x22, 0xAB), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x6D, 0x67, 0x70), 1));

            // Побудова гістограми
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed Tasks",
                    Values = new ChartValues<int>(completedTasks),
                    Fill = gradientBrush,
                    MaxColumnWidth = 110
        }
            };

            Labels = weekDates.Select(date => date.ToString("dd/MM")).ToArray();

            DataContext = this;
        }
    }
}
