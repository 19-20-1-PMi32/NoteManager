using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoteManager.DBClasses;

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
            if(UserName.Text != "")
            {
                if(Password1.Text == Password2.Text)
                {
                    User.Name = UserName.Text;
                    User.Password = Password1.Text;
                    this.NavigationService.Navigate(new PageMenu());
                }
            }

        }
    }
}
