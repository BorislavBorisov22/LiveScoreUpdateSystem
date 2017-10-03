namespace LiveScoreUpdateSystem.Data.Migrations
{
    using LiveScoreUpdateSystem.Common;
    using LiveScoreUpdateSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LiveScoreUpdateSystem.Data.MsSqlDbContext>
    {
        private const string AdministratorUserName = "BorislavAdmin@gmail.com";
        private const string AdministratorPassword = "123456";
        private const string LiveUpdaterUserName = "BorislavUpdater";
        private const string LiveUpdaterPassword = "123456";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LiveScoreUpdateSystem.Data.MsSqlDbContext";
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(LiveScoreUpdateSystem.Data.MsSqlDbContext context)
        {
            this.SeedUsers(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                CreateRole(GlobalConstants.AdminRole, context);
                CreateRole(GlobalConstants.LiveUpdaterRole, context);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, GlobalConstants.AdminRole);
                userManager.AddToRole(user.Id, GlobalConstants.LiveUpdaterRole);
            }
        }

        private void CreateRole(string roleName, MsSqlDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = roleName };

            roleManager.Create(role);
        }
    }
}
