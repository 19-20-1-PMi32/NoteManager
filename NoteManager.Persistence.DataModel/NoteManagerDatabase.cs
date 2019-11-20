using System.Data.Entity;

namespace NoteManager.Persistence.DataModel
{
    public partial class NoteManagerDatabase : DbContext
    {
        public NoteManagerDatabase()
            : base("name=NoteManagerDatabase")
        {
        }

        public virtual DbSet<AudioEntry> AudioEntries { get; set; }
        public virtual DbSet<DailyRecord> DailyRecords { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<PictureEntry> PictureEntries { get; set; }
        public virtual DbSet<Remainder> Remainders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersData> UsersDatas { get; set; }
        public virtual DbSet<UsersIdentity> UsersIdentities { get; set; }
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

            modelBuilder.Entity<Remainder>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Remainder>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<UsersData>()
                .HasMany(e => e.DailyRecords)
                .WithRequired(e => e.UsersData)
                .HasForeignKey(e => e.UserDataId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersData>()
                .HasMany(e => e.Remainders)
                .WithRequired(e => e.UsersData)
                .HasForeignKey(e => e.UserData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersData>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UsersData)
                .HasForeignKey(e => e.DataId);

            modelBuilder.Entity<UsersIdentity>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<UsersIdentity>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UsersIdentity>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UsersIdentity)
                .HasForeignKey(e => e.IdentityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VideoEntry>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
