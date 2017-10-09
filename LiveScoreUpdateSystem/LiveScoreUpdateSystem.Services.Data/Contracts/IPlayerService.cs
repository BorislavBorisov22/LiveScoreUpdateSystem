using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IPlayerService
    {
        void Add(Player playerToAdd, string teamName, string countryName);

        IEnumerable<Player> GetAll();
    }
}
