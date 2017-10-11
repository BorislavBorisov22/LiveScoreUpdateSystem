using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Services.Data.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class FixtureService : DataService<Fixture>, IFixtureService
    {
        private readonly IEfRepository<Team> teamsRepo;
        private readonly IFixturesFactory fixturesFactory;

        public FixtureService(IEfRepository<Fixture> dataSet, IEfRepository<Team> teamsRepo, IFixturesFactory fixturesFactory) 
            : base(dataSet)
        {
            Guard.WhenArgument(teamsRepo, "teamsRepo").IsNull().Throw();
            Guard.WhenArgument(fixturesFactory, "fixturesFactory").IsNull().Throw();

            this.teamsRepo = teamsRepo;
            this.fixturesFactory = fixturesFactory;
        }

        public void Add(string homeTeamName, string awayTeamName, DateTime? startTime)
        {
            if (homeTeamName == awayTeamName)
            {
                throw new InvalidOperationException("Home and away team must be different!");
            }

            var homeTeam = this.GetTeamByName(homeTeamName);
            var awayTeam = this.GetTeamByName(awayTeamName);

            var fixture = this.fixturesFactory.GetFixture(homeTeam, awayTeam, startTime);
            this.Data.Add(fixture);
        }

        private Team GetTeamByName(string teamName)
        {
            var targetTeam = this.teamsRepo.All.FirstOrDefault(t => t.Name == teamName);
            if (targetTeam == null)
            {
                throw new ArgumentNullException(string.Format("No team with name {0} was found!", teamName));
            }

            return targetTeam;
        }

        public IEnumerable<Fixture> GetAvailableFixtures(DateTime targetDate)
        {
            var availableFixtures = this.Data.All
                .Where(f => f.FirstHalfStart.Value.Month == targetDate.Month &&
                f.FirstHalfStart.Value.Year == targetDate.Year &&
                f.FirstHalfStart.Value.Day == targetDate.Day);

            return availableFixtures;
        }
    }
}