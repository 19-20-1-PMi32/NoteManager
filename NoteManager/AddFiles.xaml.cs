using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using NoteManager.DBClasses;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for AddFiles.xaml
    /// </summary>
    public partial class AddFiles : Page
    {
        public AddFiles()
        {
            InitializeComponent();
            TemporaryNote.Init();
        }

        private void EventMouseDownOnBlackArea(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
        }

        private bool IsValid()
        {
            if (TemporaryNote.Text == String.Empty)
            {
                return false;
            }
            return true;
        }
        private void ClickSave(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                TemporaryNote.IsUsed = true;

                var note = new Note(TemporaryNote.Text, TemporaryNote.CreationTime)
                {
                    Videos = TemporaryNote.Videos,
                    Pictures = TemporaryNote.Pictures,
                    Musics = TemporaryNote.Musics,
                    Records = TemporaryNote.Records
                };
                User.Notes.Add(note);

                TemporaryNote.AnnulOfNote();
                // Annul TemporaryNote
                this.NavigationService.Source = null;
            }
            else
            {
                Notification.ShowMessage("Note is empty, please add more information");
            }
        }

        private void ClickOnNote(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionNote.xaml", UriKind.Relative);
        }

        private void ClickOnVideo(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionVideo.xaml", UriKind.Relative);
        }

        private void ClickOnPicture(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionPicture.xaml", UriKind.Relative);
        }

        private void ClickOnMusic(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionMusic.xaml", UriKind.Relative);
        }

        private void ClickOnRecord(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionRecord.xaml", UriKind.Relative);
        }
    }
}
