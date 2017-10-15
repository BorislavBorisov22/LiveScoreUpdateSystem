using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
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
    public class Add_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenHomeAndAwayTeamsNameAreEqual()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureService = new FixtureService(
                fixtureRepo.Object,
                teamsRepo.Object,
                playersRepo.Object,
                fixturesFactory.Object,
                mailService.Object);

            // act and assert
            Assert.Throws<InvalidOperationException>(() => fixtureService.Add("sameName", "sameName", null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenTeamWithPassedHomeTeamNameIsNotPresent()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            teamsRepo.Setup(r => r.All).Returns(new List<Team>().AsQueryable());

            var fixtureService = new FixtureService(
                fixtureRepo.Object,
                teamsRepo.Object,
                playersRepo.Object,
                fixturesFactory.Object,
                mailService.Object);

            // act and assert
            Assert.Throws<ArgumentNullException>(() => fixtureService.Add("notPresentName", "sameName", null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenTeamWithPassedAwayTeamNameIsNotPresent()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var teams = new List<Team>() { new Team() { Name = "presentName" } };
            teamsRepo.Setup(r => r.All).Returns(teams.AsQueryable());

            var fixtureService = new FixtureService(
                fixtureRepo.Object,
                teamsRepo.Object,
                playersRepo.Object,
                fixturesFactory.Object,
                mailService.Object);

            // act and assert
            Assert.Throws<ArgumentNullException>(() => fixtureService.Add("presentName", "notPresentNae", null));
        }

        [Test]
        public void CallFixturesFactoryGetFixtureWithCorrectParameters_WhenBothTeamsAreFoundInRepository()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var teams = new List<Team>() { new Team() { Name = "presentName" }, new Team() { Name="AnotherPresentName" } };
            teamsRepo.Setup(r => r.All).Returns(teams.AsQueryable());

            var date = new DateTime?(new DateTime(2015, 1, 1));
            var returnedFixture = new Fixture();
            fixturesFactory.Setup(f => f.GetFixture(It.IsAny<Team>(), It.IsAny<Team>(), date));

            var fixtureService = new FixtureService(
                fixtureRepo.Object,
                teamsRepo.Object,
                playersRepo.Object,
                fixturesFactory.Object,
                mailService.Object);

            fixtureService.Add("presentName", "AnotherPresentName", date);

            // assert
            fixturesFactory.Verify(f => f.GetFixture(teams[0], teams[1], date));
        }

        [Test]
        public void CallFixturesRepositoryAddMethodWithFixtureReturnFromFactory_WhenPassedParametersAreValid()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var teams = new List<Team>() { new Team() { Name = "presentName" }, new Team() { Name = "AnotherPresentName" } };
            teamsRepo.Setup(r => r.All).Returns(teams.AsQueryable());

            var date = new DateTime?(new DateTime(2015, 1, 1));
            var returnedFixture = new Fixture();
            fixturesFactory.Setup(f => f.GetFixture(It.IsAny<Team>(), It.IsAny<Team>(), date))
                .Returns(returnedFixture);
            fixtureRepo.Setup(fr => fr.Add(It.Is<Fixture>(f => f == returnedFixture)));

            var fixtureService = new FixtureService(
                fixtureRepo.Object,
                teamsRepo.Object,
                playersRepo.Object,
                fixturesFactory.Object,
                mailService.Object);

            fixtureService.Add("presentName", "AnotherPresentName", date);

            // assert
            fixtureRepo.Verify(fr => fr.Add(It.Is<Fixture>(f => f == returnedFixture)), Times.Once);
        }
    }
}
