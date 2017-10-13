using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
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

        public FixtureEvent GetFixtureEvent(FixtureEventType fixtureEvent, int minute, Player involvedPlayer)
        {
            return new FixtureEvent()
            {
                FixtureEventType = fixtureEvent,
                InvolvedPlayer = involvedPlayer,
                Minute = minute
            };
        }
    }
}
