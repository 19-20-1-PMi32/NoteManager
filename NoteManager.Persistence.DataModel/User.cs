using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Persistence.DataModel
{
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int IdentityId { get; set; }

        public int? DataId { get; set; }

        public virtual UsersData UsersData { get; set; }

        public virtual UsersIdentity UsersIdentity { get; set; }
    }
}
