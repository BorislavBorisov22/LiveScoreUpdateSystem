using Bytes2you.Validation;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Infrastructure.Providers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly ICountryService countryService;
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;

        public PanelController(
            ICountryService countriesService,
            ILeagueService leaguesService,
            ITeamService teamService,
            IPlayerService playerService)
        {
            Guard.WhenArgument(countriesService, "CountriesService").IsNull().Throw();
            Guard.WhenArgument(leaguesService, "LeaguesService").IsNull().Throw();
            Guard.WhenArgument(teamService, "TeamService").IsNull().Throw();
            Guard.WhenArgument(playerService, "PlayerService").IsNull().Throw();

            this.countryService = countriesService;
            this.leagueService = leaguesService;
            this.teamService = teamService;
            this.playerService = playerService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddLeague()
        {
            var countriesList = this.countryService.GetAll()
                    .Select(c => new SelectListItem() { Text = c.Name, Value = c.Name });

            var leagueViewModel = new LeagueViewModel()
            {
                CountriesSelectList = countriesList
            };

            return this.PartialView(PartialViews.AddLeague, leagueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLeague(LeagueViewModel leagueModel)
        {
            if (ModelState.IsValid)
            {
                var leagueDataModel = MappingService.MappingProvider.Map<League>(leagueModel);
                this.leagueService.Add(leagueDataModel);
            }

            return this.RedirectToAction(action => action.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddTeam()
        {
            var leaguesSelectList = this.leagueService
                .GetAll()
                .Select(l => new SelectListItem() { Text = l.Name, Value = l.Name });

            var leagueViewModel = new TeamViewModel()
            {
                LeaguesSelectList = leaguesSelectList
            };

            return this.PartialView(PartialViews.AddTeam, leagueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeam(TeamViewModel teamModel)
        {
            if (ModelState.IsValid)
            {
                var teamDataModel = MappingService.MappingProvider.Map<Team>(teamModel);
                this.teamService.Add(teamDataModel, teamModel.LeagueName);
            }

            return this.RedirectToAction(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddPlayer()
        {
            var countriesList = this.countryService
                                .GetAll()
                                .Select(c => new SelectListItem() { Text = c.Name, Value = c.Name })
                                .ToList();

            var groupedTeams = this.teamService.GetAll()
                .Map<Team, TeamViewModel>()
                .ToList()
                .GroupBy(t => t.LeagueName);

            var playerViewModel = new PlayerViewModel()
            {
                CountriesList = countriesList,
                LeagueGroupedTeams = groupedTeams
            };

            return this.PartialView(PartialViews.AddPlayer, playerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlayer(PlayerViewModel playerModel)
        {
            if (ModelState.IsValid)
            {
                var playerDataModel = MappingService.MappingProvider.Map<Player>(playerModel);
                this.playerService.Add(playerDataModel, playerModel.TeamName, playerModel.CountryName);
            }

            return this.RedirectToAction(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddCountry()
        {
            return this.PartialView(PartialViews.AddCountry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountry(CountryViewModel countryModel)
        {
            if (this.ModelState.IsValid)
            {
                var mappedCountry = MappingService.MappingProvider.Map<Country>(countryModel);
                this.countryService.Add(mappedCountry);

                return this.RedirectToAction(c => c.Index());
            }

            return this.RedirectToAction(c => c.Index());
        }
    }
}