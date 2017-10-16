using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
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
    public class AddFixtureForm_Should
    {
        [Test]
        public void CallTeamServiceGetByLeaguNameWithValidTheCorrectLeagueName_WhenPassedLeagueNameParamIsValid()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var teams = new List<Team>() { new Team() { Name = "someName" } };
            teamService.Setup(t => t.GetTeamsByLeague("someName")).Returns(teams);

            // act 
            controller.AddFixtureForm("someName");

            // assert
            teamService.Verify(t => t.GetTeamsByLeague("someName"), Times.Once);
        }

        [Test]
        public void ReturnCorrectPartialView_WhenPassedLeagueNameParamIsValid()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var teams = new List<Team>() { new Team() { Name = "someName" } };
            teamService.Setup(t => t.GetTeamsByLeague("someName")).Returns(teams);

            // act 
            controller.AddFixtureForm("someName");

            // assert
            controller.WithCallTo(c => c.AddFixtureForm("someName"))
                .ShouldRenderPartialView(PartialViews.AddFixtureFormPartial);
        }

        [Test]
        public void PasCorrectViewModelToPartialView_WhenPassedLeagueNameParamIsValid()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            var teams = new List<Team>() { new Team() { Name = "someName" } };
            teamService.Setup(t => t.GetTeamsByLeague("someName")).Returns(teams);

            // act 
            controller.AddFixtureForm("someName");

            // assert
            controller.WithCallTo(c => c.AddFixtureForm("someName"))
                .ShouldRenderPartialView(PartialViews.AddFixtureFormPartial)
                .WithModel<AddFixtureViewModel>(m => m.TeamsNames.Contains("someName"));
        }
    }
}
