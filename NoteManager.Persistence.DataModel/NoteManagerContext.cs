namespace NoteManager.Persistence.DataModel
{
    using System.Data.Entity;
    using Database;

    public class NoteManagerContext : DbContext
    {
        public NoteManagerContext()
            : base("name=NoteManager")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}