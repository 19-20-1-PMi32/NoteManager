using System;

namespace NoteManager.Persistence.DataModel.Database
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public int? PictureId { get; set; }
        public Picture Picture { get; set; }

        public int? VideoId { get; set; }
        public Video Video { get; set; }

    }
}
