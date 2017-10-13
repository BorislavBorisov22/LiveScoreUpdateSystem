using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IFixtureService
    {
        IEnumerable<Fixture> GetAll();

        IEnumerable<Fixture> GetAvailableFixtures(DateTime targetDate);

        Fixture GetById(Guid id);

        void Add(string homeTeamName, string awayTeamName, DateTime? startTime);

        void AddFixtureEvent(Guid fixtureId, FixtureEventType fixtureEventType, int minute, Guid playerId);
    }
}
