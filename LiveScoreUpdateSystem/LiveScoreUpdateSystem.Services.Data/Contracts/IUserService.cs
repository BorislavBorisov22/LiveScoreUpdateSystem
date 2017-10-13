using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IUserService
    {
        void SubscribeUserForTeamResults(string username, IEnumerable<string> teamsNames);
    }
}
