using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Video : File
    {
        public DateTime CreationTime { get; set; }

        public Video() { }

        public Video(string name, DateTime date)
        {
            CreationTime = date;
        }
    }
}
