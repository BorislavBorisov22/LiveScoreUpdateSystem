using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.CountriesGridControllerTests
{
    [TestFixture]
    public class EditCountry_Should
    {
        [Test]
        public void CallCountryServiceUpdateMethodWithCOrrectMappedCountryDateModel_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var countryViewModel = new GridCountryViewModel() { Name = "someName" };

            var mapService = new Mock<IMappingService>();

            var countryDataModel = new Country() { Name="someName"};
            mapService.Setup(c => c.Map<Country>(It.IsAny<Object>()))
                .Returns(countryDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new CountriesGridController(countryService.Object);

            // act
            controller.EditCountry(countryViewModel);

            // assert
            countryService.Verify(c => c.Update(countryDataModel), Times.Once);
        }

        [Test]
        public void ReturnJsonArrayWithTheEditedCountry_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var countryViewModel = new GridCountryViewModel() { Name = "someName" };

            var mapService = new Mock<IMappingService>();

            var countryDataModel = new Country() { Name = "someName" };
            mapService.Setup(c => c.Map<Country>(It.IsAny<Object>()))
                .Returns(countryDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new CountriesGridController(countryService.Object);

            // act
            controller.EditCountry(countryViewModel);

            // assert
            controller.WithCallTo(c => c.EditCountry(countryViewModel))
                .ShouldReturnJson((data) => Assert.AreSame(data[0], countryViewModel));
        }
    }
}
