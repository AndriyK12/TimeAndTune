namespace TimeAndTune
{
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Media;
    using System.Runtime.InteropServices;
    using System.Security.RightsManagement;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Xml.Linq;
    using TimeAndTune.BLL;
    using TimeAndTune.Pages;

    public partial class FocusPage : Page
    {
        private readonly ILogger logger = Log.ForContext<FocusPage>();
        MediaPlayer mediaPlayer = new MediaPlayer();
        
        public FocusPage()
        {
            logger.Information("Initializing FocusPage");
            InitializeComponent();
            logger.Information("FocusPage initialized successfully");
            FocusPageBack.setFocusPageObject(this);
        }

        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openNavigation_Click clicked");
            FocusPageBack.openNavigationClick(sender, e);
            logger.Debug("Navigation window opened");
        }

        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            logger.Debug("openUserInfo_Click clicked");
            FocusPageBack.openUserInfoClick(sender, e);
            logger.Debug("UserInfo opened successfully");
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
        
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            logger.Debug("Slider value was changed");
            if (ProgressBar != null)
            {
                ProgressBar.Value = Slider.Value;
            }

            FocusPageBack.setVolumeValue(Slider.Value);
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("PlayButton clicked");
            FocusPageBack.PlayButton(sender, e);
        }

        private void CafeSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("CafeSoundButton clicked");
            FocusPageBack.CafeSoundButton(sender, e);
        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("RainSoundButton clicked");
            FocusPageBack.RainSoundButton(sender, e);
        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("CampFireSoundButton clicked");
            FocusPageBack.CampFireSoundButton(sender, e);
        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("NightCricketsSoundButton clicked");
            FocusPageBack.NightCricketsSoundButton(sender, e);
        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("TrainSoundButton clicked");
            FocusPageBack.TrainSoundButton(sender, e);
        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            logger.Debug("WindSoundButton clicked");
            FocusPageBack.WindSoundButton(sender, e);
        }
    }
}
