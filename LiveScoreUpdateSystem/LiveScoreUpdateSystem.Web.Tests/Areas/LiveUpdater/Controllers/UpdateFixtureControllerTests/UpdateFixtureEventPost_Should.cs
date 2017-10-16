using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models;
using LiveScoreUpdateSystem.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.LiveUpdater.Controllers.UpdateFixtureControllerTests
{
    [TestFixture]
    public class UpdateFixtureEventPost_Should
    {
        [Test]
        public void CallFixtureServiceAddFixtureEventWithCorrectParameters_WhenPassMathodParametersAreValid()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();
            var fixtureModel = new UpdateFixtureViewModel() { Id = id,PlayerId = id, FixtureEvent = FixtureEventType.Penalty, Minute = 22};

            // act
            controller.UpdateFixtureEvent(id, fixtureModel);

            // assert
            fixtureService.Verify(f => f.AddFixtureEvent(id, fixtureModel.FixtureEvent, fixtureModel.Minute, fixtureModel.Id), Times.Once);
        }

        [Test]
        public void RedirectToScoresControllerAvalableScoresAction_WhenPassMathodParametersAreValid()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();
            var fixtureModel = new UpdateFixtureViewModel() { Id = id, PlayerId = id, FixtureEvent = FixtureEventType.Penalty, Minute = 22 };

            // act
            controller.UpdateFixtureEvent(id, fixtureModel);

            // assert
            controller.WithCallTo(c => c.UpdateFixtureEvent(id, fixtureModel))
                .ShouldRedirectTo<ScoresController>(sc => sc.AvailableScores());
        }
    }
}
