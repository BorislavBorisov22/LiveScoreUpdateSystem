namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<LiveScoreUpdateSystem.Data.MsSqlDbContext>
    {
        private const string AdminRoleName = "Admin";
        private const string LiveUpdaterName = "LiveUpdater";


        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LiveScoreUpdateSystem.Data.MsSqlDbContext";
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(LiveScoreUpdateSystem.Data.MsSqlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void SeedUsers()
        {

        }
    }
}
