using SourceWeave.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

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
    }
}
