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
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnlyAttribute]
        public ActionResult AddCountry()
        {
            return this.PartialView(PartialViews.AddCountry);
        }
         
        [HttpPost]
        public ActionResult AddCountry(CountryViewModel countryModel)
        {
            return this.RedirectToAction(c => c.Index());
        }
    }
}