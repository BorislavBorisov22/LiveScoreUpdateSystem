using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.PlayersGridControllerTests
{

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenInvoked()
        {
            // arrange
            var playerService = new Mock<IPlayerService>();
            var controller = new PlayersGridController(playerService.Object);

            // act & assert
            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
