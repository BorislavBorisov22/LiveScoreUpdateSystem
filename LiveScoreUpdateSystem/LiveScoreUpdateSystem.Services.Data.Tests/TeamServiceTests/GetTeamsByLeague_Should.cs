using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.TeamServiceTests
{
    [TestFixture]
    public class GetTeamsByLeague_Should
    {
        [Test]
        public void ReturnCorrectlyFilteredTeams_WhenInvoked()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var firstLeague = new League() { Name = "someName" };
            var secondLeague = new League() { Name = "otherName" };

            var teams = new List<Team>()
            {
                new Team(){Name = "Team1", League = firstLeague},
                new Team(){Name = "Team2", League = secondLeague},
                new Team(){Name = "Team3", League = firstLeague},
            };

            teamsRepo.Setup(t => t.All).Returns(teams.AsQueryable());
            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            var result = teamService.GetTeamsByLeague(firstLeague.Name);

            // assert
            Assert.AreEqual(result.Count(), 2);
            Assert.AreSame(result.First(), teams[0]);
            Assert.AreSame(result.Skip(1).First(), teams[2]);
        }

        [Test]
        public void ReturnEmptyCollection_WhenNoTeamsInThisLeagueArePresent()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var firstLeague = new League() { Name = "someName" };
            var secondLeague = new League() { Name = "otherName" };

            var teams = new List<Team>()
            {
                new Team(){Name = "Team1", League = firstLeague},
                new Team(){Name = "Team2", League = secondLeague},
                new Team(){Name = "Team3", League = firstLeague},
            };

            teamsRepo.Setup(t => t.All).Returns(teams.AsQueryable());
            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            var result = teamService.GetTeamsByLeague("leagueNotPresent");

            // assert
            Assert.AreEqual(result.Count(), 0);
        }
    }
}
