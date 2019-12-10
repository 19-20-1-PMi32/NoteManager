using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoteManager.DBClasses;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNotes.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        /// <summary>
        /// The color of the page title.
        /// </summary>
        private static Color colorBlue = Color.FromRgb(0, 122, 204);
        /// <summary>
        /// Label color before hover.
        /// </summary>
        private Brush PreviouslyColor = new SolidColorBrush(colorBlue);

        public PageMenu()
        {
            InitializeComponent();
            LabelUnderNotes.Foreground = new SolidColorBrush(colorBlue);
            LabelUnderReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderPlans.Foreground = new SolidColorBrush(Colors.Gray);

            LabelNotes.Foreground = new SolidColorBrush(colorBlue);
            LabelReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelPlans.Foreground = new SolidColorBrush(Colors.Gray);
            CreateNotesForUser();
        }

        private static void CreateNotesForUser()
        {
            User.Notes = new System.Collections.Generic.List<Note>();
            var d = new System.DateTime(2019, 8, 29);
            var n = new Note("Today.", d);
            n.Musics = new System.Collections.Generic.List<Music>() { new Music("Music", new System.DateTime(2019, 8, 29)) };
            n.Videos = new System.Collections.Generic.List<Video>() { new Video("Video", new System.DateTime(2019, 8, 29)) };
            n.Pictures = new System.Collections.Generic.List<Picture>() { new Picture("Picture", new System.DateTime(2019, 8, 29)) };
            n.Records = new System.Collections.Generic.List<Record>() { new Record("Record", new System.DateTime(2019, 8, 29)) };
            User.Notes.Add(n);
            User.Notes.Add(new Note("Tommorow", new System.DateTime(2019, 10, 20)));
            User.Notes.Add(new Note("Yesterday", new System.DateTime(2019, 10, 28)));
            User.Notes.Add(new Note("I want to belive.", new System.DateTime(2019, 11, 27)));
            User.Notes.Add(new Note("I'll be back.", new System.DateTime(2019, 11, 28)));
        }

        /// <summary>
        /// The event of a click on the inscription notes. 
        /// As a result, the note page is loaded, the notes inscription and underscore become blue, 
        /// and the rest of the menu inscriptions become gray.
        /// </summary>
        /// <param name="sender">The object of label notes.</param>
        /// <param name="e">Mouse event</param>
        private void ClickOnNotes(object sender, RoutedEventArgs e)
        {
            FrameMain.Source = new Uri("PageNote.xaml", UriKind.Relative);
            LabelUnderNotes.Foreground = new SolidColorBrush(colorBlue);
            LabelUnderReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderPlans.Foreground = new SolidColorBrush(Colors.Gray);
            LabelNotes.Foreground = new SolidColorBrush(colorBlue);
            LabelReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelPlans.Foreground = new SolidColorBrush(Colors.Gray);
            PreviouslyColor = (sender as Label).Foreground;
        }

        /// <summary>
        /// The event of a click on the inscription reminders. 
        /// As a result, the reminder page is loaded, the reminders inscription and underscore become blue, 
        /// and the rest of the menu inscriptions become gray.
        /// </summary>
        /// <param name="sender">The object of label reminders.</param>
        /// <param name="e">Mouse event</param>
        private void ClickOnReminders(object sender, RoutedEventArgs e)
        {
            FrameMain.Source = new Uri("PageReminders.xaml", UriKind.Relative);
            LabelUnderNotes.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderReminders.Foreground = new SolidColorBrush(colorBlue);
            LabelUnderPlans.Foreground = new SolidColorBrush(Colors.Gray);
            LabelNotes.Foreground = new SolidColorBrush(Colors.Gray);
            LabelReminders.Foreground = new SolidColorBrush(colorBlue);
            LabelPlans.Foreground = new SolidColorBrush(Colors.Gray);
            PreviouslyColor = (sender as Label).Foreground;
        }

        /// <summary>
        /// The event of a click on the inscription plans. 
        /// As a result, the plan page is loaded, the plans inscription and underscore become blue, 
        /// and the rest of the menu inscriptions become gray.
        /// </summary>
        /// <param name="sender">The object of label plans.</param>
        /// <param name="e">Mouseover event</param>
        private void ClickOnPlans(object sender, RoutedEventArgs e)
        {
            FrameMain.Source = new Uri("PagePlans.xaml", UriKind.Relative);
            LabelUnderNotes.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelUnderPlans.Foreground = new SolidColorBrush(colorBlue);
            LabelNotes.Foreground = new SolidColorBrush(Colors.Gray);
            LabelReminders.Foreground = new SolidColorBrush(Colors.Gray);
            LabelPlans.Foreground = new SolidColorBrush(colorBlue);
            PreviouslyColor = (sender as Label).Foreground;
        }

        /// <summary>
        /// Mouse hover event on label. As a result, the label becomes light gray.
        /// </summary>
        /// <param name="sender">Object the label on which the mouse</param>
        /// <param name="e">Mouse event</param>
        private void EnterOnLabel(object sender, RoutedEventArgs e)
        {
            PreviouslyColor = (sender as Label).Foreground;
            (sender as Label).Foreground = new SolidColorBrush(Colors.LightGray);
        }

        /// <summary>
        /// The event of leaving the mouse lettering. As a result, the color of the inscription takes the previous color.
        /// </summary>
        /// <param name="sender">The inscription object that the mouse leaves</param>
        /// <param name="e">Mouse event</param>
        private void LeaveWithLabel(object sender, RoutedEventArgs e)
        {
            (sender as Label).Foreground = PreviouslyColor;
        }
    }
}
