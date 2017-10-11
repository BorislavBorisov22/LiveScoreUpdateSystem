using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System;

namespace LiveScoreUpdateSystem.Services.Data.Factories.Contracts
{
    public interface IFixturesFactory
    {
        Fixture GetFixture(Team homeTeam, Team awayTeam, DateTime startingTime)
    }
}
