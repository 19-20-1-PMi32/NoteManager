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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        // відкрити вікно добавлення нагадування
        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            ReminderAdd ra = new ReminderAdd();
            ra.Show();
        }

        // повернутись в меню
        private void buttonClickMenu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }

    public class Reminder
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
