using Bytes2you.Validation;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Controllers;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly ICountryService countryService;
        private readonly ILeagueService leaguesService;
        private readonly IMappingService mappingService;

        public PanelController(ICountryService countriesService, ILeagueService leaguesService, IMappingService mappingService)
        {
            Guard.WhenArgument(countriesService, "CountriesService").IsNull().Throw();
            Guard.WhenArgument(leaguesService, "LeaguesService").IsNull().Throw();
            Guard.WhenArgument(mappingService, "Mapping Service").IsNull().Throw();

            this.countryService = countriesService;
            this.mappingService = mappingService;
            this.leaguesService = leaguesService;
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
                    .Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Name
                    });

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
                var leagueDataModel = this.mappingService.Map<League>(leagueModel);
                this.leaguesService.Add(leagueDataModel);
            }

            return this.RedirectToAction(action => action.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddTeam()
        {
            return this.PartialView(PartialViews.AddTeam);
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
                var mappedCountry = this.mappingService.Map<Country>(countryModel);
                this.countryService.Add(mappedCountry);

                return this.RedirectToAction(c => c.Index());
            }

            return this.RedirectToAction<HomeController>(c => c.Index());
        }
    }
}