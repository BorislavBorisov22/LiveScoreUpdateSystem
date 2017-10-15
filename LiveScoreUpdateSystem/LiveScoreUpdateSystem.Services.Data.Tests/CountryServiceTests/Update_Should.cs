using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Tests.CountryServiceTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void NotCallRespositoriesUpdateMethod_WhenPassedCountryIsNotFound()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var country = new Country() { Name = "presentName" };
            var countries = new List<Country>() { country }.AsQueryable();

            repository.Setup(r => r.All).Returns(countries);
            repository.Setup(r => r.Update(It.Is<Country>(c => c == country)));

            var countryService = new CountryService(repository.Object);
            var updatingCountry = new Country() { Name = "notPresentName" };

            // act
            countryService.Update(updatingCountry);

            // assert
            repository.Verify(r => r.Update(It.Is<Country>(c => c == country)), Times.Never);
        }

        [Test]
        public void ShouldSetPropertiesCorrectlyToModelForUpdate_WhenTargetCountryIsPresentInRepository()
        {
            // arrange
            var repository = new Mock<IEfRepository<Country>>();

            var matchingId = Guid.NewGuid();

            var country = new Country() { Name = "oldName", Id=matchingId, FlagPictureUrl="OldFlag" };
            var countries = new List<Country>() { country }.AsQueryable();

            var countryService = new CountryService(repository.Object);
            var updatingCountry = new Country() { Name = "newName", Id=matchingId, FlagPictureUrl="newFlag" };

            repository.Setup(r => r.All).Returns(countries);
            repository
                .Setup(r => r.Update(It.Is<Country>(c => c.FlagPictureUrl == updatingCountry.FlagPictureUrl && 
                                c.Name == updatingCountry.Name)));

            // act
            countryService.Update(updatingCountry);

            // assert
            repository
           .Verify(r => r.Update(It.Is<Country>(c => c.FlagPictureUrl == updatingCountry.FlagPictureUrl &&
                           c.Name == updatingCountry.Name)), Times.Once);
        }
    }
}
