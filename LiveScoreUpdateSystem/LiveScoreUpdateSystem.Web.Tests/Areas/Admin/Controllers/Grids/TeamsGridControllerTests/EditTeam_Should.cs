using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
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

namespace LiveScoreUpdateSystem.Web.Tests.Areas.Admin.Grids.TeamsGridControllerTests
{

    [TestFixture]
    public class EditTeam_Should
    {
        [Test]
        public void CallTeamServiceUpdateMethodWithCorrectParametersDateModel_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var teamViewModel = new GridTeamViewModel() { Name = "someName", Id = Guid.NewGuid(), LogoUrl = "SomeLogo" };

            var mapService = new Mock<IMappingService>(); 
            var controller = new TeamsGridController(teamService.Object);

            // act
            controller.EditTeam(teamViewModel);

            // assert
            teamService.Verify(c => c.Update(teamViewModel.Id, teamViewModel.Name, teamViewModel.LogoUrl), Times.Once);
        }

        [Test]
        public void ReturnJsonArrayWithTheEditedTeam_WhenPassedModelParamIsNotNull()
        {
            // arrange
            var teamService = new Mock<ITeamService>();
            var teamViewModel = new GridTeamViewModel() { Name = "someName" };

            var mapService = new Mock<IMappingService>();

            var teamDataModel = new Team() { Name = "someName" };
            mapService.Setup(c => c.Map<Team>(It.IsAny<Object>()))
                .Returns(teamDataModel);

            MappingService.MappingProvider = mapService.Object;
            var controller = new TeamsGridController(teamService.Object);

            // act
            controller.EditTeam(teamViewModel);

            // assert
            controller.WithCallTo(c => c.EditTeam(teamViewModel))
                .ShouldReturnJson((data) => Assert.AreSame(data[0], teamViewModel));
        }
    }
}
