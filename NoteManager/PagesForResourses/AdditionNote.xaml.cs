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
using NoteManager.DBClasses;

namespace NoteManager.PagesForResourses
{
    /// <summary>
    /// Interaction logic for AdditionNote.xaml
    /// </summary>
    public partial class AdditionNote : Page
    {
        public AdditionNote()
        {
            InitializeComponent();
            TextBoxMain.Text = TemporaryNote.Text;
        }

        private void ClickOnConfirm(object sender, RoutedEventArgs e)
        {
            if(TextBoxMain.Text != "")
            {
                TemporaryNote.Text = TextBoxMain.Text;
                TemporaryNote.CreationTime = DateTime.Now;
            }
        }
    }
}
