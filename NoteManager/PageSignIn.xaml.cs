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
    /// Interaction logic for PageSignIn.xaml
    /// </summary>
    public partial class PageSignIn : Page
    {
        /// <summary>
        /// Color of label when mouse is enter(Light Blue).
        /// </summary>
        private static Color colorEnter = Color.FromRgb(0, 255, 255);

        /// <summary>
        /// Color of label when mouse is leaved(Dark Blue).
        /// </summary>
        private static Color colorLeave = Color.FromRgb(0, 116, 255);

        public PageSignIn()
        {
            InitializeComponent();
        }

        // колір label при наведенні курсору
        private void Label_MouseEnterEvent(object sender, EventArgs e)
        {
            (sender as Label).Foreground = new SolidColorBrush(colorEnter);
        }

        // колір label при покиданні курсору
        private void Label_MouseLeaveEvent(object sender, EventArgs e)
        {

            (sender as Label).Foreground = new SolidColorBrush(colorLeave);
        }

        // подія натискання на label Cancel
        private void Label_MouseDownEvent(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        // перехід на в Меню обліковогу запису
        private void buttonClickSignIn(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageStartMenu());
        }
    }
}
