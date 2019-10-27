using System.Windows;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for WindowStart.xaml
    /// </summary>
    public partial class WindowStart : Window
    {
        public WindowStart()
        {
            InitializeComponent();
            f1.Navigate(new PageStart());
        }
    }
}
