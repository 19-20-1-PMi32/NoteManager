namespace NoteManager.Persistence.DataModel
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
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

            // User configuration
            modelBuilder
                .Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<User>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder
                .Entity<User>()
                .Property(e => e.FirstName)
                .IsRequired();
            modelBuilder
                .Entity<User>()
                .Property(e => e.FirstName)
                .HasMaxLength(32);

            modelBuilder
                .Entity<User>()
                .Property(e => e.LastName)
                .IsRequired();
            modelBuilder
                .Entity<User>()
                .Property(e => e.LastName)
                .HasMaxLength(32);
            
            modelBuilder
                .Entity<User>()
                .Property(e => e.Email)
                .IsRequired();
            modelBuilder
                .Entity<User>()
                .Property(e => e.Email)
                .HasMaxLength(64);

            // Note configuration
            modelBuilder
                .Entity<Note>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Note>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder
                .Entity<Note>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Plan configuration
            modelBuilder
                .Entity<Plan>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Plan>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Plan>()
                .Property(e => e.ExpirationTime)
                .IsRequired();

            modelBuilder
                .Entity<Plan>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Reminder configuration
            modelBuilder
                .Entity<Reminder>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Reminder>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Reminder>()
                .Property(e => e.ReminderTime)
                .IsRequired();

            modelBuilder
                .Entity<Reminder>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Video configuration
            modelBuilder
                .Entity<Video>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Video>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Video>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Picture configuration
            modelBuilder
                .Entity<Picture>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Picture>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Picture>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Music configuration
            modelBuilder
                .Entity<Music>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Music>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Music>()
                .Property(e => e.CreationTime)
                .IsRequired();

            // Record configuration
            modelBuilder
                .Entity<Record>()
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<Record>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Record>()
                .Property(e => e.CreationTime)
                .IsRequired();
        }
    }
}