using System.Data.Entity;

namespace NoteManager.Persistence.DataModel
{
    public partial class NoteManagerDatabase : DbContext
    {
        public NoteManagerDatabase(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NoteManagerDatabase, Migrations.Configuration>());
        }

        public virtual DbSet<AudioEntry> AudioEntries { get; set; }
        public virtual DbSet<DailyRecord> DailyRecords { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<PictureEntry> PictureEntries { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserData> UserDatas { get; set; }
        public virtual DbSet<UserIdentity> UserIdentities { get; set; }
        public virtual DbSet<VideoEntry> VideoEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudioEntry>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<DailyRecord>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<DailyRecord>()
                .HasMany(e => e.AudioEntries)
                .WithRequired(e => e.DailyRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyRecord>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.DailyRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyRecord>()
                .HasMany(e => e.PictureEntries)
                .WithRequired(e => e.DailyRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DailyRecord>()
                .HasMany(e => e.VideoEntries)
                .WithRequired(e => e.DailyRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<PictureEntry>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Reminder>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Reminder>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<UserData>()
                .HasMany(e => e.DailyRecords)
                .WithRequired(e => e.UsersData)
                .HasForeignKey(e => e.UserDataId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserData>()
                .HasMany(e => e.Reminders)
                .WithRequired(e => e.UsersData)
                .HasForeignKey(e => e.UserData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserData>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UserData)
                .HasForeignKey(e => e.DataId);

            modelBuilder.Entity<UserIdentity>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<UserIdentity>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserIdentity>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserIdentity)
                .HasForeignKey(e => e.IdentityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VideoEntry>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
