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
    public class Add_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedLeagueIsNull()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);
            var league = new League();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => leagueService.Add(league));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedLeagueNameIsNull()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);
            var league = new League();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => leagueService.Add(league));
        }

        [Test]
        public void ThrowArgumentNullException_WhenTargetCountryIsNotFound()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            var country = new Country() { Name = "someName" };
            var league = new League() { Country = country };

            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>().AsQueryable());

            // act & assert
            Assert.Throws<ArgumentNullException>(() => leagueService.Add(league));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenLeagueForTheProvidedSeasonAlreadyExists()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            var country = new Country() { Name = "someName" };
            var league = new League() { Country = country, Season = 2017 };

            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());
            leaguesRepo.Setup(l => l.All).Returns(new List<League>() { league }.AsQueryable());

            // act & assert
            Assert.Throws<InvalidOperationException>(() => leagueService.Add(league));
        }


        [Test]
        public void ThrowInvalidOperationException_WhenTryingToAddExistingLeagueButWithDifferentCountry()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            var country = new Country() { Name = "someName" };
            var league = new League() { Country = country, Season = 2015, Name = "someName" };

            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());

            var existingLeague = new League() { Name = "someName", Country = new Country() { Name = "OtherCountryName" } };

            leaguesRepo.Setup(l => l.All).Returns(new List<League>() { existingLeague }.AsQueryable());

            // act & assert
            Assert.Throws<InvalidOperationException>(() => leagueService.Add(league));
        }

        [Test]
        public void CallLeaguesRepoAddMethodWithValidLeagueObject_WhenLeaguePassedIsValidToBeAdded()
        {
            // arrange
            var leaguesRepo = new Mock<IEfRepository<League>>();
            var countriesRepo = new Mock<IEfRepository<Country>>();

            var leagueService = new LeagueService(leaguesRepo.Object, countriesRepo.Object);

            var country = new Country() { Name = "someName" };
            var league = new League() { Country = country, Season = 2015, Name = "someName" };

            countriesRepo.Setup(cr => cr.All).Returns(new List<Country>() { country }.AsQueryable());

            var existingLeague = new League() { Name = "someName", Country = new Country() { Name = "someName" } };

            leaguesRepo.Setup(l => l.All).Returns(new List<League>() { existingLeague }.AsQueryable());

            leaguesRepo.Setup(lr => lr.Add(It.Is<League>(l => l.Country == country)));

            // act
            leagueService.Add(league);

            // assert
            leaguesRepo.Verify(lr => lr.Add(It.Is<League>(l => l.Country == country)), Times.Once);
        }
    }
}
