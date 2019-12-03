using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Record
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }

        public Record() { }

        public Record(string name, DateTime date)
        {
            Name = name;
            CreationTime = date;
        }
    }
}
