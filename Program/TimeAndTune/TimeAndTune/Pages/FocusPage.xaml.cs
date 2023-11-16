using System;
using System.Collections.Generic;
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

namespace TimeAndTune
{
    
    public partial class FocusPage : Page
    {

        bool playPauseButtonWasPressed = false;
        bool muteButtonWasPressed = false;
        bool soundEffectWasPressed = false;
        string[] soundsName = {"cafeImage", "rainImage", "campFireImage", "nightCricketsImage", "trainImage", "windImage"};

        SoundPlayer playSound;
        int state = 0;

        public FocusPage()
        {
            InitializeComponent();
        }
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
            changeFocus("");
            if (playPauseButtonWasPressed)
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/play.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
                soundEffectWasPressed = false;
                if (playSound != null)
                {
                    playSound.Stop();
                }
            } 
            else if(!playPauseButtonWasPressed && soundEffectWasPressed)
            {
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/pause.png", UriKind.Relative));
                playPauseButtonWasPressed = true;
                soundEffectWasPressed = false;
            }
        }

        private void CafeSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("cafeImage");
            changeFocus("cafeImage");
            playPauseSound("cafe");
        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("rainImage");
            changeFocus("rainImage");
            playPauseSound("rain");
        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("campFireImage");
            changeFocus("campFireImage");
            playPauseSound("campFire");
        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("nightCricketsImage");
            changeFocus("nightCricketsImage");
            playPauseSound("nightCrickets");
        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("trainImage");
            changeFocus("trainImage");
            playPauseSound("train");
        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("windImage");
            changeFocus("windImage");
            playPauseSound("wind");
        }
        private void MuteButton(object sender, RoutedEventArgs e)
        {
            if (!muteButtonWasPressed)
            {
                muteImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/volume.png", UriKind.Relative));
                muteButtonWasPressed = true;
            }
            else
            {
                muteImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/mute.png", UriKind.Relative));
                muteButtonWasPressed = false;
            }
        }

        private void playPauseSound(string name)
        {
            if(state == 0)
            {
                playSound.Stop();
            } 
            else
            {
                switch (name)
                {
                    case "cafe": 
                        playSound = new SoundPlayer(Properties.Resources.cafe);
                        break;
                    case "rain":
                        playSound = new SoundPlayer(Properties.Resources.rain);
                        break;
                    case "campFire":
                        playSound = new SoundPlayer(Properties.Resources.campFire);
                        break;
                    case "nightCrickets":
                        playSound = new SoundPlayer(Properties.Resources.nightCrickets);
                        break;
                    case "train":
                        playSound = new SoundPlayer(Properties.Resources.train);
                        break;
                    case "wind":
                        playSound = new SoundPlayer(Properties.Resources.wind);
                        break;
                }
                playSound.PlayLooping();
            }
        }

        private int smoothButtonTransform(string name)
        {
            int returnValue = 0;

            Image image = FindName(name) as Image;
            double targetWidth = 0.0;
            if (image.Width >= 580)
            {
                targetWidth = image.Width - 30;
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/play.png", UriKind.Relative));
                soundEffectWasPressed = false;
                playPauseButtonWasPressed = false;
            } 
            else
            {
                targetWidth = image.Width + 30;
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/pause.png", UriKind.Relative));
                soundEffectWasPressed = true;
                playPauseButtonWasPressed = true;

                returnValue = 1;
            }

            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            image.BeginAnimation(Image.WidthProperty, widthAnimation);

            return returnValue;
        }
        private void stopCurrentSound()
        {
            MessageBox.Show("Works");
            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/playButton.png", UriKind.Relative));
            
        }
        private void changeFocus(string name)
        {
            for (int i = 0; i < soundsName.Length; i++)
            {
                if (soundsName[i] != name) 
                {
                    Image image = FindName(soundsName[i]) as Image;
                    double targetWidth = 0.0;
                    if (image.Width >= 580)
                    {
                        targetWidth = image.Width - 30;
                        DoubleAnimation widthAnimation = new DoubleAnimation
                        {
                            To = targetWidth,
                            Duration = TimeSpan.FromSeconds(0.2)
                        };

                        image.BeginAnimation(Image.WidthProperty, widthAnimation);
                    }
                }
            }
        }
    }
}
