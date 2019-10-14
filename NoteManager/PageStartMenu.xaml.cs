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
    /// Interaction logic for PageStartMenu.xaml
    /// </summary>
    public partial class PageStartMenu : Page
    {
        /// <summary>
        /// Opasity of rectangle when mouse is enter.
        /// </summary>
        private static float opasityEnter = 1.0f;

        /// <summary>
        /// Opasity of rectangle when mouse is leaved.
        /// </summary>
        private static float opasityLeave = 0.7f;

        /// <summary>
        /// Color of label when mouse is enter(Light Blue).
        /// </summary>
        private static Color colorEnter = Color.FromRgb(9, 0, 179);

        /// <summary>
        /// Color of label when mouse is leaved(Dark Blue).
        /// </summary>
        private static Color colorLeave = Color.FromRgb(5, 0, 102);

        public PageStartMenu()
        {
            InitializeComponent();
        }

        // Стан чотирикутника і його напису коли наведений курсор.
        private void Rectangle_MouseEnterEvent(object sender, EventArgs e)
        {
            if (sender is Rectangle)
            {
                (sender as Rectangle).Opacity = opasityEnter;
                if ((sender as Rectangle).Name == "RectangleNotes")
                    labelNotes.Foreground = new SolidColorBrush(colorEnter);

                else if ((sender as Rectangle).Name == "RectangleReminders")
                    labelReminders.Foreground = new SolidColorBrush(colorEnter);

                else if ((sender as Rectangle).Name == "RectanglePlans")
                    labelPlans.Foreground = new SolidColorBrush(colorEnter);

                else if ((sender as Rectangle).Name == "RectangleExit")
                    labelExit.Foreground = new SolidColorBrush(colorEnter);
            }
            if (sender is Label)
            {
                (sender as Label).Foreground = new SolidColorBrush(colorEnter);
                if ((sender as Label).Name == "labelNotes")
                    RectangleNotes.Opacity = opasityEnter;

                else if ((sender as Label).Name == "labelReminders")
                    RectangleReminders.Opacity = opasityEnter;

                else if ((sender as Label).Name == "labelPlans")
                    RectanglePlans.Opacity = opasityEnter;

                else if ((sender as Label).Name == "labelExit")
                    RectangleExit.Opacity = opasityEnter;
            }

        }

        // Стан чотирикутника і його напису коли курсор за межами.
        private void Rectangle_MouseLeaveEvent(object sender, EventArgs e)
        {
            if (sender is Rectangle)
            {
                (sender as Rectangle).Opacity = opasityLeave;
                if ((sender as Rectangle).Name == "RectangleNotes")
                    labelNotes.Foreground = new SolidColorBrush(colorLeave);

                else if ((sender as Rectangle).Name == "RectangleReminders")
                    labelReminders.Foreground = new SolidColorBrush(colorLeave);

                else if ((sender as Rectangle).Name == "RectanglePlans")
                    labelPlans.Foreground = new SolidColorBrush(colorLeave);

                else if ((sender as Rectangle).Name == "RectangleExit")
                    labelExit.Foreground = new SolidColorBrush(colorLeave);
            }
            if (sender is Label)
            {
                (sender as Label).Foreground = new SolidColorBrush(colorLeave);
                if ((sender as Label).Name == "labelNotes")
                    RectangleNotes.Opacity = opasityLeave;

                else if ((sender as Label).Name == "labelReminders")
                    RectangleReminders.Opacity = opasityLeave;

                else if ((sender as Label).Name == "labelPlans")
                    RectanglePlans.Opacity = opasityLeave;

                else if ((sender as Label).Name == "labelExit")
                    RectangleExit.Opacity = opasityLeave;
            }

        }

        // Подія натискання на один з чотирикутників чи його напису
        private void Rectangle_MouseDownEvent(object sender, EventArgs e)
        {
            if(sender is Rectangle)
            {
                switch ((sender as Rectangle).Name)
                {
                    case "RectangleNotes":
                        this.NavigationService.Navigate(new PageNotes());
                        break;

                    case "RectangleReminders":
                        this.NavigationService.Navigate(new PageReminders());
                        break;

                    case "RectanglePlans":
                        MessageBox.Show("поки нема");
                        break;

                    case "RectangleExit":
                        this.NavigationService.GoBack();
                        break;

                }

            }
            else if(sender is Label)
            {
                switch ((sender as Label).Name)
                {
                    case "labelNotes":
                        this.NavigationService.Navigate(new PageNotes());
                        break;

                    case "labelReminders":
                        this.NavigationService.Navigate(new PageReminders());
                        break;

                    case "labelPlans":
                        MessageBox.Show("поки нема");
                        break;

                    case "labelExit":
                        this.NavigationService.GoBack();
                        break;

                }
            }
        }
    }
}
