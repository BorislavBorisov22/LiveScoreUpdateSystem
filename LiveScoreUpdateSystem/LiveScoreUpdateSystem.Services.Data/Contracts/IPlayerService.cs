using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;
using System;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IPlayerService
    {
        void Add(Player playerToAdd, string teamName, string countryName);

        IEnumerable<Player> GetAll();

        IEnumerable<Player> GetAll(Guid teamId);

        void Delete(Guid id);

        void Update(Player updatedPlayer);
    }
}
