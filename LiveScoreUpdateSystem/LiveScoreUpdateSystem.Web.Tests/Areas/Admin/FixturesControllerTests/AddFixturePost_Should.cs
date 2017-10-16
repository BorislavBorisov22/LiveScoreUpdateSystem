using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.FixturesControllerTests
{
    [TestFixture]
    public class AddFixture_Should
    {

        [Test]
        public void CallLeageServiceGetAllMethodOnce_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var leagues = new List<League>() { new League() { Name = "some" } };
            leagueService.Setup(l => l.GetAll()).Returns(leagues);

            // act 
            controller.AddFixture();

            // assert
            leagueService.Verify(l => l.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnCorrectPartialView_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var leagues = new List<League>() { new League() { Name = "some" } };
            leagueService.Setup(l => l.GetAll()).Returns(leagues);

            // act 
            controller.AddFixture();

            // assert
            controller.WithCallTo(c => c.AddFixture())
                .ShouldRenderPartialView(PartialViews.AddFixtureForUpdate);
        }

        [Test]
        public void PassValidModelToPartialView_WhenInoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var leagues = new List<League>() { new League() { Name = "some" } };
            leagueService.Setup(l => l.GetAll()).Returns(leagues);

            // act 
            controller.AddFixture();

            // assert
            controller.WithCallTo(c => c.AddFixture())
                .ShouldRenderPartialView(PartialViews.AddFixtureForUpdate)
                .WithModel<IEnumerable<string>>(m => m.Contains("some"));
        }
    }
}
