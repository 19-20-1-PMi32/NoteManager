using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Page
    {
        private bool IsPlay = true;
        private TimeSpan TotalTimeOfVideo;
        private DispatcherTimer timerForToddlerOfSlider;
        private ushort sliderUpdateSpeed = 100;

        public VideoPlayer()
        {
            InitializeComponent();
            InitializeTimer();
            MediaElementVideo.Play();
            Play.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
            timerForToddlerOfSlider.Start();
        }

        private void LeavePage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
            MediaElementVideo.Stop();
        }

        private void ClickOnPlay(object sender, RoutedEventArgs e)
        {
            if(IsPlay)
            {
                MediaElementVideo.Pause();
                Play.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayLightGray.png"));
                IsPlay = false;
                timerForToddlerOfSlider.Stop();
            }
            else
            {
                MediaElementVideo.Play();
                Play.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
                IsPlay = true;
                timerForToddlerOfSlider.Start();
            }
        }

        private void Timer_TickForLabelTimer(object sender, EventArgs e)
        {
            if (MediaElementVideo.Source != null && MediaElementVideo.NaturalDuration.HasTimeSpan)
            {
                lableCurentTimeOfPlay.Content = string.Format("{0}", MediaElementVideo.Position.ToString(@"mm\:ss"));
                lableAllTimeOfPlay.Content = string.Format("{0}", MediaElementVideo.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
        }

        private void Timer_TickForSlider(object sender, EventArgs e)
        {
            if (MediaElementVideo.Source != null)
            {
                if (MediaElementVideo.NaturalDuration.HasTimeSpan)
                {
                    TotalTimeOfVideo = MediaElementVideo.NaturalDuration.TimeSpan;
                    SliderLine.Maximum = MediaElementVideo.NaturalDuration.TimeSpan.TotalMilliseconds;
                    SliderLine.Value += sliderUpdateSpeed;
                }
            }
        }

        private void TimeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTimeOfVideo.TotalMilliseconds > 0)
            {
                MediaElementVideo.Position = TimeSpan.FromMilliseconds(SliderLine.Value);
            }
        }

        private void InitializeTimer()
        {
            SliderLine.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(TimeSlider_MouseLeftButtonUp), true);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_TickForLabelTimer;
            timer.Start();

            timerForToddlerOfSlider = new DispatcherTimer();
            timerForToddlerOfSlider.Interval = TimeSpan.FromMilliseconds(sliderUpdateSpeed);
            timerForToddlerOfSlider.Tick += Timer_TickForSlider;
            timerForToddlerOfSlider.Start();
            SliderLine.Minimum = 0;
        }
    }
}
