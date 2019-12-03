using NoteManager.Showcase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageReminders.xaml
    /// </summary>
    public partial class PageReminders : Page
    {
        public PageReminders()
        {
            InitializeComponent();
            MyNotifyIcon.Visibility = Visibility.Hidden;
        }

        // повернутись в меню
        private void buttonClickMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void buttonClickSampleNotify(object sender, RoutedEventArgs e)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = "Custom Balloon";
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
        }
    }

    public class Reminder
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
