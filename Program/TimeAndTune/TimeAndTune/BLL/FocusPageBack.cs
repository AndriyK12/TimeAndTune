namespace TimeAndTune.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using EFCore.Service;
    using TimeAndTune.DialogWindows;
    using TimeAndTune.Pages;
    using Serilog;

    internal class FocusPageBack : Page
    {
        private static readonly ILogger logger = Log.ForContext<FocusPageBack>();

        static FocusPage fp;
        static bool playPauseButtonWasPressed = false;
        static bool muteButtonWasPressed = false;
        static bool soundEffectWasPressed = false;
        static string[] soundsName = { "cafeImageBackGround", "rainImageBackGround", "campFireImageBackGround", "nightCricketsImageBackGround", "trainImageBackGround", "windImageBackGround" };

        static MediaPlayer mediaPlayer = new MediaPlayer();
        static int state = 0;

        public static void setFocusPageObject(FocusPage fp_)
        {
            fp = fp_;
            logger.Debug("FocusPage object is successfully set");
        }

        public static void openNavigationClick(object sender, RoutedEventArgs e)
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

        public static void openUserInfoClick(object sender, RoutedEventArgs e)
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

        public static void setVolumeValue(double value)
        { 
            if (mediaPlayer != null)
            {
                mediaPlayer.Volume = (float)(value / 100.0);
            }
            logger.Debug("New volume value is successfully set");
        }

        public static void PlayButton(object sender, RoutedEventArgs e)
        {
            changeFocus(string.Empty);
            if (playPauseButtonWasPressed)
            {
                fp.PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/play.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
                soundEffectWasPressed = false;
                if (mediaPlayer != null)
                {
                    mediaPlayer.Stop();
                }

            }
            else if (!playPauseButtonWasPressed && soundEffectWasPressed)
            {
                fp.PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/pause.png", UriKind.Relative));
                playPauseButtonWasPressed = true;
                soundEffectWasPressed = false;
            }
        }

        public static void CafeSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("cafeImageBackGround");
            changeFocus("cafeImageBackGround");
            playPauseSound("cafe");
        }

        public static void RainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("rainImageBackGround");
            changeFocus("rainImageBackGround");
            playPauseSound("rain");
        }

        public static void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("campFireImageBackGround");
            changeFocus("campFireImageBackGround");
            playPauseSound("campFire");
        }

        public static void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("nightCricketsImageBackGround");
            changeFocus("nightCricketsImageBackGround");
            playPauseSound("nightCrickets");
        }

        public static void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("trainImageBackGround");
            changeFocus("trainImageBackGround");
            playPauseSound("train");
        }

        public static void WindSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("windImageBackGround");
            changeFocus("windImageBackGround");
            playPauseSound("wind");
        }

        public static void playPauseSound(string name)
        {
            if (state == 0)
            {
                mediaPlayer.Stop();
                logger.Debug("Sound effect playback stopped");
            }
            else
            {
                switch (name)
                {
                    case "cafe":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/cafe.wav"));
                        break;
                    case "rain":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/rain1.wav"));
                        break;
                    case "campFire":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/campFire.wav"));
                        break;
                    case "nightCrickets":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/nightCrickets.wav"));
                        break;
                    case "train":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/train.wav"));
                        break;
                    case "wind":
                        mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Pages/FocusPageSounds/wind.wav"));
                        break;
                }

                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                mediaPlayer.Play();
                logger.Debug("Sound effect playback started");
            }
        }

        public static int smoothButtonTransform(string name)
        {
            int returnValue = 0;

            Image? image = fp.FindName(name) as Image;

            double targetOpacity = 0.0;
            if (image.Opacity == 1)
            {
                targetOpacity = 0.0;
                fp.PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/play.png", UriKind.Relative));
                soundEffectWasPressed = false;
                playPauseButtonWasPressed = false;
            }
            else
            {
                targetOpacity = 1.0;
                fp.PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/pause.png", UriKind.Relative));
                soundEffectWasPressed = true;
                playPauseButtonWasPressed = true;

                returnValue = 1;
            }

            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                To = targetOpacity,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            image.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            return returnValue;
        }

        public static void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }

        public static void changeFocus(string name)
        {
            
            for (int i = 0; i < soundsName.Length; i++)
            {
                if (soundsName[i] != name)
                {
                    Image? image = fp.FindName(soundsName[i]) as Image;
                    DoubleAnimation opacityAnimation = new DoubleAnimation
                    {
                        To = 0.0,
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    if (image != null)
                    {
                        image.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                    }
                }
            }
            logger.Debug("Focus changed");
        }
    }
}
