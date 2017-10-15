using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.LeagueServiceTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void CallLeaguesRepoUpdateMethodWithCorrectLeagueObject_WhenProvideLeagueHasValidNewNameAndSeason()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var league = new League() { Name = "someName", Id = Guid.NewGuid(), Season = 2017 };
            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>() { league }.AsQueryable());

            var updateLeague = new League() { Name = "someName", Id = league.Id, Season = 2010 };
            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            // act
            leagueService.Update(updateLeague);

            // assert
            leaguesRepo.Verify(lr => lr.Update(It.Is<League>(l => l.Name == updateLeague.Name && l.Season == updateLeague.Season)));
        }
    }
}
