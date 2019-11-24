using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class AudioEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DailyRecordId { get; set; }

        [Required]
        public byte[] AudioFile { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? DateEdited { get; set; }

        public virtual DailyRecord DailyRecord { get; set; }
    }
}
