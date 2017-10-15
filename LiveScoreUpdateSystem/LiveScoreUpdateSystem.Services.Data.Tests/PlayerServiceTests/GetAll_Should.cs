using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Tests.PlayerServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnCorrectFilteredPlayersByProvidedTeamId_WhenInvoked()
        {
            // arrange
            var playersRepo = new Mock<IEfRepository<Player>>();
            var teamsRepo = new Mock<IEfRepository<Team>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var targetTeam = new Team() { Id = Guid.NewGuid() };
            var players = new List<Player>()
            {
                new Player(){Team = targetTeam},
                new Player(){Team = new Team(){Id= Guid.NewGuid() } },
                new Player(){Team = targetTeam}
            };

            playersRepo.Setup(tr => tr.All).Returns(players.AsQueryable());

            var playerService = new PlayerService(playersRepo.Object, teamsRepo.Object, countriesRepo.Object);

            // act
            var returnValues = playerService.GetAll(targetTeam.Id).ToList();

            // assert
            Assert.AreEqual(2, returnValues.Count);
            Assert.AreSame(returnValues[0], players[0]);
            Assert.AreSame(returnValues[1], players[2]);
        }
    }
}
