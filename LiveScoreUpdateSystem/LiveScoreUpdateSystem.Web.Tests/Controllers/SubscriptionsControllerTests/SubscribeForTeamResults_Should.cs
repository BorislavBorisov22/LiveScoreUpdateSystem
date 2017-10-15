using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.SubscriptionsControllerTests
{
    [TestFixture]
    public class SubscribeForTeamResults_Should
    {
        [Test]
        public void RedirectToIndexAction_WhenPassedTeamsNamesAreNull()
        {
            // arrange
            var userService = new Mock<IUserService>();
            var subscriptionsController = new SubscriptionsController(userService.Object);

            // act 
            subscriptionsController.SubscribeForTeamResults(null);

            // assert
            subscriptionsController.WithCallTo(c => c.SubscribeForTeamResults(null))
                .ShouldRedirectTo(c => c.Index());
        }

        [Test]
        public void CallUserServiceSubscribeForTeamsResultsMethod_WhenPassedTeamsNamesIsNotNull()
        {
            // arrange
            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var userService = new Mock<IUserService>();
            var subscriptionsController = new SubscriptionsController(userService.Object);

            var controller = new SubscriptionsController(userService.Object);
            controller.ControllerContext = contextMock.Object;

            var teams = new List<string>() { "someName"};

            // act
            controller.SubscribeForTeamResults(teams);

            // assert
            userService.Verify(u => u.SubscribeUserForTeamResults("username", teams), Times.Once);
        }
    }
}
