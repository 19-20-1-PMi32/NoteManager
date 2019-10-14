using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
