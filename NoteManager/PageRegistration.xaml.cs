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
    /// Interaction logic for PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        /// <summary>
        /// Color of label when mouse is enter(Light Blue).
        /// </summary>
        private static Color colorEnter = Color.FromRgb(0, 255, 255);

        /// <summary>
        /// Color of label when mouse is leaved(Dark Blue).
        /// </summary>
        private static Color colorLeave = Color.FromRgb(13, 32, 149);

        public PageRegistration()
        {
            InitializeComponent();
        }

        // колір прии наведенні курсору
        private void Label_MouseEnterEvent(object sender, EventArgs e)
        {
            (sender as Label).Foreground = new SolidColorBrush(colorEnter);
        }

        // колір при покинанні курсора
        private void Label_MouseLeaveEvent(object sender, EventArgs e)
        {
            (sender as Label).Foreground = new SolidColorBrush(colorLeave);
        }

        // повернутись назад на попередню сторінку
        private void Label_MouseDownEvent(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        // реєстрація
        private void buttonClickRegistrstion(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageStartMenu());
        }
    }
}
