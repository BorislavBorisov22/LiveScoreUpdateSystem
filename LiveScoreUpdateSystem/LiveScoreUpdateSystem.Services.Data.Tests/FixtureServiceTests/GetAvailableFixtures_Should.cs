using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Factories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.FixtureServiceTests
{
    [TestFixture]
    public class GetAvailableFixtures_Should
    {
        [Test]
        public void ShouldReturnFixturesOnyFromTheSpecifiedDate_WhenPassedDateIsValid()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var targetDate = new DateTime?(new DateTime(2017, 12, 12));
            var fixtures = new List<Fixture>()
            {
                new Fixture(){FirstHalfStart = targetDate },
                new Fixture(){FirstHalfStart = new DateTime(2017,12,2)} ,
                new Fixture(){FirstHalfStart = new DateTime(2017,12,4) },
                new Fixture() { FirstHalfStart = targetDate },
            };

            fixtureRepo.Setup(r => r.All).Returns(fixtures.AsQueryable());

            var fixtureService = new FixtureService(
               fixtureRepo.Object,
               teamsRepo.Object,
               playersRepo.Object,
               fixturesFactory.Object,
               mailService.Object);

            // act
            var returnedFixtures = fixtureService.GetAvailableFixtures(targetDate.Value).ToList();

            // assert
            Assert.AreSame(returnedFixtures[0], fixtures[0]);
            Assert.AreSame(returnedFixtures[1], fixtures[3]);
        }
    }
}
