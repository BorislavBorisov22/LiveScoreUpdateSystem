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
    public class Delete_Should
    {
        [Test]
        public void CallTeamsRepoDeleteMethodWithCorrectTeamObject_WhenTargetTeamIsFound()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            var team = new Team() { Id = Guid.NewGuid() };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>() { team}.AsQueryable());
            teamsRepo.Setup(tr => tr.Delete(It.Is<Team>(t => t == team)));

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            teamService.Delete(team.Id);

            // assert
            teamsRepo.Verify(tr => tr.Delete(It.Is<Team>(t => t == team)), Times.Once);
        }

        [Test]
        public void NotCallTeamsRepoDeleteMethod_WhenTargetTeamIsFound()
        {
            // arrange
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var leaguesRepo = new Mock<IEfRepository<League>>();

            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>().AsQueryable());

            var teamService = new TeamService(teamsRepo.Object, leaguesRepo.Object);

            // act
            teamService.Delete(Guid.NewGuid());

            // assert
            teamsRepo.Verify(tr => tr.Delete(It.IsAny<Team>()), Times.Never);
        }
    }
}
