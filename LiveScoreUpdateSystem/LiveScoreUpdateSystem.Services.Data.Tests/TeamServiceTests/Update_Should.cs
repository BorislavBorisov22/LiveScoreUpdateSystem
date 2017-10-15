using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.TeamServiceTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void ThrowArgumentException_WhenNewTeamNameIsAlreadyTaken()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var firstLeague = new League() { Name = "someName" };
            var secondLeague = new League() { Name = "otherName" };

            var teams = new List<Team>()
            {
                new Team(){Id= Guid.NewGuid(), Name = "Team1", League = firstLeague},
                new Team(){Id= Guid.NewGuid(), Name = "Team2", League = secondLeague},
                new Team(){Id= Guid.NewGuid(), Name = "Team3", League = firstLeague},
            };

            teamsRepo.Setup(t => t.All).Returns(teams.AsQueryable());
            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act & assert
            Assert.Throws<ArgumentException>(() => teamService.Update(teams[0].Id, "Team2", "logo"));
        }

        [Test]
        public void CallTeamsRepoUpdateMethodWithCorrectlyUpdateTeamObject_WhenAllValidationsPass()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var firstLeague = new League() { Name = "someName" };
            var secondLeague = new League() { Name = "otherName" };

            var teams = new List<Team>()
            {
                new Team(){Id= Guid.NewGuid(), Name = "Team1", League = firstLeague},
                new Team(){Id= Guid.NewGuid(), Name = "Team2", League = secondLeague},
                new Team(){Id= Guid.NewGuid(), Name = "Team3", League = firstLeague},
            };

            teamsRepo.Setup(t => t.All).Returns(teams.AsQueryable());
            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            teamService.Update(teams[0].Id, "Team5", "logo");

            // assert
            teamsRepo.Verify(tr => tr.Update(It.Is<Team>(t => t.Name == "Team5" && t.LogoUrl == "logo")));
        }
    }
}
