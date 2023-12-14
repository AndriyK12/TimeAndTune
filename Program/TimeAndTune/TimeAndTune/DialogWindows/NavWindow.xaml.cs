namespace TimeAndTune
{
    using Serilog;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using TimeAndTune.BLL;

    public partial class NavWindow : Window
    {
        private readonly ILogger logger = Log.ForContext<NavWindow>();
        public bool isClosedByUser = false;

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!isClosedByUser)
            {
                Close();
                logger.Information("NavWindow deactivated");
            }
        }

        public void navigateHome_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to HomePage");
            NavWindowBack.navigateHome_Click(sender, e, this);
            logger.Debug("Navigated to HomePage successfully");
        }

        public void navigateStatistics_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to StatisticsPage");
            NavWindowBack.navigateStatistics_Click(sender, e, this);
            logger.Debug("Navigated to StatisticsPage successfully");
        }

        public void navigateFocus_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("Navigating to FocusPage");
            NavWindowBack.navigateFocus_Click(sender, e, this);
            logger.Debug("Navigated to FocusPage successfully");
        }

        public NavWindow()
        {
            logger.Information("Initializing NavWindow");
            InitializeComponent();
            Left = 0;
            Top = 23;
            logger.Information("NavWindow initialized successfully");
        }
    }
}
