using NoteManager.Showcase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using NoteManager.DBClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            CreateRemindersForUser();
            buttonClickSampleNotify(null, null);
        }

        private void buttonClickSampleNotify(object sender, RoutedEventArgs e)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = "Kill him. Now!";
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
        }

        protected void OnTabControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActualCoulumnText.Width = e.NewSize.Width - 192.5;
            NoActualCoulumnText.Width = e.NewSize.Width - 192.5;
        }

        private void ClickOnAddReminder(object sender, RoutedEventArgs e)
        {
            DateTime time = DatePickerDate.DisplayDate;
            string[] timeInDay = TextBoxHours.Text.Split(':');
            DateTime Hours = new DateTime(time.Year, time.Month, time.Day, int.Parse(timeInDay[0]), int.Parse(timeInDay[1]), int.Parse(timeInDay[2]));
            string text = TextBoxAddReminder.Text;
            User.Reminders.Add(new Reminder(text, Hours));
            ShowReminders();
        }

        private void ShowReminders()
        {
            ActualReminder.ItemsSource = User.Reminders;
        }

        private void CreateRemindersForUser()
        {
            User.Reminders = new ObservableCollection<Reminder>();
            User.Reminders.Add(new Reminder("Kill", DateTime.Now));
            User.Reminders.Add(new Reminder("Love", DateTime.Now));
            User.Reminders.Add(new Reminder("Run", DateTime.Now));
            ActualReminder.ItemsSource = User.Reminders;
        }
    }
}
