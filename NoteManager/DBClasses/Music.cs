using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Music : File
    {
        public DateTime CreationTime { get; set; }

        public Music() { }

        public Music(string name, DateTime date)
        {
            CreationTime = date;
        }
    }
}
