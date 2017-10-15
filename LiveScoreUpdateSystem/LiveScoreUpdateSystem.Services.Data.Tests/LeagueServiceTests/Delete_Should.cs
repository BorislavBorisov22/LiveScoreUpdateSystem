using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Tests.LeagueServiceTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void ReturnFalse_WhenPassedLeagueNameDoesNotTargetExistingLeague()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>().AsQueryable());
            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            // act 
            var returnValue = leagueService.Delete("someName");

            // assert
            Assert.IsFalse(returnValue);
        }

        [Test]
        public void CallLeagueReposDeleteMethodWithCorrectLeagueObject_WhenLeagueNameTargetsExistingLeague()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var league = new League() { Name = "someName" };
            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>() { league }.AsQueryable());
            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            // act 
            var returnValue = leagueService.Delete("someName");

            // assert
            leaguesRepo.Verify(lr => lr.Delete(It.Is<League>(l => l == league)));
        }

        [Test]
        public void ReturnTrue_WhenLeagueIsDeletedSuccessfully()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var league = new League() { Name = "someName" };
            leaguesRepo.Setup(lr => lr.All).Returns(new List<League>() { league }.AsQueryable());
            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            // act 
            var returnValue = leagueService.Delete("someName");

            // assert
            Assert.IsTrue(returnValue);
        }
    }
}
