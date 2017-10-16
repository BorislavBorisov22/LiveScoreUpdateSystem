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

namespace LiveScoreUpdateSystem.Web.Tests.Controllers.TeamsControllerTests
{
    [TestFixture]
    public class TeamsList_Should
    {
        [Test]
        public void CallTeamServiceGetAllMethodOnce_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();

            var mappingService = new Mock<IMappingService>();
            mappingService.Setup(m => m.Map<ListTeamsViewModel>(It.IsAny<Object>()))
                .Returns(new ListTeamsViewModel());

            MappingService.MappingProvider = mappingService.Object;

            teamService.Setup(t => t.GetAll()).Returns(new List<Team>());
            var teamsController = new TeamsController(teamService.Object);

            // act
            teamsController.TeamsList();

            // assert
            teamService.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void RenderDefaultView_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();

            var mappingService = new Mock<IMappingService>();
            mappingService.Setup(m => m.Map<ListTeamsViewModel>(It.IsAny<Object>()))
                .Returns(new ListTeamsViewModel());

            MappingService.MappingProvider = mappingService.Object;

            teamService.Setup(t => t.GetAll()).Returns(new List<Team>());
            var teamsController = new TeamsController(teamService.Object);

            // act
            teamsController.TeamsList();

            // assert
            teamsController.WithCallTo(c => c.TeamsList(1, 1))
                 .ShouldRenderDefaultView();
        }

        [Test]
        public void PassCorrectTeamsCollection_WhenInvoked()
        {
            // arrange
            var teamService = new Mock<ITeamService>();

            var mappingService = new Mock<IMappingService>();
            mappingService.Setup(m => m.Map<ListTeamsViewModel>(It.IsAny<Object>()))
                .Returns(new ListTeamsViewModel());

            MappingService.MappingProvider = mappingService.Object;

            teamService.Setup(t => t.GetAll()).Returns(new List<Team>());
            var teamsController = new TeamsController(teamService.Object);

            // act
            teamsController.TeamsList();

            // assert
            teamsController.WithCallTo(c => c.TeamsList(1, 1))
                 .ShouldRenderDefaultView()
                 .WithModel<IEnumerable<ListTeamsViewModel>>();
        }
    }
}
