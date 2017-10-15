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
    public class UpdateShould
    {
        [Test]
        public void CallPlayerRepositoryUpdateMethodWithObjectWithCorrectProperties_WhenTargetPlayerIsPresent()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var playerId = Guid.NewGuid();

            var updatePlayer = new Player() { Id = playerId};
            var existingPlayer = new Player()
            {
                Id = playerId,
                FirstName = "someName",
                LastName = "otherName",
                Age = 33,
                ShirtNumber = 22,
                PictureUrl = "SomeUrl"
            };

            playersRepo.Setup(pr => pr.All).Returns(new List<Player>() { existingPlayer }.AsQueryable());
            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            // act
            playerService.Update(updatePlayer);

            // assert
            playersRepo.Verify(pr => pr.Update(It.Is<Player>(p => p.FirstName == existingPlayer.FirstName &&
            p.LastName == existingPlayer.LastName &&
            p.Age == existingPlayer.Age &&
            p.ShirtNumber == existingPlayer.ShirtNumber &&
            p.PictureUrl == existingPlayer.PictureUrl
            )));
        }
    }
}
