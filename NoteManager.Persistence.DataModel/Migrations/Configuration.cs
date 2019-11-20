using System.Data.Entity.Migrations;

namespace NoteManager.Persistence.DataModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NoteManager.Persistence.DataModel.NoteManagerDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NoteManager.Persistence.DataModel.NoteManagerDatabase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
