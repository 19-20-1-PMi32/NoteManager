using System;
using System.Windows;
using System.Windows.Controls;

namespace NoteManager
{
    public partial class PageSignIn : Page
    {
        public PageSignIn()
        {
            InitializeComponent();
        }

        private void EventReturn(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void GoToRegistratin(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new PageRegistration());
        }

        private void GoToNext(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageNotes());
        }
    }
}