using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.LiveUpdater.Controllers.UpdateFixtureControllerTests
{
    [TestFixture]
    public class UpdateFixtureEventGet_Should
    {
        [Test]
        public void RenderCorrectPartialView_WhenMethodIsInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var targetFixture = new Fixture()
            {
                HomeTeam = new Team() { Name = "someName" },
                Id = Guid.NewGuid(),
            };

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>())).Returns(targetFixture);
            playerService.Setup(p => p.GetAll(It.IsAny<Guid>())).Returns(new List<Player>());

            var mapSerivce = new Mock<IMappingService>();
            mapSerivce.Setup(m => m.Map<UpdatingFixturePlayerViewModel>(It.IsAny<Object>()))
                .Returns(new UpdatingFixturePlayerViewModel());

            MappingService.MappingProvider = mapSerivce.Object;

            // act
            controller.UpdateFixtureEvent("someName", targetFixture.Id);

            // assert
            controller.WithCallTo(c => c.UpdateFixtureEvent("someName", targetFixture.Id))
                .ShouldRenderPartialView(PartialViews.UpdateFixtureFormPartial);

        }

        [Test]
        public void PassCorrectViewModelToPartialView_WhenEverythinHasPassedSuccessfully()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var targetFixture = new Fixture()
            {
                HomeTeam = new Team() { Name = "someName" },
                Id = Guid.NewGuid(),
            };

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>())).Returns(targetFixture);
            playerService.Setup(p => p.GetAll(It.IsAny<Guid>())).Returns(new List<Player>());

            var mapSerivce = new Mock<IMappingService>();
            mapSerivce.Setup(m => m.Map<UpdatingFixturePlayerViewModel>(It.IsAny<Object>()))
                .Returns(new UpdatingFixturePlayerViewModel());

            MappingService.MappingProvider = mapSerivce.Object;

            // act
            controller.UpdateFixtureEvent("someName", targetFixture.Id);

            // assert
            controller.WithCallTo(c => c.UpdateFixtureEvent("someName", targetFixture.Id))
                .ShouldRenderPartialView(PartialViews.UpdateFixtureFormPartial)
                .WithModel<UpdateFixtureViewModel>(m => m.TeamId == targetFixture.HomeTeam.Id);

        }

        [Test]
        public void CallFixtureServiceGetByIdOnce_WhenPassedParametersAreValid()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var targetFixture = new Fixture()
            {
                HomeTeam = new Team() { Name = "someName" },
                Id = Guid.NewGuid(),
            };

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>())).Returns(targetFixture);
            playerService.Setup(p => p.GetAll(It.IsAny<Guid>())).Returns(new List<Player>());

            var mapSerivce = new Mock<IMappingService>();
            mapSerivce.Setup(m => m.Map<UpdatingFixturePlayerViewModel>(It.IsAny<Object>()))
                .Returns(new UpdatingFixturePlayerViewModel());

            MappingService.MappingProvider = mapSerivce.Object;

            // act
            controller.UpdateFixtureEvent("someName", targetFixture.Id);

            // assert
            fixtureService.Verify(f => f.GetById(targetFixture.Id), Times.Once);
        }


        [Test]
        public void CallPlayerServiceGetAllOnceWithCorrectId_WhenPassedParametersAreValid()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();
            var playerService = new Mock<IPlayerService>();

            var controller = new UpdateFixtureController(fixtureService.Object, playerService.Object);

            var targetFixture = new Fixture()
            {
                HomeTeam = new Team() { Name = "someName" },
                Id = Guid.NewGuid(),
            };

            fixtureService.Setup(f => f.GetById(It.IsAny<Guid>())).Returns(targetFixture);
            playerService.Setup(p => p.GetAll(It.IsAny<Guid>())).Returns(new List<Player>());

            var mapSerivce = new Mock<IMappingService>();
            mapSerivce.Setup(m => m.Map<UpdatingFixturePlayerViewModel>(It.IsAny<Object>()))
                .Returns(new UpdatingFixturePlayerViewModel());

            MappingService.MappingProvider = mapSerivce.Object;

            // act
            controller.UpdateFixtureEvent("someName", targetFixture.Id);

            // assert
            playerService.Verify(f => f.GetAll(targetFixture.HomeTeam.Id), Times.Once);
        }
    }
}
