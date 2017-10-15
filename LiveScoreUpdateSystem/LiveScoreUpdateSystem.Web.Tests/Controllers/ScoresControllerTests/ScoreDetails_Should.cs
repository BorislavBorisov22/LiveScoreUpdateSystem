using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers;
using LiveScoreUpdateSystem.Web.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.ScoresControllerTests
{
    [TestFixture]
    public class ScoreDetails_Should
    {
        [Test]
        public void CallFixtureServiceGetByIdMethodWithCorrectIdParameter_WhenInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>()));

            var scoresController = new ScoresController(fixtureService.Object);
            var guid = Guid.NewGuid();

            // act
            scoresController.ScoreDetails(guid);

            // assert
            fixtureService.Verify(s => s.GetById(guid), Times.Once);
        }


        [Test]
        public void RedirectToAvalableScoresAction_WhenPassedIdDoesNotTargetAnyFixture()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>()));

            var scoresController = new ScoresController(fixtureService.Object);
            var guid = Guid.NewGuid();

            // act
            scoresController.ScoreDetails(guid);

            // assert
            scoresController
                .WithCallTo(c => c.ScoreDetails(guid))
                .ShouldRedirectTo<ScoresController>(c => c.AvailableScores());
        }

        [Test]
        public void ShouldRenderCorrectViewModel_WhenPassedIdMatchesFixture()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            var fixture = new Fixture();
            var mappingService = new Mock<IMappingService>();

            var scoreDetailsViewModel = new ScoreDetailsViewModel()
            {
                FixtureEvents = new List<FixtureEventViewModel>() { new FixtureEventViewModel() { Minute = 1 } }
            };

            mappingService.Setup(m => m.Map<ScoreDetailsViewModel>(It.IsAny<Object>()))
                .Returns(scoreDetailsViewModel);

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>()))
                .Returns(fixture);

            MappingService.MappingProvider = mappingService.Object;
            var scoresController = new ScoresController(fixtureService.Object);
            var guid = Guid.NewGuid();

            // act
            scoresController.ScoreDetails(guid);

            // assert
            scoresController
                .WithCallTo(c => c.ScoreDetails(guid))
                .ShouldRenderDefaultView()
                .WithModel<ScoreDetailsViewModel>(m => m == scoreDetailsViewModel);
        }
    }
}
