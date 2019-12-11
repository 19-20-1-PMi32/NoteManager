using System.Windows;
using System.Windows.Controls;

namespace NoteManager
{
    /// <summary>
    /// Interaction logic for PageWelcome.xaml
    /// </summary>
    public partial class PageWelcome : Page
    {
        public PageWelcome()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The event of pressing the button starts and as a result, the transition to the login page.
        /// </summary>
        /// <param name="sender">The object of button START.</param>
        /// <param name="e">Specific event.</param>
        private void Button_ClickStart(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageSignIn());
        }
    }
}
