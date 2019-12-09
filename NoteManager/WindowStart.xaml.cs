using SourceWeave.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for WindowStart.xaml
    /// Referenses on sources TrayIcon https://4px.livejournal.com/126058.html
    /// </summary>
    public partial class WindowStart : SWWindow
    {
        public WindowStart()
        {
            InitializeComponent();
            f1.Navigate(new PageMenu());
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e); 
            createTrayIcon(); 
        }

        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private ContextMenu TrayMenu = null;

        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            { 
                TrayIcon = new System.Windows.Forms.NotifyIcon();
                TrayIcon.Icon = Properties.Resources.icon2; 
                TrayIcon.Text = "Here is tray icon text."; 
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

        private WindowState fCurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return fCurrentWindowState; }
            set { fCurrentWindowState = value; }
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

        private bool fCanClose = false;
        public bool CanClose
        { 
            get { return fCanClose; }
            set { fCanClose = value; }
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

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            CanClose = true;
            Close();
        }
    }
}
