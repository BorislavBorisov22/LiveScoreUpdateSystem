using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.CountryServiceTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void ReturnFalse_WhenCountryWithThePassedNameIsNotPresentInRepository()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();
            var countries = new List<Country>().AsQueryable();
            repository.Setup(r => r.All).Returns(countries);

            var countryService = new CountryService(repository.Object);

            // act
            var returnedResult = countryService.Delete("notPresentName");

            // assert
            Assert.IsFalse(returnedResult);
        }

        [Test]
        public void CallRepositoriesDeleteMethodWithCorrectCountryModel_WhenCountryWithPassedNameIsFound()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "presentName" };
            var countries = new List<Country>() { country }.AsQueryable();

            repository.Setup(r => r.All).Returns(countries);
            repository.Setup(r => r.Delete(It.Is<Country>(c => c == country)));

            var countryService = new CountryService(repository.Object);

            // act
            var returnedResult = countryService.Delete("presentName");

            // assert
            repository.Verify(r => r.Delete(It.Is<Country>(c => c == country)), Times.Once);
        }

        [Test]
        public void ReturnTrue_WhenCountryWithPassedNameIsFoundAndDeleted()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "presentName" };
            var countries = new List<Country>() { country }.AsQueryable();

            repository.Setup(r => r.All).Returns(countries);
            repository.Setup(r => r.Delete(It.Is<Country>(c => c == country)));

            var countryService = new CountryService(repository.Object);

            // act
            var returnedResult = countryService.Delete("presentName");

            // assert
            Assert.IsTrue(returnedResult);
        }
    }
}
