using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IUserService
    {
        void SubscribeUserForTeamResults(string username, IEnumerable<string> teamsNames);

        IEnumerable<Team> GetUserSubscriptions(string username);

        void RemoveSubscription(string username, string teamName);
    }
}
