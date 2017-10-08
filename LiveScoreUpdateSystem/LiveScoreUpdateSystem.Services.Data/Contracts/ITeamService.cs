using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface ITeamService
    {
        void Add(Team team, string leagueName);

        IEnumerable<Team> GetAll();

        IEnumerable<IGrouping<string, Team>> GroupTeamsByLeague();
        void Delete(Guid id);
    }
}
