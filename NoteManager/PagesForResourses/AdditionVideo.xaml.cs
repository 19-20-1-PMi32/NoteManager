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
using System.Diagnostics;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionVideo.xaml
    /// </summary>
    public partial class AdditionVideo : Page
    {
        List<File> videos;
        public AdditionVideo()
        {
            InitializeComponent();
            videos = new List<File>();
            //Here we must get all user video for the related note
            VideoList.ItemsSource = videos;
        }

        private void DoubleClickOnVideo1(object sender, RoutedEventArgs e)
        {
            VideosElem.Source = new Uri(@"E:\PROGRAMMING\My_Projects\C#\WPF\NoteManager\NoteManager\Resources\Me_vs_Bugs.wmv");
            VideosElem.Visibility = Visibility.Visible;
            VideosElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (VideosElem.Source != null)
            {
                if (VideosElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", VideosElem.Position.ToString(@"mm\:ss"), VideosElem.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lableStatus.Content = "No file selected...";
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            VideosElem.Play();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            VideosElem.Pause();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            VideosElem.Stop();
        }

        private void addVideoToList(File video)
        {
            VideoList.BeginInit();
            videos.Add(video);
            VideoList.EndInit();
            // Push notification that video was added
            Debug.WriteLine("Video was added succesfully");
        }

        private void AddVideo(object sender, MouseEventArgs e)
        {
            FileUploader uploader = new FileUploader("avi,wmv,mp4,mpg,mkv");
            string fileName = uploader.Upload();
            // TODO: add checking is video already in list
            if (String.Empty != fileName) {
                File video = new File(fileName, (int)FileType.Video, (int)FileState.OnlyUploaded);
                addVideoToList(video);
                Debug.WriteLine(video.FileName);
                Debug.WriteLine(fileName);
            }
            else
            {
                //Push notification that video was not added(for some reasons)
            }
        }

        private void SaveFiles(object sender, MouseButtonEventArgs e)
        {
            //Some actions to save file to db
            foreach (var i in videos.Where(x => x.State == FileState.OnlyUploaded))
            {
                //some actions for save video to database
            }
        }
    }
}
