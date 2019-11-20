using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IdentityId { get; set; }

        public int? DataId { get; set; }

        public virtual UserData UserData { get; set; }

        public virtual UserIdentity UserIdentity { get; set; }
    }
}
