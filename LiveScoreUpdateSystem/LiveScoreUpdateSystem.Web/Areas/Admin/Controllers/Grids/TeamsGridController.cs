using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids
{
    public class TeamsGridController : AdminController
    {
        private readonly ITeamService teamService;

        public TeamsGridController(ITeamService teamService)
        {
            Guard.WhenArgument(teamService, "teamService").IsNull().Throw();

            this.teamService = teamService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadTeams([DataSourceRequest] DataSourceRequest request)
        {
            var teams = this.teamService
                .GetAll()
                .Map<Team, GridTeamViewModel>()
                .ToDataSourceResult(request);

            return this.Json(teams);
        }

        public ActionResult DeleteTeam([DataSourceRequest] DataSourceRequest request, GridTeamViewModel teamModel)
        {
            if (teamModel != null)
            {
                this.teamService.Delete(teamModel.Id);
            }

            return this.Json(new[] { teamModel }.ToDataSourceResult(request, ModelState));
        }
    }
}