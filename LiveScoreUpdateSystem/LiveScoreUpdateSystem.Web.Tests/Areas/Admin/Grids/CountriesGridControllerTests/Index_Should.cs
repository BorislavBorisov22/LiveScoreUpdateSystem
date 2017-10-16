using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.CountriesGridControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultViewWhenInvoked()
        {
            // arrange
            var countryService = new Mock<ICountryService>();
            var controller = new CountriesGridController(countryService.Object);

            // act & assert
            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
