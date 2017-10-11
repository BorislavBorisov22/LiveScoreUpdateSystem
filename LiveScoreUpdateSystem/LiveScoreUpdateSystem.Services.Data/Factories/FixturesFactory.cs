using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Factories.Contracts;
using System;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Factories
{
    public class FixturesFactory : IFixturesFactory
    {
        private const int StartingPlayersCount = 11;

        public Fixture GetFixture(Team homeTeam, Team awayTeam, DateTime? startingTime)
        {
            return new Fixture()
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                FirstHalfStart = startingTime,
                HomeTeamStartingPlayers = homeTeam.Players.Take(StartingPlayersCount).ToList(),
                AwayTeamStartingPlayers = awayTeam.Players.Take(StartingPlayersCount).ToList(),
            };
        }
    }
}
