using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionVideo.xaml
    /// </summary>
    public partial class AdditionVideo : Page
    {
        private TimeSpan TotalTimeOfVideo;
        private DispatcherTimer timerForToddlerOfSlider;
        private ushort sliderUpdateSpeed = 100;
        string fileExtensions = "mp4,avi,mkv,mpg,wmv";
        List<File> files;
        List<File> deleted;
        FileType type;
        TimeSpan? pausePosition;
        public AdditionVideo()
        {
            InitializeComponent();
            type = FileType.Video;
            files = new List<File>();
            deleted = new List<File>();
            FileList.ItemsSource = files;
        }

        private void InitTimer()
        {
            VideoElem.Visibility = Visibility.Visible;
            VideoElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            SliderLine.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(TimeSlider_MouseLeftButtonUp), true);
            SliderLine.Minimum = 0;
            timerForToddlerOfSlider = new DispatcherTimer();
            timerForToddlerOfSlider.Interval = TimeSpan.FromMilliseconds(sliderUpdateSpeed);
            timerForToddlerOfSlider.Tick += Timer_TickForSlider;
            timerForToddlerOfSlider.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (VideoElem.Source != null)
            {
                if (VideoElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", VideoElem.Position.ToString(@"hh\:mm\:ss"), VideoElem.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
            }
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayLightGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            var sel = SelectedFile();
            if (sel != null)
                VideoElem.Source = new Uri(sel.FilePath);
            VideoElem.IsMuted = false;
            VideoElem.Play();
            pausePosition = null;
            InitTimer();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            if (VideoElem.Source != null)
            {
                if (VideoElem.Position != TimeSpan.Zero && pausePosition != null) {
                    VideoElem.Position = (TimeSpan)pausePosition;
                    VideoElem.Play();
                    pausePosition = null;
                }
                else
                {
                    pausePosition = VideoElem.Position;
                    VideoElem.Pause();
                }
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopLight.png"));
            if (VideoElem.Source != null)
                VideoElem.Stop();
            pausePosition = null;
        }

        private void AddFileToList(File video)
        {
            if (!files.Contains(video, new FileComparer()))
            {
                FileList.BeginInit();
                files.Add(video);
                FileList.EndInit();
                // Push notification that item was added
            }
            else
            {
                // Push notification that item was not added(for some reasons)
            }
        }

        private void AddFile(object sender, MouseEventArgs e)
        {
            // File extension must be loaded from any config file ar anywhere else
            FileUploader uploader = new FileUploader(fileExtensions);
            string filePath = uploader.Upload();
            if (String.Empty != filePath)
            {
                File file = new File(filePath, (int)type, (int)FileState.OnlyUploaded);
                AddFileToList(file);
                Debug.WriteLine(files.Count);
                Debug.WriteLine(deleted.Count);
            }
            else
            {
                //Push notification that file was not added(for some reasons)
            }
        }

        private void DeleteFronList(File file)
        {
            file.State = FileState.MustBeDeleted;
            FileList.BeginInit();
            files.Remove(file);
            FileList.EndInit();
        }

        private File SelectedFile()
        {
            return (File)FileList.SelectedItem;
        }

        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var file = SelectedFile();
            if (file != null)
            {
                DeleteFronList(file);
                deleted.Add(file);

            }
        }

        private void SaveFiles(object sender, MouseEventArgs e)
        {
            //logic for save file to database
        }

        private void FilePlay(object sender, MouseEventArgs e)
        {
            if (SelectedFile() != null)
            {
                Stop(null, null);
                Process.Start(SelectedFile().FilePath);
            }
        }

        private void Timer_TickForSlider(object sender, EventArgs e)
        {
            if (VideoElem.Source != null)
            {
                if (VideoElem.NaturalDuration.HasTimeSpan)
                {
                    TotalTimeOfVideo = VideoElem.NaturalDuration.TimeSpan;
                    SliderLine.Maximum = VideoElem.NaturalDuration.TimeSpan.TotalMilliseconds;
                    SliderLine.Value += sliderUpdateSpeed;
                }
            }
        }

        private void TimeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTimeOfVideo.TotalMilliseconds > 0)
            {
                VideoElem.Position = TimeSpan.FromMilliseconds(SliderLine.Value);
            }
        }
    }
}
