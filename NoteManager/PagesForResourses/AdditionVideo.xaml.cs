using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionVideo.xaml
    /// </summary>
    public partial class AdditionVideo : Page
    {
        string fileExtensions = "mp4,avi,mkv,mpg,wmv";
        List<File> files;
        public AdditionVideo()
        {
            InitializeComponent();
            files = new List<File>();
            //Here we must get all user video for the related note
            FileList.ItemsSource = files;
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
                File file = new File(filePath, (int)FileType.Video, (int)FileState.OnlyUploaded);
                AddFileToList(file);
            }
            else
            {
                //Push notification that file was not added(for some reasons)
            }
        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var file = (File)FileList.SelectedItem;
            if (file != null)
            {
                FileList.BeginInit();
                files.Remove(file);
                FileList.EndInit();
            }
        }
        private void SaveFiles(object sender, MouseEventArgs e)
        {
            //logic for save file to database
        }
    }
}
