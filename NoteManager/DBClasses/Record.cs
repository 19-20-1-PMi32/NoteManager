using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Record : File
    {
        public DateTime CreationTime { get; set; }

        public Record() { }

        public Record(string name, DateTime date)
        {
            CreationTime = date;
        }
    }
}
