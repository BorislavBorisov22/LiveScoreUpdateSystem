namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkedPlayerModelsCountryAsNonMandatory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Players", new[] { "Country_Id" });
            AlterColumn("dbo.Players", "Country_Id", c => c.Guid());
            CreateIndex("dbo.Players", "Country_Id");
            AddForeignKey("dbo.Players", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Players", new[] { "Country_Id" });
            AlterColumn("dbo.Players", "Country_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Players", "Country_Id");
            AddForeignKey("dbo.Players", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
