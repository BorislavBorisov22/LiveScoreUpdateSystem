using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Tests.CountryServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedCountryToAddIsNull()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var countryService = new CountryService(repository.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => countryService.Add(null));
        }

        [Test]
        public void NotThrow_WhenPassedCountryToAddIsValid()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var countryService = new CountryService(repository.Object);
            var countryModel = new Country() { Name = "SomeName" };

            // act & assert
            Assert.DoesNotThrow(() => countryService.Add(countryModel));
        }

        [Test]
        public void CallRepositoriesAddMethodWithCorrectCountryModel_WhenPassedCountryModelIsValid()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var countryService = new CountryService(repository.Object);
            var countryModel = new Country() { Name = "SomeName" };

            repository.Setup(r => r.Add(It.Is<Country>(c => c == countryModel)));

            // act
            countryService.Add(countryModel);

            // assert
            repository.Verify(r => r.Add(It.Is<Country>(c => c == countryModel)), Times.Once);
        }
    }
}
