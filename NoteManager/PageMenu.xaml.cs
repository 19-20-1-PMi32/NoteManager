using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoteManager.DBClasses;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNotes.xaml
    /// </summary>
    public partial class PageNotes : Page
    {
        private static Color colorBlue = Color.FromRgb(0, 122, 204);
        public PageNotes()
        {
            InitializeComponent();
            LabelUnderNotes.Foreground = new SolidColorBrush(colorBlue);
            LabelUnderReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderPlans.Foreground = new SolidColorBrush(Colors.Gray);
            CreateNotesForUser();
        }

        private static void CreateNotesForUser()
        {
            User.Notes = new System.Collections.Generic.List<Note>();
            var d = new System.DateTime(2019, 11, 29);
            var n = new Note("Today.", d);
            n.Musics = new System.Collections.Generic.List<Music>() { new Music("wfe", new System.DateTime(2019, 11, 29)) };
            n.Videos = new System.Collections.Generic.List<Video>() { new Video("wfe", new System.DateTime(2019, 11, 29)) };
            n.Pictures = new System.Collections.Generic.List<Picture>() { new Picture("wfe", new System.DateTime(2019, 11, 29)) };
            n.Records = new System.Collections.Generic.List<Record>() { new Record("wfe", new System.DateTime(2019, 11, 29)) };
            User.Notes.Add(n);
            User.Notes.Add(new Note("Tommorow", new System.DateTime(2019, 11, 30)));
            User.Notes.Add(new Note("Yesterday", new System.DateTime(2019, 11, 28)));
            User.Notes.Add(new Note("I want to belive.", new System.DateTime(2019, 10, 28)));
            User.Notes.Add(new Note("I'll be back.", new System.DateTime(2019, 9, 28)));
        }
    }
}
