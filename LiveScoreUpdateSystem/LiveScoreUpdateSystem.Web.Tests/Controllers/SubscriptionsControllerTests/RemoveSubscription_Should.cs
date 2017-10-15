using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers;
using LiveScoreUpdateSystem.Web.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.SubscriptionsControllerTests
{
    [TestFixture]
    public class RemoveSubscription_Should
    {
        [Test]
        public void ReturnJsonArrayResult_WhenInvoked()
        {
            // arrange
            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var userService = new Mock<IUserService>();
            var subscriptionsController = new SubscriptionsController(userService.Object);

            // act
            subscriptionsController.RemoveSubscription(null);

            // assert
            subscriptionsController.WithCallTo(c => c.RemoveSubscription(null))
                .ShouldReturnJson();
        }

        [Test]
        public void CallUserServiceRemoveSubscriptionMethodWithValidUsernameAndTeamName_WhenInvokedWithValidModel()
        {
            // arrange
            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var userService = new Mock<IUserService>();

            var subscriptionsController = new SubscriptionsController(userService.Object);
            subscriptionsController.ControllerContext = contextMock.Object;

            // act
            subscriptionsController.RemoveSubscription(new TeamSubscriptionViewModel() { Name = "TeamName"});

            // assert
            userService.Verify(u => u.RemoveSubscription("username", "TeamName"), Times.Once);
        }
    }
}
