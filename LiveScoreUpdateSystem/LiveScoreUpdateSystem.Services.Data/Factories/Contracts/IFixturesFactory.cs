using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using System;

namespace LiveScoreUpdateSystem.Services.Data.Factories.Contracts
{
    public interface IFixturesFactory
    {
        Fixture GetFixture(Team homeTeam, Team awayTeam, DateTime? startingTime);

        FixtureEvent GetFixtureEvent(FixtureEventType fixtureEvent, int minute, Player involvedPlayer);
    }
}
