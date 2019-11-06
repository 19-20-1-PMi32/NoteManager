using System;

namespace NoteManager.Persistence.DataModel.Database
{
    public class Record
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime CreationTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
