using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface ILeagueService
    {
        void Add(League league);

        IEnumerable<League> GetAll();
    }
}
