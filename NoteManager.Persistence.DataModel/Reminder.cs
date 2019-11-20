using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class Reminder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserDataId { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime TriggerDate { get; set; }

        public virtual UserData UserData { get; set; }
    }
}
