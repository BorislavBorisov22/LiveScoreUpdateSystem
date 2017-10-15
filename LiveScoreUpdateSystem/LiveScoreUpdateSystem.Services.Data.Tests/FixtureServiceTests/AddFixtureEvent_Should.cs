using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
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
    public class AddFixtureEvent_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIdDoesNotTargetExistingFixture()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();


            var fixtureId = Guid.NewGuid();
            var fixtureEvent = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            fixtureRepo.Setup(x => x.All).Returns(new List<Fixture>().AsQueryable());

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => fixtureService.AddFixtureEvent(fixtureId, fixtureEvent, minute, playerId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPlayerIdDoesNotTargetExistingPlayer()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();


            var fixtureId = Guid.NewGuid();
            var fixtureEvent = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            var fixtures = new List<Fixture>() { new Fixture() { Id = fixtureId } }.AsQueryable();
            fixtureRepo.Setup(x => x.All).Returns(fixtures);

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => fixtureService.AddFixtureEvent(fixtureId, fixtureEvent, minute, playerId));
        }

        [Test]
        public void CallFixtureFactoriesGetFixtureEventMethodWithCorrectParameters_WhenPlayerIdAndFixtureIdAreValid()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();
            var fixtureEventType = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            var players = new List<Player>() { new Player() { Id = playerId } };
            playersRepo.Setup(pr => pr.All).Returns(players.AsQueryable());

            var fixtures = new List<Fixture>()
            { new Fixture()
                {
                    Id = fixtureId,
                    FixtureEvents = new List<FixtureEvent>(),
                    HomeTeam = new Team(){Players = players },
                }
            }.AsQueryable();
            fixtureRepo.Setup(x => x.All).Returns(fixtures);

            var fixtureEvent = new FixtureEvent();
            fixturesFactory.Setup(f => f.GetFixtureEvent(fixtureEventType, minute, players[0])).Returns(fixtureEvent);

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act
            fixtureService.AddFixtureEvent(fixtureId, fixtureEventType, minute, playerId);

            // assert
            fixturesFactory.Verify(f => f.GetFixtureEvent(fixtureEventType, minute, players[0]), Times.Once);
        }

        [Test]
        public void AddCorrectFixtureEventToTargetFixturesEvents_WhenPlayerIdAndFixtureIdAreValid()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();
            var fixtureEventType = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            var players = new List<Player>() { new Player() { Id = playerId } };
            playersRepo.Setup(pr => pr.All).Returns(players.AsQueryable());

            var fixtures = new List<Fixture>()
            { new Fixture()
                {
                    Id = fixtureId,
                    FixtureEvents = new List<FixtureEvent>(),
                    HomeTeam = new Team(){Players = players },
                }
            }.AsQueryable();
            fixtureRepo.Setup(x => x.All).Returns(fixtures);

            var fixtureEvent = new FixtureEvent();
            fixturesFactory.Setup(f => f.GetFixtureEvent(fixtureEventType, minute, players[0])).Returns(fixtureEvent);

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act
            fixtureService.AddFixtureEvent(fixtureId, fixtureEventType, minute, playerId);

            // assert
            fixtureRepo.Verify(r => r.Update(It.Is<Fixture>(f => f.FixtureEvents.First() == fixtureEvent)));
        }

        [Test]
        public void IncreaseHomeTeamsScoreByOne_WhenPlayerIsFoundInHomeTeamsPlayers()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();
            var fixtureEventType = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            var players = new List<Player>() { new Player() { Id = playerId } };
            playersRepo.Setup(pr => pr.All).Returns(players.AsQueryable());

            var fixtures = new List<Fixture>()
            { new Fixture()
                {
                    Id = fixtureId,
                    FixtureEvents = new List<FixtureEvent>(),
                    HomeTeam = new Team(){Players = players },
                }
            }.AsQueryable();
            fixtureRepo.Setup(x => x.All).Returns(fixtures);

            var fixtureEvent = new FixtureEvent();
            fixturesFactory.Setup(f => f.GetFixtureEvent(fixtureEventType, minute, players[0])).Returns(fixtureEvent);

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act
            fixtureService.AddFixtureEvent(fixtureId, fixtureEventType, minute, playerId);

            // assert
            fixtureRepo.Verify(r => r.Update(It.Is<Fixture>(f => f.ScoreHomeTeam == 1)));
        }
        [Test]
        public void IncreaseAwayTeamsScoreByOne_WhenPlayerIsNotFoundInHomeTeamsPlayers()
        {
            // arrange
            var fixtureRepo = new Mock<IEfRepository<Fixture>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var playersRepo = new Mock<IEfRepository<Player>>();
            var fixturesFactory = new Mock<IFixturesFactory>();
            var mailService = new Mock<IFixtureMailService>();

            var fixtureId = Guid.NewGuid();
            var fixtureEventType = FixtureEventType.Goal;
            int minute = 22;
            var playerId = Guid.NewGuid();

            var players = new List<Player>() { new Player() { Id = playerId } };
            playersRepo.Setup(pr => pr.All).Returns(players.AsQueryable());

            var fixtures = new List<Fixture>()
            { new Fixture()
                {
                    Id = fixtureId,
                    FixtureEvents = new List<FixtureEvent>(),
                    HomeTeam = new Team(){Players = new List<Player>() },
                }
            }.AsQueryable();
            fixtureRepo.Setup(x => x.All).Returns(fixtures);

            var fixtureEvent = new FixtureEvent();
            fixturesFactory.Setup(f => f.GetFixtureEvent(fixtureEventType, minute, players[0])).Returns(fixtureEvent);

            var fixtureService = new FixtureService(
              fixtureRepo.Object,
              teamsRepo.Object,
              playersRepo.Object,
              fixturesFactory.Object,
              mailService.Object);

            // act
            fixtureService.AddFixtureEvent(fixtureId, fixtureEventType, minute, playerId);

            // assert
            fixtureRepo.Verify(r => r.Update(It.Is<Fixture>(f => f.ScoreAwayTeam == 1)));
        }
    }
}

