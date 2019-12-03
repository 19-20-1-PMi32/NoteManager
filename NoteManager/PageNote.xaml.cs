using System;
using System.Windows;
using System.Windows.Controls;
using NoteManager.DBClasses;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNote.xaml
    /// </summary>
    public partial class PageNote : Page
    {
        private ContextMenu NoteMenu = null;
        private TreeViewItem ThatWillBeDeleted;
        private Note CurentNote = null;

        public PageNote()
        {
            InitializeComponent();
            InitializeDatas();
            NoteMenu = Resources["NoteMenu"] as ContextMenu;
        }

        private void InitializeDatas()
        {
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

        private void GoToAddFiles(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("AddFiles.xaml", UriKind.Relative);
        }

        private void SaveNote(object sender, RoutedEventArgs e)
        {
            if(TextBoxMain.Text != "")
            {
                DateTime TimeOfcreateNote = DateTime.Now;
                User.Notes.Add(new Note(TextBoxMain.Text, TimeOfcreateNote));
                AddingNewNoteInTreeView(TimeOfcreateNote);
            }
        }

        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            var parent = ThatWillBeDeleted.Parent as TreeViewItem;
            if (parent != null)
            {
                parent.Items.Remove(ThatWillBeDeleted);
            }
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

        private void MouseButtonRightClickHandler(object sender, MouseButtonEventArgs e)
        {
            NoteMenu.IsOpen = true;
            ThatWillBeDeleted = sender as TreeViewItem;
        }

        private void MouseButtonDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            var t = (TreeViewItem)sender;
            var p = (from it in User.Notes
                     where it.CreationTime.ToString() == (string)t.Header
                     select it).First();
            TextBoxMain.Text = p.Text;
            CurentNote = p;
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
    }
}
