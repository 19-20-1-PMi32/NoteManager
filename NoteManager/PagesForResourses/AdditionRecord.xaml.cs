using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using NoteManager.DBClasses;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionRecord.xaml
    /// </summary>
    public partial class AdditionRecord : Page
    {
        string fileExtensions = "mp3,aac,flac,wav";
        List<Record> records;

        TimeSpan? pausePosition;
        public AdditionRecord()
        {
            InitializeComponent();
            records = TemporaryNote.Records;
            FileList.ItemsSource = records;
        }

        private void InitTimer()
        {
            RecordElem.Visibility = Visibility.Visible;
            RecordElem.Play();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private Record SelectedFile()
        {
            return (Record)FileList.SelectedItem;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (RecordElem.Source != null)
            {
                if (RecordElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", RecordElem.Position.ToString(@"mm\:ss"), RecordElem.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lableStatus.Content = "No file selected...";
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            var sel = SelectedFile();
            if (sel != null)
                RecordElem.Source = new Uri(sel.FilePath);
            RecordElem.IsMuted = false;
            RecordElem.Play();
            pausePosition = null;
            InitTimer();
        }
        private void Pause(object sender, RoutedEventArgs e)
        {
            if (RecordElem.Source != null)
            {
                if (RecordElem.Position != TimeSpan.Zero && pausePosition != null)
                {
                    RecordElem.Position = (TimeSpan)pausePosition;
                    RecordElem.Play();
                    pausePosition = null;
                }
                else
                {
                    pausePosition = RecordElem.Position;
                    RecordElem.Pause();
                }
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (RecordElem.Source != null)
                RecordElem.Stop();
            pausePosition = null;
        }
        private void AddFileToList(Record record)
        {
            if (!records.Contains(record, new FileComparer()))
            {
                Save(record);
                UpdateList();
            }
            else
            {
                Notification.ShowMessage("Is already in list");
            }
        }
        private void AddFile(object sender, MouseEventArgs e)
        {
            // File extension must be loaded from any config file ar anywhere else
            FileUploader uploader = new FileUploader(fileExtensions);
            string filePath = uploader.Upload();
            if (String.Empty != filePath)
            {
                Record record = new Record() { FilePath = filePath, CreationTime = DateTime.Now };
                AddFileToList(record);
            }
            else
            {
                Notification.ShowMessage("Music was not loaded");
            }
        }
        private void UpdateList()
        {
            FileList.BeginInit();
            records = TemporaryNote.Records;
            FileList.EndInit();
        }
        private void DeleteFile(object sender, MouseEventArgs e)
        {
            var record = SelectedFile();
            if (record != null)
            {
                Delete(record);
                UpdateList();
            }
        }
        private void Save(Record record)
        {
            TemporaryNote.Records.Add(record);
        }
        private void Delete(Record record)
        {
            TemporaryNote.Records.Remove(record);
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
