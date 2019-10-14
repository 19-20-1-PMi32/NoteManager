using System;
using System.Windows;
using System.Windows.Controls;

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
