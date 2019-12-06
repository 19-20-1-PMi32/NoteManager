using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        List<File> deleted;
        public AdditionVideo()
        {
            InitializeComponent();
            files = new List<File>();
            deleted = new List<File>();
            //Here we must get all user video for the related note
            FileList.ItemsSource = files;
        }

        private void DoubleClickOnVideo1(object sender, RoutedEventArgs e)
        {
            VideoElem.Source = new Uri(@"E:\PROGRAMMING\My_Projects\C#\WPF\NoteManager\NoteManager\Resources\Me_vs_Bugs.wmv");
            VideoElem.Visibility = Visibility.Visible;
            VideoElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (VideoElem.Source != null)
            {
                if (VideoElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", VideoElem.Position.ToString(@"mm\:ss"), VideoElem.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lableStatus.Content = "No file selected...";
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            var sel = SelectedFile();
            if (sel != null)
                VideoElem.Source = new Uri(sel.FilePath);
            VideoElem.IsMuted = false;
            VideoElem.Play();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            if(VideoElem.Source != null)
                VideoElem.Pause();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (VideoElem.Source != null)
                VideoElem.Stop();
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
            Debug.WriteLine(files.Count);
            Debug.WriteLine(deleted.Count);
        }
        private void SaveFiles(object sender, MouseEventArgs e)
        {
            //logic for save file to database
        }
    }
}
