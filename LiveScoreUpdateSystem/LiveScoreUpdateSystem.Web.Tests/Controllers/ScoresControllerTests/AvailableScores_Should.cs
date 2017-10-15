using NUnit.Framework;
using Moq;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Common;
using TestStack.FluentMVCTesting;
using System;

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.ScoresControllerTests
{
    [TestFixture]
    public class AvailableScores_Should
    {
        [Test]
        public void CallDefaultShouldRenderDefaultView_WhenInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            var date = new DateTime(2012, 12, 12);
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.Setup(t => t.CurrentDate).Returns(date);

            TimeProvider.CurrentProvider = timeProvider.Object;
            var scoresController = new ScoresController(fixtureService.Object);

            // act
            scoresController.AvailableScores();

            // assert
            scoresController.WithCallTo(c => c.AvailableScores())
                .ShouldRenderDefaultView();
        }
    }
}
