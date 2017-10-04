namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedFixturesModelsToDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FlagPictureUrl = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 40),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Fixtures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ScoreHomeTeam = c.Int(nullable: false),
                        ScoreAwayTeam = c.Int(nullable: false),
                        Half = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Minute = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        League_Id = c.Guid(),
                        AwayTeam_Id = c.Guid(),
                        HomeTeam_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.League_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        League_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.League_Id);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Season = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Country_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Age = c.Int(nullable: false),
                        ShirtNumber = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        Position = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Country_Id = c.Guid(nullable: false),
                        Team_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Country_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.FixtureEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FixtureEventType = c.Int(nullable: false),
                        Minute = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        InvolvedPlayer_Id = c.Guid(),
                        Fixture_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.InvolvedPlayer_Id)
                .ForeignKey("dbo.Fixtures", t => t.Fixture_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.InvolvedPlayer_Id)
                .Index(t => t.Fixture_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fixtures", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.FixtureEvents", "Fixture_Id", "dbo.Fixtures");
            DropForeignKey("dbo.FixtureEvents", "InvolvedPlayer_Id", "dbo.Players");
            DropForeignKey("dbo.Fixtures", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Players", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Players", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Teams", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.Fixtures", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.Leagues", "Country_Id", "dbo.Countries");
            DropIndex("dbo.FixtureEvents", new[] { "Fixture_Id" });
            DropIndex("dbo.FixtureEvents", new[] { "InvolvedPlayer_Id" });
            DropIndex("dbo.FixtureEvents", new[] { "IsDeleted" });
            DropIndex("dbo.Players", new[] { "Team_Id" });
            DropIndex("dbo.Players", new[] { "Country_Id" });
            DropIndex("dbo.Players", new[] { "IsDeleted" });
            DropIndex("dbo.Leagues", new[] { "Country_Id" });
            DropIndex("dbo.Leagues", new[] { "IsDeleted" });
            DropIndex("dbo.Teams", new[] { "League_Id" });
            DropIndex("dbo.Teams", new[] { "IsDeleted" });
            DropIndex("dbo.Fixtures", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Fixtures", new[] { "AwayTeam_Id" });
            DropIndex("dbo.Fixtures", new[] { "League_Id" });
            DropIndex("dbo.Fixtures", new[] { "IsDeleted" });
            DropIndex("dbo.Countries", new[] { "IsDeleted" });
            DropTable("dbo.FixtureEvents");
            DropTable("dbo.Players");
            DropTable("dbo.Leagues");
            DropTable("dbo.Teams");
            DropTable("dbo.Fixtures");
            DropTable("dbo.Countries");
        }
    }
}
