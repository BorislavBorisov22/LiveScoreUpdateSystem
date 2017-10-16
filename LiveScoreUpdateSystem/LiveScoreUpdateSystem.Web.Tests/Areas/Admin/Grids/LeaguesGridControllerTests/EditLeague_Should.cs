using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.LeaguesGridControllerTests
{
    [TestFixture]
    public class EditLeague_Should
    {
        [Test]
        public void CallCountryServiceUpdateMethodWithCOrrectMappedCountryDateModel_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var leagueService = new Mock<ILeagueService>();
            var leagueViewModel = new GridLeagueViewModel() { Name = "someName" };

            var mapService = new Mock<IMappingService>();

            var leagueDataModel = new League() { Name = "someName" };
            mapService.Setup(c => c.Map<League>(It.IsAny<Object>()))
                .Returns(leagueDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new LeaguesGridController(leagueService.Object);

            // act
            controller.EditLeague(leagueViewModel);

            // assert
            leagueService.Verify(c => c.Update(leagueDataModel), Times.Once);
        }

        [Test]
        public void ReturnJsonArrayWithTheEditedCountry_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var leagueService = new Mock<ILeagueService>();
            var leagueViewModel = new GridLeagueViewModel() { Name = "someName" };

            var mapService = new Mock<IMappingService>();

            var leagueDataModel = new League() { Name = "someName" };
            mapService.Setup(c => c.Map<League>(It.IsAny<Object>()))
                .Returns(leagueDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new LeaguesGridController(leagueService.Object);

            // act
            controller.EditLeague(leagueViewModel);

            // assert
            controller.WithCallTo(c => c.EditLeague(leagueViewModel))
                .ShouldReturnJson((data) => Assert.AreSame(data[0], leagueViewModel));
        }
    }
}
