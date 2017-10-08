namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredAttributeFromLeaguesCountry : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leagues", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Leagues", new[] { "Country_Id" });
            AlterColumn("dbo.Leagues", "Country_Id", c => c.Guid());
            CreateIndex("dbo.Leagues", "Country_Id");
            AddForeignKey("dbo.Leagues", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leagues", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Leagues", new[] { "Country_Id" });
            AlterColumn("dbo.Leagues", "Country_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Leagues", "Country_Id");
            AddForeignKey("dbo.Leagues", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
