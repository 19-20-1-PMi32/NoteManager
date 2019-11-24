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
    /// Interaction logic for AddFiles.xaml
    /// </summary>
    public partial class AddFiles : Page
    {
        public AddFiles()
        {
            InitializeComponent();
        }

        private void EventMouseDownOnBlackArea(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
        }

        private void ClickOnVideo(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionVideo.xaml", UriKind.Relative);
        }

        private void ClickOnPicture(object sender, RoutedEventArgs e)
        {
            FrameAddFiles.Source = new Uri("PagesForResourses/AdditionPicture.xaml", UriKind.Relative);
        }
    }
}
