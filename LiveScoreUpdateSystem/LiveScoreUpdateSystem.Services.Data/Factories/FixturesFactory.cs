using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Factories
{
    public class FixturesFactory
    {
        private const int StartingPlayersCount = 11;

        public Fixture GetFixture(Team homeTeam, Team awayTeam, DateTime startingTime)
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
