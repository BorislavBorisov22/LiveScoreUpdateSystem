using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Models;
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
            var availableScores = this.fixtureService
                .GetAvailableFixtures(currentDate)
                .ToList()
                .Map<Fixture, AvailableScoreViewModel>()
                .GroupBy(s => s.LeagueName);

            return this.View(availableScores);
        }
    }
}