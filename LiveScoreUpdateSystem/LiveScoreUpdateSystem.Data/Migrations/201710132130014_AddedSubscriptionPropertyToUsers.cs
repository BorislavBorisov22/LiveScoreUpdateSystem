namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubscriptionPropertyToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teams", "User_Id");
            AddForeignKey("dbo.Teams", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "User_Id" });
            DropColumn("dbo.Teams", "User_Id");
        }
    }
}
