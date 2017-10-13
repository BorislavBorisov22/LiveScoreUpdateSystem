namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedScoreAtTheTimeOfEventPropertryToFixtureEventModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Fixture_Id", "dbo.Fixtures");
            DropForeignKey("dbo.Players", "Fixture_Id1", "dbo.Fixtures");
            DropIndex("dbo.Players", new[] { "Fixture_Id" });
            DropIndex("dbo.Players", new[] { "Fixture_Id1" });
            AddColumn("dbo.FixtureEvents", "EventScore", c => c.String());
            DropColumn("dbo.Players", "Fixture_Id");
            DropColumn("dbo.Players", "Fixture_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Fixture_Id1", c => c.Guid());
            AddColumn("dbo.Players", "Fixture_Id", c => c.Guid());
            DropColumn("dbo.FixtureEvents", "EventScore");
            CreateIndex("dbo.Players", "Fixture_Id1");
            CreateIndex("dbo.Players", "Fixture_Id");
            AddForeignKey("dbo.Players", "Fixture_Id1", "dbo.Fixtures", "Id");
            AddForeignKey("dbo.Players", "Fixture_Id", "dbo.Fixtures", "Id");
        }
    }
}
