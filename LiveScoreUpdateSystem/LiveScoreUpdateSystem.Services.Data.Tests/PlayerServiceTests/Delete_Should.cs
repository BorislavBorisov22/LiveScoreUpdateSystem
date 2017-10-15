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
    public class Delete_Should
    {
        [Test]
        public void CallPlayersRepoWithCorrectPlayerObject_WhenProvidedPlayerIdMatchesAPlayer()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            var playerId = Guid.NewGuid();
            var player = new Player() { Id = playerId };
            playersRepo.Setup(pr => pr.All).Returns(new List<Player>() { player}.AsQueryable());

            // act
            playerService.Delete(playerId);

            // assert
            playersRepo.Verify(pr => pr.Delete(It.Is<Player>(p => p == player)));
        }
    }
}
