using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteManager
{
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
        }

        private void EventReturn(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void SignUpAndGoToNext(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageNotes());
        }
    }
}
