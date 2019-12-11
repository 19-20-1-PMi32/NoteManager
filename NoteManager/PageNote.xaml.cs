using System;
using System.Windows;
using System.Windows.Controls;
using NoteManager.DBClasses;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNote.xaml
    /// </summary>
    public partial class PageNote : Page
    {
        private ContextMenu NoteMenu = null;
        private TreeViewItem ThatTreeItemIsSelected = null;
        private ListBoxItem ThatListItemIsSelected = null;
        private Note CurentNote = null;
        private TimeSpan TotalTimeOfVideo;
        private DispatcherTimer timerForToddlerOfSlider = null;
        private ushort sliderUpdateSpeed = 100;
        TimeSpan? pausePosition;

        public PageNote()
        {
            InitializeComponent();
            InitializeTreeDates();
            InitializeListDates();
            NoteMenu = Resources["NoteMenu"] as ContextMenu;
        }

        private void ClickOnUpdate(object sender, RoutedEventArgs e)
        {
            InitializeTreeDates();
            InitializeListDates();
        }

        private void InitializeTreeDates()
        {
            Dates.Items.Clear();
            var NoteYears = from year in User.Notes
                            select year.CreationTime.Year;
            NoteYears = NoteYears.Distinct();

            foreach (var item in NoteYears)
            {
                var year = new TreeViewItem() { Header = $"{item}" };
                var NoteMonths = from month in User.Notes
                                 where month.CreationTime.Year == item
                                 select month.CreationTime.Month;

                NoteMonths = NoteMonths.Distinct();
                foreach (var item2 in NoteMonths)
                {
                    var month = new TreeViewItem() { Header = $"{item2}" };

                    var NoteDays = from days in User.Notes
                                   where days.CreationTime.Year == item && days.CreationTime.Month == item2
                                   select days.CreationTime.Day;

                    NoteDays = NoteDays.Distinct();
                    foreach (var item3 in NoteDays)
                    {
                        var day = new TreeViewItem() { Header = $"{item3}" };
                        var notes = from note_ in User.Notes
                                    where note_.CreationTime.Year == item && note_.CreationTime.Month == item2 && note_.CreationTime.Day == item3
                                    select note_.CreationTime;
                        foreach (var item4 in notes)
                        {
                            var note = new TreeViewItem() { Header = $"{item4}" };
                            day.Items.Add(note);
                            note.MouseDoubleClick += MouseButtonDoubleClickHandler;
                            note.MouseRightButtonUp += MouseButtonRightClickHandler;
                        }
                        month.Items.Add(day);
                    }
                    year.Items.Add(month);
                }
                Dates.Items.Add(year);
            }
        }

        private void InitializeListDates()
        {
            ListBoxDates.Items.Clear();
            foreach (var item in User.Notes)
            {
                var date = new ListBoxItem() { Content = $"{item.CreationTime}" };
                date.MouseDoubleClick += MouseButtonDoubleClickHandler;
                date.MouseRightButtonUp += MouseButtonRightClickHandler;
                ListBoxDates.Items.Add(date);
            }
        }

        private void GoToAddFiles(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("AddFiles.xaml", UriKind.Relative);
        }

        private void SaveNote(object sender, RoutedEventArgs e)
        {
            if (TextBoxMain.Text != "")
            {
                DateTime TimeOfCreateNote = DateTime.Now;
                User.Notes.Add(new Note(TextBoxMain.Text, TimeOfCreateNote));
                AddingNewNoteInTreeView(TimeOfCreateNote);
                AddingNewNoteInListBox(TimeOfCreateNote);
            }
        }

        private void ViewNote(object sender, RoutedEventArgs e)
        {
            if (TabItemList.IsSelected)
            {
                var t = ThatListItemIsSelected;
                var p = (from it in User.Notes
                         where it.CreationTime.ToString() == (string)t.Content
                         select it).First();
                TextBoxMain.Text = p.Text;
                CurentNote = p;
            }
            else if (TabItemTree.IsSelected)
            {
                var t = ThatTreeItemIsSelected;
                var p = (from it in User.Notes
                         where it.CreationTime.ToString() == (string)t.Header
                         select it).First();
                TextBoxMain.Text = p.Text;
                CurentNote = p;
            }
        }

        private void UpdateNote(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            if (TabItemList.IsSelected)
            {
                if (ThatListItemIsSelected != null)
                {
                    User.Notes.Remove((from t in User.Notes
                                       where t.CreationTime.ToString() == (string)ThatListItemIsSelected.Content
                                       select t).First());
                    ListBoxDates.Items.Remove(ThatListItemIsSelected);
                    InitializeTreeDates();
                    ThatListItemIsSelected = null;
                }
            }
            else if (TabItemTree.IsSelected)
            {
                if (ThatTreeItemIsSelected != null)
                {
                    User.Notes.Remove((from t in User.Notes
                                       where t.CreationTime.ToString() == (string)ThatTreeItemIsSelected.Header
                                       select t).First());

                    var day = ThatTreeItemIsSelected.Parent as TreeViewItem;
                    if (day != null)
                    {
                        day.Items.Remove(ThatTreeItemIsSelected);
                        if (day.Items.Count == 0)
                        {
                            var month = day.Parent as TreeViewItem;
                            month.Items.Remove(day);
                            if (month.Items.Count == 0)
                            {
                                var year = month.Parent as TreeViewItem;
                                year.Items.Remove(month);
                                if (year.Items.Count == 0)
                                {
                                    var years = year.Parent as TreeViewItem;
                                    years.Items.Remove(year);
                                }
                            }
                        }
                    }
                    InitializeListDates();
                }
                ThatTreeItemIsSelected = null;
            }
            TextBoxMain.Text = "";
        }

        private void AddingNewNoteInTreeView(DateTime time)
        {
            bool isThisYear = false;
            bool isThisMonth = false;
            bool isThisDay = false;
            foreach (var year in Dates.Items.SourceCollection)
            {
                if ((string)(year as TreeViewItem).Header == time.Year.ToString())
                {
                    foreach (var month in (year as TreeViewItem).Items.SourceCollection)
                    {
                        if ((string)(month as TreeViewItem).Header == time.Month.ToString())
                        {
                            foreach (var day in (month as TreeViewItem).Items.SourceCollection)
                            {
                                if ((string)(day as TreeViewItem).Header == time.Day.ToString())
                                {
                                    var newNote = new TreeViewItem() { Header = $"{time}" };
                                    (day as TreeViewItem).Items.Add(newNote);
                                    newNote.MouseDoubleClick += MouseButtonDoubleClickHandler;
                                    newNote.MouseRightButtonUp += MouseButtonRightClickHandler;
                                    isThisDay = true;
                                    break;
                                }
                            }
                            if (!isThisDay)
                            {
                                var newNote = new TreeViewItem() { Header = $"{time}" };
                                var newDay = new TreeViewItem() { Header = $"{time.Day}" };
                                newDay.Items.Add(newNote);
                                (month as TreeViewItem).Items.Add(newDay);
                                newNote.MouseDoubleClick += MouseButtonDoubleClickHandler;
                                newNote.MouseRightButtonUp += MouseButtonRightClickHandler;
                            }
                            isThisMonth = true;
                            break;
                        }
                    }
                    if (!isThisMonth)
                    {
                        var newNote = new TreeViewItem() { Header = $"{time}" };
                        var newDay = new TreeViewItem() { Header = $"{time.Day}" };
                        var newMonth = new TreeViewItem() { Header = $"{time.Month}" };
                        newDay.Items.Add(newNote);
                        newMonth.Items.Add(newDay);
                        (year as TreeViewItem).Items.Add(newMonth);
                        newNote.MouseDoubleClick += MouseButtonDoubleClickHandler;
                        newNote.MouseRightButtonUp += MouseButtonRightClickHandler;
                    }
                    isThisYear = true;
                    break;
                }
            }
            if (!isThisYear)
            {
                var newNote = new TreeViewItem() { Header = $"{time}" };
                var newDay = new TreeViewItem() { Header = $"{time.Day}" };
                var newMonth = new TreeViewItem() { Header = $"{time.Month}" };
                var newYear = new TreeViewItem() { Header = $"{time.Year}" };
                newDay.Items.Add(newNote);
                newMonth.Items.Add(newDay);
                newYear.Items.Add(newMonth);
                Dates.Items.Add(newYear);
                newNote.MouseDoubleClick += MouseButtonDoubleClickHandler;
                newNote.MouseRightButtonUp += MouseButtonRightClickHandler;
            }
        }

        private void AddingNewNoteInListBox(DateTime time)
        {
            var date = new ListBoxItem() { Content = $"{time}" };
            date.MouseDoubleClick += MouseButtonDoubleClickHandler;
            date.MouseRightButtonUp += MouseButtonRightClickHandler;
            ListBoxDates.Items.Add(date);
        }

        private void MouseButtonRightClickHandler(object sender, MouseButtonEventArgs e)
        {
            NoteMenu.IsOpen = true;
            if (sender is ListBoxItem)
            {
                ThatListItemIsSelected = sender as ListBoxItem;
            }
            else if (sender is TreeViewItem)
            {
                ThatTreeItemIsSelected = sender as TreeViewItem;
            }
        }

        private void MouseButtonDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem)
            {
                var t = (TreeViewItem)sender;
                var p = (from it in User.Notes
                         where it.CreationTime.ToString() == (string)t.Header
                         select it).First();
                TextBoxMain.Text = p.Text;
                CurentNote = p;
                ShowAllFiles(new object(), new RoutedEventArgs());
            }
            else if (sender is ListBoxItem)
            {
                var t = (ListBoxItem)sender;
                var p = (from it in User.Notes
                         where it.CreationTime.ToString() == (string)t.Content
                         select it).First();
                TextBoxMain.Text = p.Text;
                CurentNote = p;
                ShowAllFiles(new object(), new RoutedEventArgs());
            }
        }

        private void ShowAllFiles(object sender, RoutedEventArgs e)
        {
            if (CurentNote != null)
            {
                ComboBoxFiles.SelectedValue = "All files";
                ListBoxResourses.Items.Clear();
                foreach (var item in CurentNote.Videos)
                {
                    ListBoxResourses.Items.Add(item);
                }

                foreach (var item in CurentNote.Pictures)
                {
                    ListBoxResourses.Items.Add(item);
                }

                foreach (var item in CurentNote.Musics)
                {
                    ListBoxResourses.Items.Add(item);
                }

                foreach (var item in CurentNote.Records)
                {
                    ListBoxResourses.Items.Add(item);
                }
            }
        }

        private void ShowVideos(object sender, RoutedEventArgs e)
        {
            if (CurentNote != null)
            {
                ListBoxResourses.Items.Clear();
                foreach (var item in CurentNote.Videos)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }
            }
        }

        private void ShowPictures(object sender, RoutedEventArgs e)
        {
            if (CurentNote != null)
            {
                ListBoxResourses.Items.Clear();
                foreach (var item in CurentNote.Pictures)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }
            }
        }

        private void ShowMusics(object sender, RoutedEventArgs e)
        {
            if (CurentNote != null)
            {
                ListBoxResourses.Items.Clear();
                foreach (var item in CurentNote.Musics)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }
            }
        }

        private void ShowRecords(object sender, RoutedEventArgs e)
        {
            if (CurentNote != null)
            {
                ListBoxResourses.Items.Clear();
                foreach (var item in CurentNote.Records)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }
            }
        }

        private void FileClick(object sender, SelectionChangedEventArgs e)
        {
            var file = SelectedFile();
            if (file is Music)
            {
                FileViewer.music = (Music)file;
                TextBoxMain.Margin = new Thickness(200, 40, 230, 28);
                Play(null, null);
            }
            else if(file is Picture)
            {
                FileViewer.picture = (Picture)file;
                FrameAddFiles.Source = new Uri("PagesForResourses/PhotoViewer.xaml", UriKind.Relative);
            }
            else if(file is Video)
            {
                FileViewer.video = (Video)file;
                FrameAddFiles.Source = new Uri("PagesForResourses/VideoPlayer.xaml", UriKind.Relative);
            }
            else
            {
                FileViewer.record = (Record)file;
            }
            //music = (Music)ListBoxResourses.SelectedItem;
        }

        public object SelectedFile()
        {
            return ListBoxResourses.SelectedItem;
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayLightGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            var sel = FileViewer.music;

            if (sel != null)
                MusicElem.Source = new Uri(sel.FilePath);
            MusicElem.IsMuted = false;
            MusicElem.Play();
            pausePosition = null;
            InitTimer();
            timerForToddlerOfSlider.Start();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            if (MusicElem.Source != null)
            {
                if (MusicElem.Position != TimeSpan.Zero && pausePosition != null)
                {
                    MusicElem.Position = (TimeSpan)pausePosition;
                    MusicElem.Play();
                    timerForToddlerOfSlider.Start();
                    pausePosition = null;
                }
                else
                {
                    pausePosition = MusicElem.Position;
                    MusicElem.Pause();
                    timerForToddlerOfSlider.Stop();
                }
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopLight.png"));
            if (MusicElem.Source != null)
                MusicElem.Stop();
            pausePosition = null;
            timerForToddlerOfSlider.Stop();
            timerForToddlerOfSlider = null;
            SliderLine.Value = 0;
        }

        private void ClosePlayer(object sender, RoutedEventArgs e)
        {
            TextBoxMain.Margin = new Thickness(200, 0, 230, 28);
            // Stop play player.
        }

        private void Timer_TickForSlider(object sender, EventArgs e)
        {
            if (MusicElem.Source != null)
            {
                if (MusicElem.NaturalDuration.HasTimeSpan)
                {
                    TotalTimeOfVideo = MusicElem.NaturalDuration.TimeSpan;
                    SliderLine.Maximum = MusicElem.NaturalDuration.TimeSpan.TotalMilliseconds;
                    SliderLine.Value += sliderUpdateSpeed;
                }
            }
        }

        private void TimeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTimeOfVideo.TotalMilliseconds > 0)
            {
                MusicElem.Position = TimeSpan.FromMilliseconds(SliderLine.Value);
            }
        }

        private void InitTimer()
        {
            if (timerForToddlerOfSlider == null)
            {
                MusicElem.Visibility = Visibility.Visible;
                MusicElem.Play();
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
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (MusicElem.Source != null)
            {
                if (MusicElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", MusicElem.Position.ToString(@"hh\:mm\:ss"), MusicElem.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
            }
        }
    }
}
