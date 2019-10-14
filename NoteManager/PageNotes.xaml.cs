using System.Windows;
using System.Windows.Controls;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageNotes.xaml
    /// </summary>
    public partial class PageNotes : Page
    {
        public PageNotes()
        {
            InitializeComponent();
        }

        // відкрити вікно відео
        private void buttonClickVideo(object sender, RoutedEventArgs e)
        {
            WindowVideos WV = new WindowVideos();
            WV.Show();
        }

        // перейти в меню
        private void buttonClickMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
