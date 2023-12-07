using EFCore;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TimeAndTune.Pages;

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User ActiveUser { get; set; }
        public static Dictionary<int, string> taskPerformingTime = new Dictionary<int, string>();
        public static Dictionary<int, DateTime> backgroundTaskPerformingTime = new Dictionary<int, DateTime>();
        public MainWindow()
        {
            InitializeComponent();

            mainFrame.Navigate(new LoginPage());

        }

        public static void addTaskPerformingTime(int taskId, string currentTimer)
        {
            taskPerformingTime[taskId] = currentTimer;
        }

        public static string getTaskPerformingTime(int taskId)
        {
            try
            {
                return taskPerformingTime[taskId];
            }
            catch (Exception e)
            {
                return "00:00:00";
            }
        }

        public static void deleteTaskPerformingDate(int taskId)
        {
            taskPerformingTime.Remove(taskId);
        }

        public static void addTaskBackgroundPerformingTime(int taskId)
        {
            backgroundTaskPerformingTime[taskId] = DateTime.Now;
        }

        public static String getTaskBackgroundPerformingTime(int taskId)
        {
            try
            {
                DateTime backgroundPerformingTime = backgroundTaskPerformingTime[taskId];
                TimeSpan difference = DateTime.Now - backgroundPerformingTime;
                
                return difference.ToString();
            }catch (Exception e)
            {
                return "00:00:00";
            }
        }
        public static void deleteTaskBackgroundPerformingTime(int taskId)
        {
            backgroundTaskPerformingTime.Remove(taskId);
        }
    }
}
