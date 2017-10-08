using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids
{
    public class CountriesGridController : AdminController
    {
        private readonly ICountryService countryService;

        public CountriesGridController(ICountryService countryService)
        {
            Guard.WhenArgument(countryService, "countryService").IsNull().Throw();

            this.countryService = countryService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadCountries([DataSourceRequest] DataSourceRequest request)
        {
            var countries = this.countryService
                .GetAll()
                .Map<Country, CountryViewModel>()
                .ToDataSourceResult(request);

            return this.Json(countries);
        }

        public ActionResult DeleteCountry([DataSourceRequest] DataSourceRequest request, CountryViewModel model)
        {
            if (model != null)
            {
                this.countryService.Delete(model.Name);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult EditCountry([DataSourceRequest] DataSourceRequest request, CountryViewModel model)
        {
            if (model != null)
            {
                var countryDataModel = MappingService.MappingProvider.Map<Country>(model);
                this.countryService.Update(countryDataModel);
            }

            return this.Json(new[] { request });
        }
    }
}