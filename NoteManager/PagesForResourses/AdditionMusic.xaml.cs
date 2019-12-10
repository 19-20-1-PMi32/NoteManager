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

using NoteManager.DBClasses;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionMusic.xaml
    /// </summary>
    public partial class AdditionMusic : Page
    {
        string fileExtensions = "mp3,aac,flac,wav";
        List<Music> musics;

        TimeSpan? pausePosition;
        public AdditionMusic()
        {
            InitializeComponent();
            musics = TemporaryNote.Musics;
            FileList.ItemsSource = musics;
        }

        private void InitTimer()
        {
            MusicElem.Visibility = Visibility.Visible;
            MusicElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private Music SelectedFile()
        {
            return (Music)FileList.SelectedItem;
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
            pausePosition = null;
            InitTimer();
        }
        private void Pause(object sender, RoutedEventArgs e)
        {
            if (MusicElem.Source != null)
            {
                if (MusicElem.Position != TimeSpan.Zero && pausePosition != null)
                {
                    MusicElem.Position = (TimeSpan)pausePosition;
                    MusicElem.Play();
                    pausePosition = null;
                }
                else
                {
                    pausePosition = MusicElem.Position;
                    MusicElem.Pause();
                }
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (MusicElem.Source != null)
                MusicElem.Stop();
            pausePosition = null;
        }
        private void AddFileToList(Music music)
        {
            if (!musics.Contains(music, new FileComparer()))
            {
                Save(music);
                UpdateList();
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
                Music music = new Music() { FilePath = filePath, CreationTime = DateTime.Now };
                AddFileToList(music);
            }
            else
            {
                //Push notification that file was not added(for some reasons)
            }
        }
        private void UpdateList()
        {
            FileList.BeginInit();
            musics = TemporaryNote.Musics;
            FileList.EndInit();
        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var music = SelectedFile();
            if (music != null)
            {
                Delete(music);
                UpdateList();
            }
        }
        private void Save(Music music)
        {
            TemporaryNote.Musics.Add(music);
        }
        private void Delete(Music music)
        {
            TemporaryNote.Musics.Remove(music);
        }
        private void FilePlay(object sender, MouseEventArgs e)
        {
            if (SelectedFile() != null)
            {
                Stop(null, null);
                Process.Start(SelectedFile().FilePath);
            }
        }
    }
}
