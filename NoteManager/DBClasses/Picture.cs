using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Picture : File
    {
        public DateTime CreationTime { get; set; }

        public Picture() { }

        public Picture(string name, DateTime date)
        {
            CreationTime = date;
        }
    }
}
