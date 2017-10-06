namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLogoUrlToTeamModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "LogoUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "LogoUrl");
        }
    }
}
