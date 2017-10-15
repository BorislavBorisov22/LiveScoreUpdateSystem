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
    public class Add_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedTeamIsNull()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act  & assert
            Assert.Throws<ArgumentNullException>(() => teamService.Add(null, null));
        }


        [Test]
        public void ThrowInvalidOperationException_WhenPassedTeamAlreadyExists()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var team = new Team() { Name = "someName" };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>() { team }.AsQueryable());

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act  & assert
            Assert.Throws<InvalidOperationException>(() => teamService.Add(team, team.Name));
        }


        [Test]
        public void ThrowInvalidOperationException_WhenLeagueNameDoesNotTargetExistingLeague()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var team = new Team() { Name = "someName" };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>().AsQueryable());

            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>().AsQueryable());

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => teamService.Add(team, team.Name));
        }

        [Test]
        public void CallTeamsReposAddMethodWithCorrectlySetLeagueToTeamModel_WhenAlValidationPassed()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var team = new Team() { Name = "someName" };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>().AsQueryable());

            var league = new League() { Name = "someLeague" };
            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>() { league }.AsQueryable());

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            teamService.Add(team, league.Name);

            // assert
            teamsRepo.Verify(tr => tr.Add(It.Is<Team>(t => t.League == league)));
        }
    }
}
