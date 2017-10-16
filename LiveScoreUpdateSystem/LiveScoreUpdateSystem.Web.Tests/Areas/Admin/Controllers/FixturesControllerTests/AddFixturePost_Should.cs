using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.FixturesControllerTests
{
    [TestFixture]
    public class AddFixturePost_Should
    {
        [Test]
        public void CallFixtureServiceAddMethodWithCorrectModelParameter_WhenModelIsInValidState()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var addFixtureViewModel = new AddFixtureViewModel()
            {
                HomeTeamName = "Milan",
                AwayTeamName = "Lazio",
                StartTime = new System.DateTime(2012, 3, 3),
            };

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            // act
            controller.AddFixture(addFixtureViewModel);

            // assert
            fixtureService.Verify(f => f.Add(addFixtureViewModel.HomeTeamName, addFixtureViewModel.AwayTeamName, addFixtureViewModel.StartTime), Times.Once);
        }

        [Test]
        public void RedicrectToPanelControllerIndex_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var leagueService = new Mock<ILeagueService>();
            var fixtureService = new Mock<IFixtureService>();

            var addFixtureViewModel = new AddFixtureViewModel()
            {
                HomeTeamName = "Milan",
                AwayTeamName = "Lazio",
                StartTime = new System.DateTime(2012, 3, 3),
            };

            var controller = new FixturesController(leagueService.Object, teamService.Object, fixtureService.Object);

            // act
            controller.AddFixture(addFixtureViewModel);

            // assert
            controller.WithCallTo(c => c.AddFixture(addFixtureViewModel))
                .ShouldRedirectTo<PanelController>(c => c.Index());
        }
    }
}
