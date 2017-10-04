using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using System.Web.Mvc.Expressions;
using System.Web.Mvc;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using Bytes2you.Validation;
using LiveScoreUpdateSystem.Services.Common.Contracts;

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
        public ActionResult AddCountry()
        {
            return this.PartialView(PartialViews.AddCountry);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCountry(CountryViewModel countryModel)
        {
            this.countriesService.GetAll();
            return this.RedirectToAction(c => c.Index());
        }
    }
}