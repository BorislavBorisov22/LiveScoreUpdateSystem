namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedStartTimeAndStartingPlayersProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixtures", "FirstHalfStart", c => c.DateTime());
            AddColumn("dbo.Fixtures", "SecondHalfStart", c => c.DateTime());
            AddColumn("dbo.Teams", "SeasonPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Fixture_Id", c => c.Guid());
            AddColumn("dbo.Players", "Fixture_Id1", c => c.Guid());
            CreateIndex("dbo.Players", "Fixture_Id");
            CreateIndex("dbo.Players", "Fixture_Id1");
            AddForeignKey("dbo.Players", "Fixture_Id", "dbo.Fixtures", "Id");
            AddForeignKey("dbo.Players", "Fixture_Id1", "dbo.Fixtures", "Id");
            DropColumn("dbo.Fixtures", "Half");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fixtures", "Half", c => c.Int(nullable: false));
            DropForeignKey("dbo.Players", "Fixture_Id1", "dbo.Fixtures");
            DropForeignKey("dbo.Players", "Fixture_Id", "dbo.Fixtures");
            DropIndex("dbo.Players", new[] { "Fixture_Id1" });
            DropIndex("dbo.Players", new[] { "Fixture_Id" });
            DropColumn("dbo.Players", "Fixture_Id1");
            DropColumn("dbo.Players", "Fixture_Id");
            DropColumn("dbo.Teams", "SeasonPoints");
            DropColumn("dbo.Fixtures", "SecondHalfStart");
            DropColumn("dbo.Fixtures", "FirstHalfStart");
        }
    }
}
