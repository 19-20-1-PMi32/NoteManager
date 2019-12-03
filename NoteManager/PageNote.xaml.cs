using System;
using System.Windows;
using System.Windows.Controls;
using NoteManager.DBClasses;
using System.Linq;
using System.Windows.Input;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNote.xaml
    /// </summary>
    public partial class PageNote : Page
    {
        public PageNote()
        {
            InitializeComponent();
            InitializeDatas();
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

        private void MouseButtonDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            var t = (TreeViewItem)sender;
            var p = (from it in User.Notes
                     where it.CreationTime.ToString() == (string)t.Header
                     select it).First();
            TextBoxMain.Text = p.Text;
        }
    }
}
