using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.PlayerServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ThrowArgumentNullExceptio_WhenPassedPlayerIsNull()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesrepo = new Mock<IEfRepository<Country>>();

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesrepo.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => playerService.Add(null, null, null));
        }


        [Test]
        public void ThrowArgumentNullExceptio_WhenPassedPlayerNameIsNull()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesrepo = new Mock<IEfRepository<Country>>();

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesrepo.Object);
            var player = new Player();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => playerService.Add(player, null, null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedCountryNameIsNull()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesrepo = new Mock<IEfRepository<Country>>();

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesrepo.Object);
            var player = new Player();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => playerService.Add(player, "someName", null));
        }


        [Test]
        public void ThrowArgumentNullException_WhenCountryNameDoesNotTargetExistingCountry()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesrepo = new Mock<IEfRepository<Country>>();

            countriesrepo.Setup(cr => cr.All).Returns(new List<Country>().AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesrepo.Object);
            var player = new Player();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => playerService.Add(player, "someName", "otherName"));
        }


        [Test]
        public void ThrowArgumentNullException_WhenTeamNameDoestNotTargetExistingLeague()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "someName" };
            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country}.AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);
            var player = new Player();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => playerService.Add(player, "someName", "otherName"));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenPlayersShirtNumberInThisTeamIsAlreadyTaken()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "someName" };
            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());

            var player = new Player() { ShirtNumber = 2 };
            var team = new Team() { Name = "otherName",Players = new List<Player>() { player}  };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>() { team}.AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            var playerToAdd = new Player() { ShirtNumber = 2};

            // act & assert
            Assert.Throws<InvalidOperationException>(() => playerService.Add(playerToAdd, "otherName", "someName"));
        }


        [Test]
        public void AddPlayerWithTheCorrectProperties_WhenPassedParametersMatchAllValidations()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "someName" };
            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());

            var player = new Player() { ShirtNumber = 33 };
            var team = new Team() { Name = "otherName", Players = new List<Player>() { player } };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>() { team }.AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            var playerToAdd = new Player() { ShirtNumber = 2 };

            // act
            playerService.Add(playerToAdd, "otherName", "someName");

            // assert
            playersRepo.Verify(pr => pr.Add(It.Is<Player>(p => p.Team == team && p.Country == country)));
        }


        [Test]
        public void AddNewPlayerToTargetTeam_WhenPassedParametersMatchAllValidations()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "someName" };
            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());

            var player = new Player() { ShirtNumber = 33 };
            var team = new Team() { Name = "otherName", Players = new List<Player>() { player } };
            teamsRepo.Setup(tr => tr.All).Returns(new List<Team>() { team }.AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            var playerToAdd = new Player() { ShirtNumber = 2 };

            // act
            playerService.Add(playerToAdd, "otherName", "someName");

            // assert
            Assert.AreSame(playerToAdd, team.Players.ToList()[1]);
        }
    }
}
