namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSeasonPointsPropertyFromTeamModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "SeasonPoints");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "SeasonPoints", c => c.Int(nullable: false));
        }
    }
}
