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
    /// Interaction logic for PageStart.xaml
    /// </summary>
    public partial class PageStart : Page
    {
        public PageStart()
        {
            InitializeComponent();
        }

        // Перейти на сторінку входу
        private void buttonSignInClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageSignIn());
        }

        // Перейти на сторінку реєстрації
        private void buttonClickRegistration(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageRegistration());
        }

        // Вийти з програми
        private void buttonClickExit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
