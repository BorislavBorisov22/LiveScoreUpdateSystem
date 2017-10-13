using Bytes2you.Validation;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Models;
using LiveScoreUpdateSystem.Web.Controllers;
using System.Web.Mvc.Expressions;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers
{
    public class UpdateFixtureController : LiveUpdaterController
    {
        private readonly IFixtureService fixtureService;
        private readonly IPlayerService playerService;

        public UpdateFixtureController(IFixtureService fixtureService, IPlayerService playerService)
        {
            Guard.WhenArgument(fixtureService, "fixtureService").IsNull().Throw();
            Guard.WhenArgument(playerService, "playerService").IsNull().Throw();

            this.fixtureService = fixtureService;
            this.playerService = playerService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Update(string teamName, Guid fixtureId)
        {
            var targetFixture = this.fixtureService.GetById(fixtureId);
            var targetTeam = targetFixture.HomeTeam.Name == teamName ?
                targetFixture.HomeTeam
                : targetFixture.AwayTeam;

            var playerModels = this.playerService.GetAll(targetTeam.Id)
                .Map<Player, UpdatingFixturePlayerViewModel>()
                .Select(p =>
                    new SelectListItem() { Text = string.Format("{0}. {1} {2}", p.ShirtNumber, p.FirstName, p.LastName), Value = p.Id.ToString() });

            var fixtureViewModel = new UpdateFixtureViewModel()
            {
                Players = playerModels,
                Id = fixtureId,
                TeamId = targetTeam.Id,
                TeamName = targetTeam.Name,
            };

            return this.PartialView(PartialViews.UpdateFixtureFormPartial, fixtureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Guid fixtureId, UpdateFixtureViewModel fixtureModel)
        {
            this.fixtureService
                .Update(fixtureId, fixtureModel.FixtureEvent, fixtureModel.Minute, fixtureModel.PlayerId);

            this.TempData[GlobalConstants.SuccessMessage] = "Fixture has been updated!";
            return this.RedirectToAction<ScoresController>(c => c.AvailableScores());
        }
    }
}