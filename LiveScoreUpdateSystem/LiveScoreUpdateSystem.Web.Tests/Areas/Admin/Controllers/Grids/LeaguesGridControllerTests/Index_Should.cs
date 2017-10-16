using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
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
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenInvoked()
        {
            // arrange
            var leagueService = new Mock<ILeagueService>();
            var controller = new LeaguesGridController(leagueService.Object);

            // act & assert
            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
