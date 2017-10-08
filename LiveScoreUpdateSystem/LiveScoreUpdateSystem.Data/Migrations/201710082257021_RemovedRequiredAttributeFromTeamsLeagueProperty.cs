namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedRequiredAttributeFromTeamsLeagueProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "League_Id", "dbo.Leagues");
            DropIndex("dbo.Teams", new[] { "League_Id" });
            AlterColumn("dbo.Teams", "League_Id", c => c.Guid());
            CreateIndex("dbo.Teams", "League_Id");
            AddForeignKey("dbo.Teams", "League_Id", "dbo.Leagues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "League_Id", "dbo.Leagues");
            DropIndex("dbo.Teams", new[] { "League_Id" });
            AlterColumn("dbo.Teams", "League_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Teams", "League_Id");
            AddForeignKey("dbo.Teams", "League_Id", "dbo.Leagues", "Id", cascadeDelete: true);
        }
    }
}
