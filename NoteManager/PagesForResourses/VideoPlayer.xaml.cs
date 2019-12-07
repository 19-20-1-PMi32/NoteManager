using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private TimeSpan currentPosition;
        public VideoPlayer()
        {
            InitializeComponent();
        }
        public VideoPlayer(File file)
        {
            if(Player != null)
                Player.Source = new Uri(file.FilePath);
        }
        private void ToggleWindow()
        {
            switch (this.WindowState)
            {
                case (WindowState.Maximized):
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    break;
                default:
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    break;
            }
        }
        //stop
        private void Stop(object sender, MouseButtonEventArgs e)
        {
            Player.Stop();
        }
        //play
        private void Play(object sender, MouseButtonEventArgs e)
        {
            if (currentPosition != null)
                Player.Position = currentPosition;
            Player.Play();
        }
        //pause
        private void Pause(object sender, MouseButtonEventArgs e)
        {
            currentPosition = Player.Position;
            Player.Pause();
        }
    }
}
