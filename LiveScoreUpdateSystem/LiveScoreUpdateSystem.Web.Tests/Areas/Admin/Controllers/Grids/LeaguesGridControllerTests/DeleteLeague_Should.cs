using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.LeaguesGridControllerTests
{
    [TestFixture]
    public class DeleteLeague_Should
    {
        [Test]
        public void CallLeagueServiceDeleteMethodWithCorrectLeagueName_WhenPassedModelIsNotNull()
        {
            // arrange
            var countryService = new Mock<ILeagueService>();
            var controller = new LeaguesGridController(countryService.Object);

            var leagueViewModel = new GridLeagueViewModel() { Name = "someName" };

            // act
            controller.DeleteLeague(leagueViewModel);

            // assert
            countryService.Verify(c => c.Delete("someName"), Times.Once);
        }

        [Test]
        public void ReturnJsonContainingRemovedLeagueAsResult_WhenInvoked()
        {
            // arrange
            var leagueService = new Mock<ILeagueService>();
            var controller = new LeaguesGridController(leagueService.Object);

            var leagueViewModel = new GridLeagueViewModel() { Name = "someName" };

            // act
            controller.DeleteLeague(leagueViewModel);

            // assert
            controller.WithCallTo(c => c.DeleteLeague(leagueViewModel))
                .ShouldReturnJson(data =>
                {
                    Assert.That(data[0].Name, Is.EqualTo("someName"));
                });
        }
    }
}
