using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class VideoEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DailyRecordId { get; set; }

        [Required]
        public byte[] VideoFile { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? DateAdded { get; set; }

        public virtual DailyRecord DailyRecord { get; set; }
    }
}
