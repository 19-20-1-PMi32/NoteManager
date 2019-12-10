using System;
using System.Windows;
using System.Windows.Controls;
using NoteManager.DBClasses;
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

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            TemporaryNote.IsUsed = true;
            User.Notes.Add(new Note(TemporaryNote.Text, TemporaryNote.CreationTime)
            {
                Videos = TemporaryNote.Videos,
                Pictures = TemporaryNote.Pictures,
                Musics = TemporaryNote.Musics,
                Records = TemporaryNote.Records
            })
            ;
            TemporaryNote.AnnulOfNote();
            // Annul TemporaryNote
            this.NavigationService.Source = null;
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
