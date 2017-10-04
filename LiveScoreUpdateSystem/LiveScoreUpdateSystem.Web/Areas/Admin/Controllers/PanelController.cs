using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using System.Web.Mvc.Expressions;
using System.Web.Mvc;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using Bytes2you.Validation;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Web.Controllers;
using System.Linq;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly ICountriesService countriesService;
        private readonly IMappingService mappingService;

        public PanelController(ICountriesService countriesService, IMappingService mappingService)
        {
            Guard.WhenArgument(countriesService, "CountriesService").IsNull().Throw();
            Guard.WhenArgument(mappingService, "Mapping Service").IsNull().Throw();

            this.countriesService = countriesService;
            this.mappingService = mappingService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddLeague()
        {
            var countriesList = this.countriesService.GetAll()
                    .Select(c => new SelectListItem() { Text = c.Name, Value = c.Name});

            var leagueViewModel = new LeagueViewModel()
            {
                CountriesSelectList = countriesList 
            };

            return this.PartialView(PartialViews.AddLeague, leagueViewModel);
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
                this.countriesService.Add(mappedCountry);

                return this.RedirectToAction(c => c.Index());
            }

            return this.RedirectToAction<HomeController>(c => c.Index());
        }
    }
}