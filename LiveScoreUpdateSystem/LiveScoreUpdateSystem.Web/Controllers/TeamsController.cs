using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly IUserService userService;

        public TeamsController(ITeamService teamService, IUserService userService)
        {
            Guard.WhenArgument(teamService, "teamService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.teamService = teamService;
            this.userService = userService;
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