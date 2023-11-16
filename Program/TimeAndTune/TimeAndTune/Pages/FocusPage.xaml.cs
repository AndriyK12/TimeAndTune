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
    /// <summary>
    /// Interaction logic for FocusPage.xaml
    /// </summary>
    
    public partial class FocusPage : Page
    {
        const uint SPI_SETSUPPRESSMESSAGES = 0x0A;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        private void MuteButton(object sender, RoutedEventArgs e)
        {
            if (!muteButtonWasPressed)
            {
                
                muteImage.Source = new BitmapImage(new Uri("/Pages/volume_image.png", UriKind.Relative));
                muteButtonWasPressed = true;
                //SystemParametersInfo(SPI_SETSUPPRESSMESSAGES, 1, IntPtr.Zero, 0);
                //DisableNotifications();
                MessageBox.Show(IsQuietHours().ToString());
            }
            else
            {
                muteImage.Source = new BitmapImage(new Uri("/Pages/mute_logo.png", UriKind.Relative));
                muteButtonWasPressed = false;
                SystemParametersInfo(SPI_SETSUPPRESSMESSAGES, 1, IntPtr.Zero, 0);

            }
        }

        public static bool IsQuietHours()
        {
            string path = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Notifications\\Settings";
            string key = "NOC_GLOBAL_SETTING_TOASTS_ENABLED";
            int? toastsEnabled = (int?)Microsoft.Win32.Registry.GetValue(path, key, 1);
            //Microsoft.Win32.Registry.SetValue(path, key, 1);
            return (toastsEnabled == 0);
        }

        bool playPauseButtonWasPressed = false;
        bool muteButtonWasPressed = false;
        string[] soundsName = {"cafeImage", "rainImage", "campFireImage", "nightCricketsImage", "trainImage", "windImage"};
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
            stopOtherSounds("");
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
            stopOtherSounds("cafeImage");

        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("rainImage");
            stopOtherSounds("rainImage");

        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("campFireImage");
            stopOtherSounds("campFireImage");

        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("nightCricketsImage");
            stopOtherSounds("nightCricketsImage");

        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("trainImage");
            stopOtherSounds("trainImage");

        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            smoothButtonTransform("windImage");
            stopOtherSounds("windImage");

        }
        /*private void MuteButton(object sender, RoutedEventArgs e)
        {
            if (!muteButtonWasPressed)
            {
                muteImage.Source = new BitmapImage(new Uri("/Pages/volume_image.png", UriKind.Relative));
                muteButtonWasPressed = true;
                
                //DisableNotifications();
            }
            else
            {
                muteImage.Source = new BitmapImage(new Uri("/Pages/mute_logo.png", UriKind.Relative));
                muteButtonWasPressed = false;

                
            }
        }*/
        
        
        
        private void smoothButtonTransform(string name)
        {
            Image image = FindName(name) as Image;
            double targetWidth = 0.0; 
            
            if (image.Width >= 580)
            {
                targetWidth = image.Width - 30;

                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/playButton.png", UriKind.Relative));
                playPauseButtonWasPressed = false;
            } 
            else
            {
                targetWidth = image.Width + 30;

                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/pauseButton.png", UriKind.Relative));
                playPauseButtonWasPressed = true;
            }

            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                To = targetWidth,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            
            image.BeginAnimation(Image.WidthProperty, widthAnimation);
            
        }
        private void stopCurrentSound()
        {
            MessageBox.Show("Works");
            PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/playButton.png", UriKind.Relative));
            
        }
        private void stopOtherSounds(string name)
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
