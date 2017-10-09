using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using System;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids
{
    public class PlayersGridController : AdminController
    {
        private readonly IPlayerService playerService;

        public PlayersGridController(IPlayerService playerService)
        {
            Guard.WhenArgument(playerService, "playerService").IsNull().Throw();

            this.playerService = playerService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadPlayers([DataSourceRequest] DataSourceRequest request)
        {
            var players = this.playerService
                .GetAll()
                .Map<Player, GridPlayerViewModel>()
                .ToDataSourceResult(request);

            return this.Json(players);
        }

        public ActionResult DeletePlayer(GridPlayerViewModel playerModel)
        {
            this.playerService.Delete(playerModel.Id);

            return this.Json(new[] { playerModel });
        }
    }
}