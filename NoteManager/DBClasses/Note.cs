using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManager.DBClasses
{
    class Note
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Video> Videos { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<Music> Musics { get; set; }
        public List<Record> Records { get; set; }
        public Note() { }
        public Note(string text, DateTime date)
        {
            Text = text;
            CreationTime = date;
            Videos = new List<Video>();
            Pictures = new List<Picture>();
            Musics = new List<Music>();
            Records = new List<Record>();
        }
    }
}
