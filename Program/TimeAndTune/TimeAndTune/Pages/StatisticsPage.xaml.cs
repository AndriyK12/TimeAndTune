namespace TimeAndTune
{
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
    using LiveCharts;
    using LiveCharts.Definitions.Charts;
    using LiveCharts.Wpf;
    using LiveCharts.Wpf.Charts.Base;
    using Serilog;
    using TimeAndTune.BLL;

    public partial class StatisticsPage : Page
    {
        private readonly ILogger logger = Log.ForContext<StatisticsPage>();

        public SeriesCollection SeriesCollection { get; set; }

        public SeriesCollection SeriesCollectionMonth { get; set; }

        public SeriesCollection SeriesCollectionYear { get; set; }

        public string[] Labels { get; set; }

        public string[] LabelsMonth { get; set; }

        public string[] LabelsYear { get; set; }

        public int overallWeekTasksAmount;
        public int overallMonthTasksAmount;
        public int overallYearTasksAmount;
        public int completedWeekTasksAmount;
        public int completedMonthTasksAmount;
        public int completedYearTasksAmount;

        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openNavigation_Click clicked");
            StatisticsPageBack.openNavigation_Click(sender, e);
            logger.Debug("Navigation window opened");
        }

        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openUserInfo_Click clicked");
            StatisticsPageBack.openUserInfo_Click(sender, e);
            logger.Debug("UserInfo opened successfully");
        }

        private void updateProgressBar(int completedTasks, int overallTasks)
        {
            logger.Debug("Updating progressBar...");
            StatisticsPageBack.updateProgressBar(completedTasks, overallTasks, this);
            logger.Debug("ProgressBar updated");
        }

        private void Week_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Week_Click clicked");
            StatisticsPageBack.Week_Click(sender, e, this);
            logger.Debug("Week graph displayed successfully");
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Month_Click clicked");
            StatisticsPageBack.Month_Click(sender, e, this);
            logger.Debug("Month graph displayed successfully");
        }

        private void Year_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Year_Click clicked");
            StatisticsPageBack.Year_Click(sender, e, this);
            logger.Debug("Year graph displayed successfully");
        }

        public StatisticsPage()
        {
            logger.Information("Initializing StatisticPage");
            InitializeComponent();
            logger.Information("StatisticPage initialized successfully");
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            int currentDayOfWeek = (int)currentDate.DayOfWeek;
            int daysToMonday = currentDayOfWeek - (int)DayOfWeek.Monday;
            DateOnly startOfWeek = currentDate.AddDays(-daysToMonday);
            DateOnly[] dates = new DateOnly[7];
            for (int i = 0; i < 7; i++)
            {
                dates[i] = startOfWeek.AddDays(i);
            }

            var tasks = new List<int>();
            DatabaseTaskProvider taskService = new DatabaseTaskProvider();
            DatabaseUserProvider userService = new DatabaseUserProvider();
            foreach (DateOnly date in dates)
            {
                int overallAmount = taskService.GetAmountOfTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                int amount = taskService.GetAmountOfCompletedTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                tasks.Add(amount);
                completedWeekTasksAmount += amount;
                overallWeekTasksAmount += overallAmount;
            }
            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x7F, 0x22, 0xAB), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x6D, 0x67, 0x70), 1));

            SeriesCollection = new SeriesCollection
                {
                new ColumnSeries
                    {
                        Title = "Completed Tasks",
                        Values = new ChartValues<int>(tasks),
                        Fill = gradientBrush,
                        MaxColumnWidth = 110
                    }
                };
            Labels = dates.Select(date => date.ToString("dd/MM")).ToArray();
            DataContext = this;
            updateProgressBar(completedWeekTasksAmount, overallWeekTasksAmount);

            DateOnly firstDayOfMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
            DateOnly lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            int daysInMonth = (int)lastDayOfMonth.Day - firstDayOfMonth.Day + 1;
            dates = new DateOnly[daysInMonth];
            for (int i = 0; i < daysInMonth; i++)
            {
                dates[i] = firstDayOfMonth.AddDays(i);
            }

            tasks = new List<int>();
            foreach (DateOnly date in dates)
            {
                int overallAmount = taskService.GetAmountOfTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                int amount = taskService.GetAmountOfCompletedTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                tasks.Add(amount);
                completedMonthTasksAmount += amount;
                overallMonthTasksAmount += overallAmount;
            }

            SeriesCollectionMonth = new SeriesCollection
                {
                new ColumnSeries
                    {
                        Title = "Completed Tasks",
                        Values = new ChartValues<int>(tasks),
                        Fill = gradientBrush,
                        MaxColumnWidth = 110
                    }
                };
            LabelsMonth = dates.Select(date => date.ToString("dd/MM")).ToArray();
            DataContext = this;

            // Year Histogram
            int currentYear = currentDate.Year;
            Dictionary<int, List<DateOnly>> dateDictionary = new Dictionary<int, List<DateOnly>>();
            for (int month = 1; month <= 12; month++)
            {
                firstDayOfMonth = new DateOnly(currentYear, month, 1);
                lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                List<DateOnly> datesOfMonth = new List<DateOnly>();
                for (DateOnly date = firstDayOfMonth; date <= lastDayOfMonth; date = date.AddDays(1))
                {
                    datesOfMonth.Add(date);
                }
                dateDictionary.Add(month, datesOfMonth);
            }
            tasks = new List<int>();
            foreach (var kvp in dateDictionary)
            {
                Console.WriteLine($"Місяць {kvp.Key}:");
                int amount_of_tasks_by_month = 0;
                int overall_amount = 0;
                foreach (var date in kvp.Value)
                {
                    overall_amount += taskService.GetAmountOfTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                    amount_of_tasks_by_month += taskService.GetAmountOfCompletedTasksByDate(date, userService.getUserID(MainWindow.ActiveUser));
                }
                tasks.Add(amount_of_tasks_by_month);
                completedYearTasksAmount += amount_of_tasks_by_month;
                overallYearTasksAmount += overall_amount;
            }
            SeriesCollectionYear = new SeriesCollection
                {
                new ColumnSeries
                    {
                        Title = "Completed Tasks",
                        Values = new ChartValues<int>(tasks),
                        Fill = gradientBrush,
                        MaxColumnWidth = 110
                    }
                };
            string[] months =
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };
            LabelsYear = months;
            DataContext = this;
        }
    }
}
