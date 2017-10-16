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
    public class UpdateFixtureStatusPost_Should
    {
        [Test]
        public void CallFixtureServiceAddFixtureStatusWithCorrectParams_WhenInvokedWithValidParameters()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();
            var model = new UpdateFixtureStatusViewModel() { Status = FixtureStatus.HalfTime };

            // act
            controller.UpdateFixtureStatus(id, model);

            // asset
            fixtureService.Verify(f => f.AddFixtureStatus(id, model.Status), Times.Once);
        }

        [Test]
        public void RedirectToScoresControllerAvailableScores_WhenInvokedWithValidParameters()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();
            var model = new UpdateFixtureStatusViewModel() { Status = FixtureStatus.HalfTime };

            // act
            controller.UpdateFixtureStatus(id, model);

            // asset
            controller.WithCallTo(c => c.UpdateFixtureStatus(id, model))
                .ShouldRedirectTo<ScoresController>(sc => sc.AvailableScores());
        }
    }
}
