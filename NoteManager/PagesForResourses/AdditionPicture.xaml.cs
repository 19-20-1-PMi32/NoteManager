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

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionPicture.xaml
    /// </summary>
    public partial class AdditionPicture : Page
    {
        public AdditionPicture()
        {
            InitializeComponent();
        }

        private void DoubleClickOnNmeOfPicture1(object sender, RoutedEventArgs e)
        {
            MyImage.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/BackroundBlackWorld.jpg"));
        }

        private void DoubleClickOnNmeOfPicture2(object sender, RoutedEventArgs e)
        {
            MyImage.Source = new BitmapImage(new Uri("pack://application:,,,/NoteManager;component/Resources/Pictures/Start1.jpg"));
        }
    }
}
