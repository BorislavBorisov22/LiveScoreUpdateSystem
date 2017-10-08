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
    public class LeaguesGridController : AdminController
    {
        private readonly ILeagueService leagueService;

        public LeaguesGridController(ILeagueService leagueService)
        {
            Guard.WhenArgument(leagueService, "leagueSerice").IsNull().Throw();

            this.leagueService = leagueService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadLeagues([DataSourceRequest] DataSourceRequest request)
        {
            var leagues = this.leagueService
                 .GetAll()
                 .Map<League, GridLeagueViewModel>()
                 .ToDataSourceResult(request);

            return this.Json(leagues);
        }

        public ActionResult DeleteLeague([DataSourceRequest] DataSourceRequest request, GridLeagueViewModel leagueModel)
        {
            if (leagueModel != null)
            {
                string leagueName = leagueModel.Name;
                this.leagueService.Delete(leagueName);
            }

            return this.Json(new[] { leagueModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult EditLeague([DataSourceRequest] DataSourceRequest request, GridLeagueViewModel leagueModel)
        {
            if (leagueModel != null)
            {
                var leagueDataModel = MappingService.MappingProvider.Map<League>(leagueModel);
                this.leagueService.Update(leagueDataModel);
            }

            return this.Json(new object[] { leagueModel }.ToDataSourceResult(request, ModelState));
        }
    }
}