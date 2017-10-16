namespace LiveScoreUpdateSystem.Data.Migrations
{
    using LiveScoreUpdateSystem.Common;
    using LiveScoreUpdateSystem.Data.Models;
    using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
    using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
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
            this.SeedSampleData(context);
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

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (context.Teams.Any())
            {
                return;
            }

            var italy = new Country() { Name = "Italy", FlagPictureUrl = "https://m.netinfo.bg/gong/images/livescore/countries/Italy_small.png" };
            var england = new Country() { Name = "Englang", FlagPictureUrl = "https://m.netinfo.bg/gong/images/livescore/countries/england_small.png" };
            var spain = new Country() { Name = "Englang", FlagPictureUrl = "https://m.netinfo.bg/gong/images/livescore/countries/Spain_small.png" };

            context.Countries.Add(italy);
            context.Countries.Add(england);
            context.Countries.Add(spain);

            var premierLeague = new League() { Name = "Premier League", Season = 2017, Country = england };
            var seriaA = new League() { Name = "Italian Serie A", Season = 2017, Country = italy };

            context.Leagues.Add(premierLeague);
            context.Leagues.Add(seriaA);

            var everton = new Team() { Name = "Everton", LogoUrl = "https://m.netinfo.bg/gong/images/livescore/teams/674.png", League = premierLeague };
            var newcastle = new Team() { Name = "Newcastle", LogoUrl = "https://m.netinfo.bg/gong/images/livescore/teams/664.png", League = premierLeague };

            var mirallas = new Player()
            {
                FirstName = "Kevin",
                LastName = "Mirallas",
                ShirtNumber = 22,
                Age = 30,
                Country = england,
                Position = PlayerPosition.CAM,
                PictureUrl = "https://cdn.images.express.co.uk/img/dynamic/67/590x/kevin-mirallas-545402.jpg",
                Team = everton,
            };

            var leightBaines = new Player()
            {
                FirstName = "Leighton",
                LastName = "Bained",
                ShirtNumber = 12,
                Age = 28,
                Country = england,
                Position = PlayerPosition.CAM,
                PictureUrl = "https://cdn.images.express.co.uk/img/dynamic/67/590x/kevin-mirallas-545402.jpg",
                Team = everton,
            };

            var obertan = new Player()
            {
                FirstName = "Gabriel",
                LastName = "Obertan",
                ShirtNumber = 35,
                Age = 30,
                Country = england,
                Position = PlayerPosition.ST,
                PictureUrl = "http://e0.365dm.com/13/09/16-9/20/gabriel-obertan-newcastle-united_3005001.jpg?20130917163749",
                Team = newcastle,
            };

            var shearer = new Player()
            {
                FirstName = "Alan",
                LastName = "Sheare",
                ShirtNumber = 40,
                Age = 40,
                Country = england,
                Position = PlayerPosition.CF,
                PictureUrl = "http://i1.chroniclelive.co.uk/incoming/article10482031.ece/ALTERNATES/s615/JS71264638.jpg",
                Team = newcastle,
            };

            var milan = new Team() { Name = "Milan", LogoUrl = "https://m.netinfo.bg/gong/images/livescore/teams/1240.png", League = seriaA };
            var inter = new Team() { Name = "Inter", LogoUrl = "https://m.netinfo.bg/gong/images/livescore/teams/1244.png", League = seriaA };


            var kaka = new Player()
            {
                FirstName = "Ricardo",
                LastName = "Kaka",
                ShirtNumber = 22,
                Age = 35,
                Country = spain,
                Position = PlayerPosition.CAM,
                PictureUrl = "http://www.rossoneriblog.com/wp-content/uploads/2016/08/481333721.jpg",
                Team = milan,
            };

            var pirlo =  new Player()
            {
                FirstName = "Andrea",
                LastName = "Pirlo",
                ShirtNumber = 21,
                Age = 38,
                Country = spain,
                Position = PlayerPosition.CM,
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8f/Andrea_Pirlo_2008.jpg",
                Team = milan,
            };

            var etoo = new Player()
            {
                FirstName = "Andrea",
                LastName = "Pirlo",
                ShirtNumber = 21,
                Age = 38,
                Country = spain,
                Position = PlayerPosition.CM,
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Samuel_Eto%27o_-_Inter_Mailand_%281%29.jpg/170px-Samuel_Eto%27o_-_Inter_Mailand_%281%29.jpg",
                Team = inter,
            };

            var milito = new Player()
            {
                FirstName = "Diego",
                LastName = "Milito",
                ShirtNumber = 25,
                Age = 31,
                Country = spain,
                Position = PlayerPosition.CM,
                PictureUrl = "https://statics.sportskeeda.com/wp-content/uploads/2015/12/diego-milito-apurosport-com_-1449913096-800.jpg",
                Team = inter,
            };

            context.Players.Add(mirallas);
            context.Players.Add(obertan);
            context.Players.Add(leightBaines);
            context.Players.Add(shearer);
            context.Players.Add(kaka);
            context.Players.Add(pirlo);
            context.Players.Add(mirallas);

            context.Teams.Add(everton);
            context.Teams.Add(newcastle);
            context.Teams.Add(milan);
            context.Teams.Add(inter);

            context.SaveChanges();
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
