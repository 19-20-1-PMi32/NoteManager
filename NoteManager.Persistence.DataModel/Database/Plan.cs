using System;

namespace NoteManager.Persistence.DataModel.Database
{
    public class Plan
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
