namespace TimeAndTune
{
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

    public partial class FocusPage : Page
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        
        public FocusPage()
        {
            InitializeComponent();
            FocusPageBack.setFocusPageObject(this);
        }

        public void openNavigation_Click(object sender, RoutedEventArgs e)
        {
            FocusPageBack.openNavigationClick(sender, e);
        }

        public void openUserInfo_Click(object sender, RoutedEventArgs e)
        {
            FocusPageBack.openUserInfoClick(sender, e);
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
        
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ProgressBar != null)
            {
                ProgressBar.Value = Slider.Value;
            }

            FocusPageBack.setVolumeValue(Slider.Value);
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.PlayButton(sender, e);
        }

        private void CafeSoundButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.CafeSoundButton(sender, e);
        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        { 
            FocusPageBack.RainSoundButton(sender, e);
        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.CampFireSoundButton(sender, e);
        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.NightCricketsSoundButton(sender, e);
        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.TrainSoundButton(sender, e);
        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            FocusPageBack.WindSoundButton(sender, e);
        }
    }
}
