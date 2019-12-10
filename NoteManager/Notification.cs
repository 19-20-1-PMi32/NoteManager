using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;

namespace NoteManager
{
    static class Notification
    {
        private static Border Box;
        private static Label Message;
        private static int Time = 2000;

        public static void SetInstances(Border b, Label m)
        {
            Box = b;
            Message = m;
        }
        public static void ShowMessage(string message)
        {
            Message.Content = message;
            Box.Background = new SolidColorBrush(Color.FromRgb(0, 102, 255));
            Box.Visibility = System.Windows.Visibility.Visible;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,Time);
            timer.Tick += HideMessage;
            timer.Start();
        }
        private static void HideMessage(object sender, EventArgs e)
        {
            DispatcherTimer dispatcher = (DispatcherTimer)sender;
            Box.Visibility = System.Windows.Visibility.Hidden;
            dispatcher.Stop();
        }
    }
}
