using Bytes2you.Validation;
using Microsoft.AspNet.SignalR;

namespace LiveScoreUpdateSystem.Web.Hubs
{
    public class GoalScored : Hub
    {
        public void NotifyGoalScored(string fixtureId, string scoringTeamName)
        {
            Guard.WhenArgument(fixtureId, "fixtureId").IsNull().Throw();
            Guard.WhenArgument(scoringTeamName, "scoringTeamName").IsNull().Throw();

            this.Clients.Others.recieveGoalNotification(fixtureId, scoringTeamName);
        }
    }
}