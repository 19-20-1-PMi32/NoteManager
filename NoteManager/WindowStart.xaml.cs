using SourceWeave.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using NoteManager.DBClasses;
using NoteManager.Showcase;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for WindowStart.xaml
    /// </summary>
    public partial class WindowStart : SWWindow
    {
        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private ContextMenu TrayMenu = null;
        public WindowState CurrentWindowState { get; set; } = WindowState.Normal;
        public bool CanClose { get; set; } = false;
        private DispatcherTimer myTimer = new DispatcherTimer();
        private Reminder FirstInQueue = new Reminder();

        public WindowStart()
        {
            InitializeComponent();
            f1.Navigate(new PageWelcome());
            //CreateRemindersForUser();
            DoInquiry();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e); 
            createTrayIcon(); 
        }

        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            { 
                TrayIcon = new System.Windows.Forms.NotifyIcon();
                TrayIcon.Icon = Properties.Resources.icon2; 
                TrayIcon.Text = "NoteManager"; 
                TrayMenu = Resources["TrayMenu"] as ContextMenu; 
                TrayIcon.Click += delegate (object sender, EventArgs e) {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        ShowHideMainWindow(sender, null);
                    }
                    else
                    {
                        TrayMenu.IsOpen = true;
                        Activate(); 
                    }
                };
                result = true;
            }
            else
            { 
                result = true;
            }
            TrayIcon.Visible = true;
            return result;
        }

        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false; 
            if (IsVisible)
            {
                Hide();
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
            }
            else
            {
                Show();
                (TrayMenu.Items[0] as MenuItem).Header = "Hide";
                WindowState = CurrentWindowState;
                Activate(); 
            }
        }


        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (this.WindowState == WindowState.Minimized)
            {
                Hide();
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
            }
            else
            {
                CurrentWindowState = WindowState;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e); 
            if (!CanClose)
            {   
                e.Cancel = true;
                CurrentWindowState = this.WindowState;
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
                Hide();
            }
            else
            {
                TrayIcon.Visible = false;
            }
        }

        /// <summary>
        /// Event of pressing the exit button. The main window closes and the program exits.
        /// </summary>
        /// <param name="sender">The object of MenuItem exit.</param>
        /// <param name="e">Specific event.</param>
        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            CanClose = true;
            Close();
        }

        public void DoInquiry()
        {
            if (User.Reminders != null && User.Reminders.Count != 0)
            {
                bool isReminder = false;
                for (int item = 0; item < User.Reminders.Count; ++item)
                {
                    if (User.Reminders[item].ReminderTime > DateTime.Now && !User.Reminders[item].IsQueue && !User.Reminders[item].IsShown)
                    {
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
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            FancyBalloon balloon = new FancyBalloon();
            balloon.BalloonText = FirstInQueue.Text;
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
            myTimer.Tick -= new EventHandler(TimerEventProcessor);
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

        private void CreateRemindersForUser()
        {
            User.Reminders = new ObservableCollection<Reminder>();
            int minute = 7;
            User.Reminders.Add(new Reminder("Kill", new DateTime(2019, 12, 10, 18, minute, 0)));
            User.Reminders.Add(new Reminder("Love", new DateTime(2019, 12, 10, 18, minute, 10)));
            User.Reminders.Add(new Reminder("Run", new DateTime(2019, 12, 10, 18, minute, 20)));
            
        }
    }
}
