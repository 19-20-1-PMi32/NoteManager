using System;
using System.Windows;
using System.Windows.Controls;
using NoteManager.DBClasses;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
        private DispatcherTimer timerForToddlerOfSlider;
        private ushort sliderUpdateSpeed = 100;
        TimeSpan? pausePosition;

        public PageNote()
        {
            InitializeComponent();
            InitializeTreeDates();
            InitializeListDates();
            NoteMenu = Resources["NoteMenu"] as ContextMenu;
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
            if(TextBoxMain.Text != "")
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
            else if(TabItemTree.IsSelected)
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
            if(TabItemList.IsSelected)
            {
                if(ThatListItemIsSelected != null)
                {
                    User.Notes.Remove((from t in User.Notes
                                       where t.CreationTime.ToString() == (string)ThatListItemIsSelected.Content
                                       select t).First());
                    ListBoxDates.Items.Remove(ThatListItemIsSelected);
                    InitializeTreeDates();
                    ThatListItemIsSelected = null;
                }
            }
            else if(TabItemTree.IsSelected)
            {
                if(ThatTreeItemIsSelected != null)
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
                                if(year.Items.Count == 0)
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
                if((string)(year as TreeViewItem).Header == time.Year.ToString())
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
                            if(!isThisDay)
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
            if(sender is ListBoxItem)
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
            if(sender is TreeViewItem)
            {
                var t = (TreeViewItem)sender;
                var p = (from it in User.Notes
                         where it.CreationTime.ToString() == (string)t.Header
                         select it).First();
                TextBoxMain.Text = p.Text;
                CurentNote = p;
                ShowAllFiles(new object(), new RoutedEventArgs());
            }
            else if(sender is ListBoxItem)
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
                    ListBoxResourses.Items.Add(item.Name);
                }

                foreach (var item in CurentNote.Pictures)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }

                foreach (var item in CurentNote.Musics)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }

                foreach (var item in CurentNote.Records)
                {
                    ListBoxResourses.Items.Add(item.Name);
                }
            }
        }

        private void ShowVideos(object sender, RoutedEventArgs e)
        {
            if(CurentNote != null)
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

        private void ClickDoubleOnListBoxResourses(object sender, RoutedEventArgs e)
        {
            TextBoxMain.Margin = new Thickness(200, 40, 230, 28);
            //FrameAddFiles.Source = new Uri("PagesForResourses/PhotoViewer.xaml", UriKind.Relative);

        }

        private void InitTimer()
        {
            MusicAndRecordElem.Visibility = Visibility.Visible;
            MusicAndRecordElem.Play();
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
            if (MusicAndRecordElem.Source != null)
            {
                if (MusicAndRecordElem.NaturalDuration.HasTimeSpan)
                    lableStatus.Content = String.Format("{0} / {1}", MusicAndRecordElem.Position.ToString(@"hh\:mm\:ss"), MusicAndRecordElem.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
            }
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayLightGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            //var sel = SelectedFile();
            //if (sel != null)
            //    MusicAndRecordElem.Source = new Uri(sel.FilePath);
            MusicAndRecordElem.IsMuted = false;
            MusicAndRecordElem.Play();
            pausePosition = null;
            InitTimer();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeLight.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopDark.png"));
            if (MusicAndRecordElem.Source != null)
            {
                if (MusicAndRecordElem.Position != TimeSpan.Zero && pausePosition != null)
                {
                    MusicAndRecordElem.Position = (TimeSpan)pausePosition;
                    MusicAndRecordElem.Play();
                    pausePosition = null;
                }
                else
                {
                    pausePosition = MusicAndRecordElem.Position;
                    MusicAndRecordElem.Pause();
                }
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            ImagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonPlayDarkGray.png"));
            ImagePause.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonResumeDark.png"));
            ImageStop.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/buttonStopLight.png"));
            if (MusicAndRecordElem.Source != null)
                MusicAndRecordElem.Stop();
            pausePosition = null;
        }

        private void Timer_TickForSlider(object sender, EventArgs e)
        {
            if (MusicAndRecordElem.Source != null)
            {
                if (MusicAndRecordElem.NaturalDuration.HasTimeSpan)
                {
                    TotalTimeOfVideo = MusicAndRecordElem.NaturalDuration.TimeSpan;
                    SliderLine.Maximum = MusicAndRecordElem.NaturalDuration.TimeSpan.TotalMilliseconds;
                    SliderLine.Value += sliderUpdateSpeed;
                }
            }
        }

        private void TimeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTimeOfVideo.TotalMilliseconds > 0)
            {
                MusicAndRecordElem.Position = TimeSpan.FromMilliseconds(SliderLine.Value);
            }
        }

        private void ClosePlayer(object sender, RoutedEventArgs e)
        {
            TextBoxMain.Margin = new Thickness(200, 0, 230, 28);
            // Stop play player.
        }
    }
}
