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
using NoteManager.DBClasses;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for PhotoViewer.xaml
    /// </summary>
    public partial class PhotoViewer : Page
    {
        public PhotoViewer()
        {
            InitializeComponent();
            ImageMain.Source = new BitmapImage(new Uri(FileViewer.picture.FilePath));
        }

        private void LeavePage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
        }
    }
}
