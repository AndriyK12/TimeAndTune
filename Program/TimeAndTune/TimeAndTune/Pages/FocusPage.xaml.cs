﻿using System;
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

namespace TimeAndTune
{
    
    public partial class FocusPage : Page
    {

        bool playPauseButtonWasPressed = false;
        bool muteButtonWasPressed = false;
        bool soundEffectWasPressed = false;
        string[] soundsName = { "cafeImageBackGround", "rainImageBackGround", "campFireImageBackGround", "nightCricketsImageBackGround", "trainImageBackGround", "windImageBackGround" };

        MediaPlayer mediaPlayer = new MediaPlayer();
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
            if(mediaPlayer != null)
            {
                mediaPlayer.Volume = (float)(Slider.Value/100.0);
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
                if (mediaPlayer != null)
                {
                    mediaPlayer.Stop();
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
            state = smoothButtonTransform("cafeImageBackGround");
            changeFocus("cafeImageBackGround");
            playPauseSound("cafe");

        }

        private void RainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("rainImageBackGround");
            changeFocus("rainImageBackGround");
            playPauseSound("rain");
        }

        private void CampFireSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("campFireImageBackGround");
            changeFocus("campFireImageBackGround");
            playPauseSound("campFire");
        }

        private void NightCricketsSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("nightCricketsImageBackGround");
            changeFocus("nightCricketsImageBackGround");
            playPauseSound("nightCrickets");
        }

        private void TrainSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("trainImageBackGround");
            changeFocus("trainImageBackGround");
            playPauseSound("train");
        }

        private void WindSoundButton(object sender, RoutedEventArgs e)
        {
            state = smoothButtonTransform("windImageBackGround");
            changeFocus("windImageBackGround");
            playPauseSound("wind");
        }
        
        private void playPauseSound(string name)
        {
            if(state == 0)
            {
                mediaPlayer.Stop();
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
            }
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
        private int smoothButtonTransform(string name)
        {
            int returnValue = 0;

            Image image = FindName(name) as Image;

            double targetOpacity = 0.0;
            if (image.Opacity == 1)
            {
                targetOpacity = 0.0;
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/play.png", UriKind.Relative));
                soundEffectWasPressed = false;
                playPauseButtonWasPressed = false;
            }
            else
            {
                targetOpacity = 1.0;
                PlayPauseImage.Source = new BitmapImage(new Uri("/Pages/FocusPageImages/pause.png", UriKind.Relative));
                soundEffectWasPressed = true;
                playPauseButtonWasPressed = true;

                returnValue = 1;
            }
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                To = targetOpacity,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            image.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            return returnValue;
        }
            
        private void changeFocus(string name)
        {
            for (int i = 0; i < soundsName.Length; i++)
            {
                if (soundsName[i] != name) 
                {
                    Image image = FindName(soundsName[i]) as Image;
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
        }
    }
}
