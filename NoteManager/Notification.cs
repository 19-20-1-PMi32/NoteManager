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
    enum MessageType
    {
        Error,
        Success,
        Info
    }
    static class Notification
    {
        public static Border Box;
        public static Label Message;
        public static int Time = 1500;

        public static void SetInstances(Border b, Label m)
        {
            Box = b;
            Message = m;
        }
        public static void ShowMessage(MessageType type, string message)
        {
            Message.Content = message;
            Box.Background = new SolidColorBrush(GetColor(type));
            Box.Visibility = System.Windows.Visibility.Visible;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,Time);
            timer.Tick += HideMessage;
            timer.Start();
        }
        public static void HideMessage(object sender, EventArgs e)
        {
            DispatcherTimer dispatcher = (DispatcherTimer)sender;
            Box.Visibility = System.Windows.Visibility.Hidden;
            Debug.WriteLine("Happened");
            dispatcher.Stop();

        }
        public static Color GetColor(MessageType type)
        {
            if (type == MessageType.Info)
                return Color.FromRgb(0, 102, 255);
            else if (type == MessageType.Error)
                return Color.FromRgb(255, 102, 0);
            else
                return Color.FromRgb(10, 10, 10);
        }

    }
}
