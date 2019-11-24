using System;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void GoToAddFiles(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("AddFiles.xaml", UriKind.Relative);
        }
    }
}
