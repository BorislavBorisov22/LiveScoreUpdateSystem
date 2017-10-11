using Bytes2you.Validation;
using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    public class ScoresController : BaseController
    {
        private readonly IFixtureService fixtureService;

        public ScoresController(IFixtureService fixtureService)
        {
            Guard.WhenArgument(fixtureService, "fixtureService").IsNull().Throw();

            this.fixtureService = fixtureService;
        }

        public ActionResult AvailableScores()
        {
            var currentDate = TimeProvider.CurrentProvider.CurrentDate;
            return this.View(currentDate);
        }

        [HttpGet]
        public ActionResult ByDate(DateTime date = default(DateTime))
        {
            var targetDate = date == default(DateTime) ?
                TimeProvider.CurrentProvider.CurrentDate :
                date;

            var availableScores = this.fixtureService
             .GetAvailableFixtures(targetDate)
             .ToList()
             .Map<Fixture, AvailableScoreViewModel>()
             .GroupBy(s => s.LeagueName);

            return this.PartialView(PartialViews.AvailableScoresPartial, availableScores);
        }
    }
}