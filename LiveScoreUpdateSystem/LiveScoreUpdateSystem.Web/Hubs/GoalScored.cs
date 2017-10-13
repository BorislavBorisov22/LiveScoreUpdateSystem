using LiveScoreUpdateSystem.Services.Data.Contracts;
using Microsoft.AspNet.SignalR;

namespace LiveScoreUpdateSystem.Web.Hubs
{
    public class GoalScored : Hub
    {
        private readonly IFixtureService fixtureService;

        public GoalScored(IFixtureService fixtureService)
        {
            this.fixtureService = fixtureService;
        }

        public void NotifyGoalScored(string message)
        {
            this.Clients.All.recieveGoalNotification();
        }
    }
}