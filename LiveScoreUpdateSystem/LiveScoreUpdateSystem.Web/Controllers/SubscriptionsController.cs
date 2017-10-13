using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using LiveScoreUpdateSystem.Web.Infrastructure.Extensions;
using LiveScoreUpdateSystem.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    [Authorize]
    public class SubscriptionsController : BaseController
    {
        private readonly IUserService userService;

        public SubscriptionsController(IUserService userService)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.userService = userService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubscribeForTeamResults(IEnumerable<string> teamName)
        {
            if (teamName != null)
            {
                this.userService.SubscribeUserForTeamResults(this.User.Identity.Name, teamName);
            }

            return this.RedirectToAction<SubscriptionsController>(c => c.Index());
        }

        [AjaxOnly]
        public ActionResult RemoveSubscription(TeamSubscriptionViewModel subscriptionToRemove)
        {
            if (subscriptionToRemove != null)
            {
                this.userService.RemoveSubscription(this.User.Identity.Name, subscriptionToRemove.Name);
            }

            return this.Json(new[] { subscriptionToRemove });
        }

        public ActionResult ReadSubscriptions([DataSourceRequest] DataSourceRequest request)
        {
            var subscriptions = this.userService
                .GetUserSubscriptions(this.User.Identity.Name)
                .ToList()
                .Map<Team, TeamSubscriptionViewModel>()
                .ToDataSourceResult(request);

            return this.Json(subscriptions);
        }
    }
}