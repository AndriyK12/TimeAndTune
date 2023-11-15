using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
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

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for FocusPage.xaml
    /// </summary>
    
    public partial class FocusPage : Page
    {
        bool playPauseButtonWasPressed = false;
        public FocusPage()
        {
            InitializeComponent();
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(ProgressBar != null)
            {
                ProgressBar.Value = Slider.Value;
            }
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            if (playPauseButtonWasPressed)
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/playButton.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
            } 
            else 
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
                playPauseButtonWasPressed = true;
            }
        }

        private void CafeSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("cafeImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("rainImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("campFireImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("nightCricketsImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("trainImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("windImage");

            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
            playPauseButtonWasPressed = true;
        }
        private void MuteButton(object sender, RoutedEventArgs e)
        {
           
        }
        private void smoothButtonTransform(string name)
        {
            Image image = FindName(name) as Image;
            double targetWidth = image.Width + 30;
            if (image.Width >= 580) targetWidth = image.Width - 30;

            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            
            image.BeginAnimation(Image.WidthProperty, widthAnimation);
            
        }
    }
}
