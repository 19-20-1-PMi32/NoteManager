using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        }

        // відкрити вікно відео
        private void buttonClickVideo(object sender, RoutedEventArgs e)
        {
            WindowVideos WV = new WindowVideos();
            WV.Show();
        }

    }
}
