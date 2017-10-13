namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSbisribersPropertyToTeamModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "User_Id" });
            CreateTable(
                "dbo.UserTeams",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Team_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Team_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Team_Id);
            
            DropColumn("dbo.Teams", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.UserTeams", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserTeams", new[] { "Team_Id" });
            DropIndex("dbo.UserTeams", new[] { "User_Id" });
            DropTable("dbo.UserTeams");
            CreateIndex("dbo.Teams", "User_Id");
            AddForeignKey("dbo.Teams", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
