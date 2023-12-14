namespace TimeAndTune.DialogWindows
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
    using System.Windows.Threading;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.BLL;

    public partial class AdditionalTaskInfoDialog : Window
    {

        private readonly ILogger logger = Log.ForContext<AdditionalTaskInfoDialog>();

        public AdditionalTaskInfoDialog(HomePage homePage, int sendedTaskID)
        {
            logger.Information("Initializing AdditionalTaskInfoDialog");
            InitializeComponent();
            logger.Information("AdditionalTaskInfoDialog initialized successfully");

            AdditionalTaskInfoBack.setAllVariables(sendedTaskID, homePage, this);
        }

        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            logger.Debug("goBackToHomePage clicked");
            AdditionalTaskInfoBack.goBackToHomePage(sender, e);
        }

        public void update_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("update_Click clicked");
            AdditionalTaskInfoBack.update_Click(sender, e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AdditionalTaskInfoBack.Timer_Tick(sender, e);
        }

        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            logger.Debug("txtTaskName text changed");
            AdditionalTaskInfoBack.txtTaskName_TextChanged(sender, e, this);
        }

        private void txtTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtTaskName got focus");
            AdditionalTaskInfoBack.txtTaskName_GotFocus(sender, e, this);
        }

        private void txtTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtTaskName lost focus");
            AdditionalTaskInfoBack.txtTaskName_LostFocus(sender, e, this);
        }

        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            logger.Debug("txtDate text changed");
            AdditionalTaskInfoBack.txtDate_TextChanged(sender, e, this);
        }

        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtDate got focus");
            AdditionalTaskInfoBack.txtDate_GotFocus(sender, e, this);
        }

        private void txtDate_LostFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtDate lost focus");
            AdditionalTaskInfoBack.txtDate_LostFocus(sender, e, this);
        }

        private void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            logger.Debug("txtDate preview text input");
            AdditionalTaskInfoBack.txtDate_PreviewTextInput(sender, e);
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            logger.Debug("txtDescription text changed");
            AdditionalTaskInfoBack.txtDescription_TextChanged(sender, e, this);
        }

        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtDescription got focus");
            AdditionalTaskInfoBack.txtDescription_GotFocus(sender, e, this);
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            logger.Debug("txtDescription lost focus");
            AdditionalTaskInfoBack.txtDescription_LostFocus(sender, e, this);
        }

        private void playTimerClick(object sender, RoutedEventArgs e)
        {
            logger.Debug("playTimer clicked");
            AdditionalTaskInfoBack.playTimerClick(sender, e);
        }

        private void FinishClick(object sender, RoutedEventArgs e)
        {
            logger.Debug("Finish Clicked");
            AdditionalTaskInfoBack.FinishClick(sender, e);
        }
    }
}
