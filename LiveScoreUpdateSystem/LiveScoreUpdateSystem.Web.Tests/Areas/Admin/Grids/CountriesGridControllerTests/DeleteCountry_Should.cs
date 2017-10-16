using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.CountriesGridControllerTests
{
    [TestFixture]
    public class DeleteCountry_Should
    {
        [Test]
        public void CallCountryServiceDeleteMethodWithCorrectCountryName_WhenPassedModelIsNotNull()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var controller = new CountriesGridController(countryService.Object);

            var countryViewModel = new GridCountryViewModel() { Name = "someName" };

            // act
            controller.DeleteCountry(countryViewModel);

            // assert
            countryService.Verify(c => c.Delete("someName"), Times.Once);
        }

        [Test]
        public void ReturnJsonAsResult_WhenInvoked()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var controller = new CountriesGridController(countryService.Object);

            var countryViewModel = new GridCountryViewModel() { Name = "someName" };

            // act
            controller.DeleteCountry(countryViewModel);

            // assert
            controller.WithCallTo(c => c.DeleteCountry(countryViewModel))
                .ShouldReturnJson(data =>
                {
                    Assert.That(data[0].Name, Is.EqualTo("someName"));
                });
        }
    }
}
