using NoteManager.Showcase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using NoteManager.DBClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageReminders.xaml
    /// </summary>
    public partial class PageReminders : Page
    {
        private DispatcherTimer myTimer = null;
        private Reminder FirstInQueue = new Reminder();

        public PageReminders()
        {
            InitializeComponent();
            MyNotifyIcon.Visibility = Visibility.Hidden;
            TabControlReminder.SizeChanged += OnTabControlSizeChanged;
            CreateRemindersForUser();
        }

        protected void OnTabControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ActualCoulumnText.Width = e.NewSize.Width - 192.5;
            NoActualCoulumnText.Width = e.NewSize.Width - 192.5;
        }
        private bool IsValid(Reminder reminder)
        {
            if (reminder.Text == String.Empty) {
                Notification.ShowMessage("Reminder text is empty");
                return false;
            }
            return true;
        }
        private void ClickOnAddReminder(object sender, RoutedEventArgs e)
        {
            DateTime time = DatePickerDate.DisplayDate;
            string[] timeInDay = TextBoxHours.Text.Split(':');
            DateTime Hours;
            if (timeInDay.Length != 3)
            {
                Notification.ShowMessage("Wrong time input");
                return;
            }
            try
            {
                Hours = new DateTime(time.Year, time.Month, time.Day, int.Parse(timeInDay[0]), int.Parse(timeInDay[1]), int.Parse(timeInDay[2]));
            }
            catch(Exception ex)
            {
                Notification.ShowMessage("Wrong time or date input");
                return;
            }
            string text = TextBoxAddReminder.Text;
            var r = new Reminder(text, Hours);
            if (!IsValid(r))
            {
                return;
            }
            User.Reminders.Add(new Reminder(text, Hours));
            ShowReminders();
            if(myTimer == null)
            {
                DoInquiry();
            }
            SuccessfulAddind();
            AnnulTabItemAdd();
        }

        private void ShowReminders()
        {
            ActualReminder.ItemsSource = User.Reminders;
        }

        private void CreateRemindersForUser()
        {
            ActualReminder.ItemsSource = User.Reminders;
        }

        public void DoInquiry()
        {
            bool isReminder = false;
            for(int item = 0; item < User.Reminders.Count; ++item)
            {
                if (User.Reminders[item].ReminderTime > DateTime.Now && !User.Reminders[item].IsQueue && !User.Reminders[item].IsShown)
                {
                    myTimer = new DispatcherTimer();
                    myTimer.Interval = User.Reminders[item].ReminderTime - DateTime.Now;
                    User.Reminders[item].IsQueue = true;
                    FirstInQueue = User.Reminders[item];
                    isReminder = true;
                    break;
                }

            }
            if (isReminder)
            {
                myTimer.Tick += new EventHandler(TimerEventProcessor);
                myTimer.Start();
            }
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = FirstInQueue.Text;
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
            myTimer.Tick -= new EventHandler(TimerEventProcessor);
            myTimer = null;
            for (int item = 0; item < User.Reminders.Count; ++item)
            {
                if (User.Reminders[item] == FirstInQueue)
                {
                    User.Reminders[item].IsShown = true;
                    User.Reminders[item].IsQueue = false;
                    DoInquiry();
                    break;
                }
            }
        }

        private void AnnulTabItemAdd()
        {
            TextBoxHours.Text = "";
            DatePickerDate.Text = "";
            TextBoxAddReminder.Text = ""; 
        }

        private async void SuccessfulAddind()
        {
            AddMessage.Opacity = 1;
            AddMessage.Content = "Adding is successful!";

            for (double i = 1; i >= 0; i = i - 0.05)
            {
                await Task.Delay(100);
                AddMessage.Opacity = i;
            }
            AddMessage.Opacity = 0;
        }
    }
}
