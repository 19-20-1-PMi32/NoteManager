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
    /// Interaction logic for AdditionMusic.xaml
    /// </summary>
    public partial class AdditionMusic : Page
    {
        /// <summary>
        /// 
        /// </summary>
        public AdditionMusic()
        {
            InitializeComponent();
        }

        private void ClickOnMusic1(object sender, RoutedEventArgs e)
        {
            MusicElem.Source = new Uri(@"E:\PROGRAMMING\My_Projects\C#\WPF\NoteManager\NoteManager\Resources\MR_Moment.mp3");
            MusicElem.Visibility = Visibility.Visible;
            MusicElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void ClickOnMusic2(object sender, RoutedEventArgs e)
        {
            MusicElem.Source = new Uri(@"E:\PROGRAMMING\My_Projects\C#\WPF\NoteManager\NoteManager\Resources\GMV The Show.mp3");
            MusicElem.Visibility = Visibility.Visible;
            MusicElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (MusicElem.Source != null)
            {
                if (MusicElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", MusicElem.Position.ToString(@"mm\:ss"), MusicElem.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lableStatus.Content = "No file selected...";
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            MusicElem.Play();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            MusicElem.Pause();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            MusicElem.Stop();
        }
    }
}
