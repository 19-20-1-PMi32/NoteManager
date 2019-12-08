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
using System.Windows.Threading;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Page
    {
        private bool IsPlay = false;
        private TimeSpan TotalTime;

        public VideoPlayer()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            //SliderLine.Minimum = 0;
            SliderLine.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp), true);
            MediaElementVideo.MediaOpened += MyMediaElement_MediaOpened;
        }

        private void EventMouseDownOnBlackArea(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
        }

        private void ClickOnPlay(object sender, RoutedEventArgs e)
        {
            if(IsPlay)
            {
                MediaElementVideo.Pause();
                Play.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayLightGray.png"));
                IsPlay = false;
            }
            else
            {
                MediaElementVideo.Play();
                Play.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
                IsPlay = true;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (MediaElementVideo.Source != null)
            {
                if (MediaElementVideo.NaturalDuration.HasTimeSpan)
                {
                    lableCurentTimeOfPlay.Content = String.Format("{0}", MediaElementVideo.Position.ToString(@"mm\:ss"));
                    lableAllTimeOfPlay.Content = String.Format("{0}", MediaElementVideo.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    //SliderLine.Maximum = MediaElementVideo.NaturalDuration.TimeSpan.TotalMilliseconds;
                }
            }
        }

        

        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = MediaElementVideo.NaturalDuration.TimeSpan;

            // Create a timer that will update the counters and the time slider
            DispatcherTimer timerVideoTime = new DispatcherTimer();
            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromMilliseconds(1);
            timerVideoTime.Tick += new EventHandler(timer_TickForSlider);
            timerVideoTime.Start();
        }

        void timer_TickForSlider(object sender, EventArgs e)
        {
            // Check if the movie finished calculate it's total time
            if (MediaElementVideo.NaturalDuration.TimeSpan.TotalMilliseconds > 0)
            {
                if (TotalTime.TotalMilliseconds > 0)
                {
                    // Updating time slider
                    SliderLine.Value = MediaElementVideo.Position.TotalMilliseconds /
                                       TotalTime.TotalMilliseconds;
                }
            }
        }

        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTime.TotalMilliseconds > 0)
            {
                MediaElementVideo.Position = TimeSpan.FromMilliseconds(SliderLine.Value * TotalTime.TotalMilliseconds);
            }
        }
    }
}
