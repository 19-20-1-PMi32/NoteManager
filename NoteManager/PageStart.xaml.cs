using System;
using System.Windows;
using System.Windows.Controls;

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
