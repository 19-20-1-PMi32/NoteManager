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
            TabControlReminder.SizeChanged += OnTabControlSizeChanged;
            
        }

        private void buttonClickSampleNotify(object sender, RoutedEventArgs e)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = "Custom Balloon";
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
        }

        protected void OnTabControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActualCoulumnText.Width = e.NewSize.Width - 252.5;
            NoActualCoulumnText.Width = e.NewSize.Width - 252.5;
        }
    }

    public class Reminder
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
