using System.Data.Entity.Migrations;

namespace NoteManager.Persistence.DataModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NoteManagerDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "NoteManager";
        }

        protected override void Seed(NoteManagerDatabase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
