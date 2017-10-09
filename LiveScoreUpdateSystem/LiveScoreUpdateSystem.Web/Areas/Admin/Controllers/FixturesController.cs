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

        public FixturesController(ILeagueService leagueService)
        {
            Guard.WhenArgument(leagueService, "leagueService").IsNull().Throw();

            this.leagueService = leagueService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddFixture()
        {
            var leaguesAvailable = this.leagueService
                .GetAll()
                .Select(l => new SelectListItem() { Text = l.Name, Value = l.Name });

            var fixtureViewModel = new FixtureViewModel() { LeaguesAvailable = leaguesAvailable };

            return this.PartialView(PartialViews.AddFixtureForUpdate, fixtureViewModel);
        }

        public ActionResult GetTeams(string leagueName)
        {
            var teams = this.teamService.GetTeamsByLeague(leagueName);

            return this.Json(teams);
        }
    }
}