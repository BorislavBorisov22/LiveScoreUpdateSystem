namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPictureUrlPropertyToFixtureEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FixtureEvents", "PictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FixtureEvents", "PictureUrl");
        }
    }
}
