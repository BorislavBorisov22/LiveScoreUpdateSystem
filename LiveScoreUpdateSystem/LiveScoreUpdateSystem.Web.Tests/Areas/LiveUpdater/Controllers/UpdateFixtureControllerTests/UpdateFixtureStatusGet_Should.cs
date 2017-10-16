using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.LiveUpdater.Controllers.UpdateFixtureControllerTests
{
    [TestFixture]
    public class UpdateFixtureStatusGet_Should
    {
        [Test]
        public void CallTheCorrectPartialView_WhenInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();

            // act
            controller.UpdateFixtureStatus(id);

            // assert
            controller.WithCallTo(c => c.UpdateFixtureStatus(id))
                .ShouldRenderPartialView(PartialViews.UpdateFixtureStatusPartial);
        }

        [Test]
        public void PassViewModelWithTheSameParameterId_WhenInvokedWithValidIdParam()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var id = Guid.NewGuid();

            // act
            controller.UpdateFixtureStatus(id);

            // assert
            controller.WithCallTo(c => c.UpdateFixtureStatus(id))
                .ShouldRenderPartialView(PartialViews.UpdateFixtureStatusPartial)
                .WithModel<UpdateFixtureStatusViewModel>(m => m.Id == id);
        }
    }
}
