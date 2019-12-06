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
    /// Interaction logic for AdditionMusic.xaml
    /// </summary>
    public partial class AdditionMusic : Page
    {
        string fileExtensions = "mp3,aac,flac,wav";
        FileType type = FileType.Music;
        List<File> files;
        List<File> deleted;
        public AdditionMusic()
        {
            InitializeComponent();
            files = new List<File>();
            deleted = new List<File>();
            FileList.ItemsSource = files;
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
        private File SelectedFile()
        {
            return (File)FileList.SelectedItem;
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
            var sel = SelectedFile();
            if (sel != null)
                MusicElem.Source = new Uri(sel.FilePath);
            MusicElem.IsMuted = false;
            MusicElem.Play();
        }
        private void Pause(object sender, RoutedEventArgs e)
        {
            if (MusicElem.Source != null)
                MusicElem.Pause();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (MusicElem.Source != null)
                MusicElem.Stop();
        }
        private void AddFileToList(File file)
        {
            if (!files.Contains(file, new FileComparer()))
            {
                FileList.BeginInit();
                files.Add(file);
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
