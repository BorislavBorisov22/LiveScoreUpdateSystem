using LiveScoreUpdateSystem.Common;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.ScoresControllerTests
{
    [TestFixture]
    public class ByDate_Should
    {
        [Test]
        public void CallFixtureServiceGetAvailableFixtureWithCorrectDateParameter_WhenInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            var date = new DateTime(2012, 12, 12);
            var scoresController = new ScoresController(fixtureService.Object);

            var mappingProvider = new Mock<IMappingService>();
            mappingProvider.Setup(c => c.Map<AvailableScoreViewModel>(It.IsAny<Object>())).Returns(new AvailableScoreViewModel() { LeagueName = "someName" });
            MappingService.MappingProvider = mappingProvider.Object;

            fixtureService.Setup(f => f.GetAvailableFixtures(date)).Returns(new List<Fixture>());

            // act
            scoresController.ByDate(date);

            // assert
            fixtureService.Verify(f => f.GetAvailableFixtures(date), Times.Once);
        }


        [Test]
        public void ReturnCorrectPartialViewWithModel_WhenInvoked()
        {
            // arrange
            var fixtureService = new Mock<IFixtureService>();

            var date = new DateTime(2012, 12, 12);
            var scoresController = new ScoresController(fixtureService.Object);

            var mappingProvider = new Mock<IMappingService>();
            mappingProvider.Setup(c => c.Map<AvailableScoreViewModel>(It.IsAny<Object>()))
                .Returns(new AvailableScoreViewModel() { LeagueName = "someName" });

            MappingService.MappingProvider = mappingProvider.Object;

            fixtureService.Setup(f => f.GetAvailableFixtures(date)).Returns(new List<Fixture>());

            // act
            scoresController.ByDate(date);

            // assert
            scoresController.WithCallTo(c => c.ByDate(date))
                .ShouldRenderPartialView(PartialViews.AvailableScoresPartial)
                .WithModel<IEnumerable<IGrouping<string, AvailableScoreViewModel>>>();
        }
    }
}
