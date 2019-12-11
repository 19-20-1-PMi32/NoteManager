using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    static class User
    {
        public static List<Note> Notes { get; set; }
        public static ObservableCollection<Reminder> Reminders { get; set; }
        public static List<Plan> Plans { get; set; }
        public static void Init()
        {
            Notes = new List<Note>();
            Reminders = new ObservableCollection<Reminder>();
            Plans = new List<Plan>();
        }
    }
}
