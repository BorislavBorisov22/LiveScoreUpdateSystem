using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Models;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamService teamService;
 
        public TeamsController(ITeamService teamService)
        {
            Guard.WhenArgument(teamService, "teamService").IsNull().Throw();

            this.teamService = teamService;
        }

        public ActionResult TeamsList(int page = 1, int size = 20)
        {
            var teams = this.teamService
                .GetAll()
                .Map<Team, ListTeamsViewModel>();

            return this.View(teams); 
        }
    }
}