namespace LiveScoreUpdateSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAllDateTimeTypesToDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Countries", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Countries", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fixtures", "FirstHalfStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fixtures", "SecondHalfStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fixtures", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fixtures", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fixtures", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Teams", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Teams", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Teams", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leagues", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leagues", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leagues", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Players", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Players", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Players", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.FixtureEvents", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.FixtureEvents", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.FixtureEvents", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.FixtureEvents", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.FixtureEvents", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.FixtureEvents", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Players", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Players", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Players", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Leagues", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Leagues", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Leagues", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Teams", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Teams", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Teams", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Fixtures", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Fixtures", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Fixtures", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Fixtures", "SecondHalfStart", c => c.DateTime());
            AlterColumn("dbo.Fixtures", "FirstHalfStart", c => c.DateTime());
            AlterColumn("dbo.Countries", "DeletedOn", c => c.DateTime());
            AlterColumn("dbo.Countries", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Countries", "CreatedOn", c => c.DateTime());
        }
    }
}
