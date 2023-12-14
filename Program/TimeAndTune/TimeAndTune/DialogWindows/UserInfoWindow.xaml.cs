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
    using System.Windows.Shapes;
    using TimeAndTune.BLL;
    using TimeAndTune.Pages;

    public partial class UserInfoWindow : Window
    {
        private readonly ILogger logger = Log.ForContext<UserInfoWindow>();
        public bool isClosedByUser = false;

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!isClosedByUser)
            {
                Close();
                logger.Information("UserInfoWindow deactivated");
            }
        }

        public void exit_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("exit_Click clicked");
            UserInfoBack.exit_Click(sender, e, this);
            logger.Debug("exit_Click completed");
        }

        public UserInfoWindow()
        {
            logger.Information("Initializing UserInfoWindow");
            InitializeComponent();
            logger.Information("UserInfoWindow initialized successfully");
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 23;
            usernameText.Text = MainWindow.ActiveUser.Username;
            emailText.Text = MainWindow.ActiveUser.Email;
            coinsAmountText.Text = $"{MainWindow.ActiveUser.Coinsamount}";
        }
    }
}
