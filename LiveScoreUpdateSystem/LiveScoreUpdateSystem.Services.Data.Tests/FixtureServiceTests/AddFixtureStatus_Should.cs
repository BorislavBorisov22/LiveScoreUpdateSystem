using LiveScoreUpdateSystem.Data.Models;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Factories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Providers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Tests.FixtureServiceTests
{
    [TestFixture]
    public class AddFixtureStatus_Should
    {
        public void ThrowArgumenNullException_WhenPassedIdDoesNotTargetExistingFixture()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtures = new List<Fixture>();
            fixtureRepo.Setup(r => r.All).Returns(fixtures.AsQueryable());

            var fixtureService = new FixtureService(
               fixtureRepo.Object,
               teamsRepo.Object,
               playersRepo.Object,
               fixturesFactory.Object,
               mailService.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => fixtureService.AddFixtureStatus(Guid.NewGuid(), FixtureStatus.FirstHalf));
        }

        [Test]
        public void SetFirstHalfStartCorreclty_WhenIdTargetsExistingFixtureAndStatusIsFirstHalf()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();

            var fixtures = new List<Fixture>() { new Fixture() { Id = fixtureId } };
            fixtureRepo.Setup(r => r.All).Returns(fixtures.AsQueryable());

            var fixtureService = new FixtureService(
               fixtureRepo.Object,
               teamsRepo.Object,
               playersRepo.Object,
               fixturesFactory.Object,
               mailService.Object);

            var timeProvider = new Mock<ITimeProvider>();
            var date = new DateTime(2012, 12, 12);
            timeProvider.Setup(r => r.CurrentDate).Returns(date);

            TimeProvider.CurrentProvider = timeProvider.Object;

            fixtureRepo.Setup(r => r.Update(It.Is<Fixture>(f => f.FirstHalfStart == date)));

            // act
            fixtureService.AddFixtureStatus(fixtureId, FixtureStatus.FirstHalf);

            // assert
            fixtureRepo.Verify(r => r.Update(It.Is<Fixture>(f => f.FirstHalfStart == date)), Times.Once);
        }

        [Test]
        public void SetSecondHalfStartCorrectly_WhenIdTargetsExistingFixtureAndStatusIsSecondHalf()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();

            var fixtures = new List<Fixture>() { new Fixture() { Id = fixtureId } };
            fixtureRepo.Setup(r => r.All).Returns(fixtures.AsQueryable());

            var fixtureService = new FixtureService(
               fixtureRepo.Object,
               teamsRepo.Object,
               playersRepo.Object,
               fixturesFactory.Object,
               mailService.Object);

            var timeProvider = new Mock<ITimeProvider>();
            var date = new DateTime(2012, 12, 12);
            timeProvider.Setup(r => r.CurrentDate).Returns(date);

            TimeProvider.CurrentProvider = timeProvider.Object;

            fixtureRepo.Setup(r => r.Update(It.Is<Fixture>(f => f.SecondHalfStart == date)));

            // act
            fixtureService.AddFixtureStatus(fixtureId, FixtureStatus.SecondHalf);

            // assert
            fixtureRepo.Verify(r => r.Update(It.Is<Fixture>(f => f.SecondHalfStart == date)), Times.Once);
        }

        [Test]
        public void CallFixtureMailServiceSendFixtureResultMailWithCorrectFixtureObjectAndSubscribers_WhenFixtureStatusIsFullTime()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();

            var homeTeam = new Team() { Subscribers = new List<User>() { new User() { TestUsername = "username" } } };
            var awayTeam = new Team() { Subscribers = new List<User>() { new User() { TestUsername = "otherUsername" } } };


            var fixtures = new List<Fixture>() { new Fixture() { Id = fixtureId, HomeTeam = homeTeam, AwayTeam = awayTeam } };
            fixtureRepo.Setup(r => r.All).Returns(fixtures.AsQueryable());

            var timeProvider = new Mock<ITimeProvider>();
            var date = new DateTime(2012, 12, 12);
            timeProvider.Setup(r => r.CurrentDate).Returns(date);

            TimeProvider.CurrentProvider = timeProvider.Object;

            mailService.Setup(m => m
                                    .SendFixtureResultMail(It.Is<Fixture>(
                                        f => f == fixtures[0]),
                                        It.Is<IEnumerable<string>>(c => c.Count() == 2)));

            var fixtureService = new FixtureService(
                                   fixtureRepo.Object,
                                   teamsRepo.Object,
                                   playersRepo.Object,
                                   fixturesFactory.Object,
                                   mailService.Object);

            // act
            fixtureService.AddFixtureStatus(fixtureId, FixtureStatus.FullTime);

            // assert
            mailService.Verify(m => m
                                 .SendFixtureResultMail(It.Is<Fixture>(
                                     f => f == fixtures[0]),
                                     It.Is<IEnumerable<string>>(c => c.Count() == 2)), Times.Once);
        }
    }
}
