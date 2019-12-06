using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    public class Reminder
    {
        public string Text { get; set; }
        public DateTime ReminderTime { get; set; }

        public Reminder(string text, DateTime date)
        {
            Text = text;
            ReminderTime = date;
        }

        public Reminder() { }
    }
}
