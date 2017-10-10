using Bytes2you.Validation;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System.Linq;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class FixturesController : AdminController
    {
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;

        public FixturesController(ILeagueService leagueService, ITeamService teamService)
        {
            Guard.WhenArgument(leagueService, "leagueService").IsNull().Throw();
            Guard.WhenArgument(teamService, "teamService").IsNull().Throw();

            this.teamService = teamService;
            this.leagueService = leagueService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddFixture()
        {
            var leaguesAvailable = this.leagueService
                .GetAll()
                .Select(l => new SelectListItem() { Text = l.Name, Value = l.Name });

            return this.PartialView(PartialViews.AddFixtureForUpdate, leaguesAvailable);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTeams(string leagueName)
        {
            var teams = this.teamService.GetTeamsByLeague(leagueName);

            return this.Content(string.Join("", teams));
        }
    }
}