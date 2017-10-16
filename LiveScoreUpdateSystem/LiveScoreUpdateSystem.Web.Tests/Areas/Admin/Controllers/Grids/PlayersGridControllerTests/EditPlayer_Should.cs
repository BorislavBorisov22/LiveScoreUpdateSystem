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

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.PlayersGridControllerTests
{
    [TestFixture]
    public class EditPlayer_Should
    {
        [Test]
        public void CallPlayerServiceUpdateMethodWithCorrectMappedPlayerDateModel_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var leagueService = new Mock<IPlayerService>();
            var playerViewModel = new GridPlayerViewModel() { FirstName = "someName" };

            var mapService = new Mock<IMappingService>();

            var playerDatamodel = new Player() { FirstName = "someName" };
            mapService.Setup(c => c.Map<Player>(It.IsAny<Object>()))
                .Returns(playerDatamodel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new PlayersGridController(leagueService.Object);

            // act
            controller.EditPlayer(playerViewModel);

            // assert
            leagueService.Verify(c => c.Update(playerDatamodel), Times.Once);
        }

        [Test]
        public void ReturnJsonArrayWithTheEditedPlayer_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var playerService = new Mock<IPlayerService>();
            var playerVieWModel = new GridPlayerViewModel() { FirstName = "someName" };

            var mapService = new Mock<IMappingService>();

            var leagueDataModel = new Player() { FirstName = "someName" };
            mapService.Setup(c => c.Map<Player>(It.IsAny<Object>()))
                .Returns(leagueDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new PlayersGridController(playerService.Object);

            // act
            controller.EditPlayer(playerVieWModel);

            // assert
            controller.WithCallTo(c => c.EditPlayer(playerVieWModel))
                .ShouldReturnJson((data) => Assert.AreSame(data[0], playerVieWModel));
        }
    }
}
