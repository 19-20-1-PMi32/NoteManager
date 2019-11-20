using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class DailyRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DailyRecord()
        {
            AudioEntries = new HashSet<AudioEntry>();
            Notes = new HashSet<Note>();
            PictureEntries = new HashSet<PictureEntry>();
            VideoEntries = new HashSet<VideoEntry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserDataId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string Title { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AudioEntry> AudioEntries { get; set; }

        public virtual UsersData UsersData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PictureEntry> PictureEntries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VideoEntry> VideoEntries { get; set; }
    }
}
