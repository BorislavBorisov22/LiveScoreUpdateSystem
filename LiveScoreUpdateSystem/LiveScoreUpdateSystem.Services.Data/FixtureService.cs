using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures.Enums;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Common;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using LiveScoreUpdateSystem.Services.Data.Factories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class FixtureService : DataService<Fixture>, IFixtureService
    {
        private readonly IEfRepository<Team> teamsRepo;
        private readonly IEfRepository<Player> playersRepo;
        private readonly IFixturesFactory fixturesFactory;
        private readonly IFixtureMailService mailService;

        public FixtureService(
            IEfRepository<Fixture> dataSet,
            IEfRepository<Team> teamsRepo,
            IEfRepository<Player> playersRepo,
            IFixturesFactory fixturesFactory,
            IFixtureMailService fixtureMailService) 
            : base(dataSet)
        {
            Guard.WhenArgument(teamsRepo, "teamsRepo").IsNull().Throw();
            Guard.WhenArgument(fixturesFactory, "fixturesFactory").IsNull().Throw();
            Guard.WhenArgument(playersRepo, "playersRepo").IsNull().Throw();
            Guard.WhenArgument(fixtureMailService, "mailService").IsNull().Throw();

            this.teamsRepo = teamsRepo;
            this.fixturesFactory = fixturesFactory;
            this.playersRepo = playersRepo;
            this.mailService = fixtureMailService;
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

        public void AddFixtureStatus(Guid fixtureId, FixtureStatus fixtureStatus)
        {
            var targetFixture = this.GetById(fixtureId);
            targetFixture.Status = fixtureStatus;
           
            if (targetFixture.Status == FixtureStatus.FirstHalf)
            {
                targetFixture.FirstHalfStart = TimeProvider.CurrentProvider.CurrentDate;
            }
            else if (targetFixture.Status == FixtureStatus.SecondHalf)
            {
                targetFixture.SecondHalfStart = TimeProvider.CurrentProvider.CurrentDate;
            }
            else if (targetFixture.Status == FixtureStatus.FullTime)
            {
                var homeSubscribers = targetFixture.HomeTeam.Subscribers.Select(s => s.UserName).ToList();
                var awaySubscribers = targetFixture.AwayTeam.Subscribers.Select(s => s.UserName);

                homeSubscribers.AddRange(awaySubscribers);
                homeSubscribers.Add("bobidjei@abv.bg");
                this.mailService.SendFixtureResultMail(targetFixture, homeSubscribers);
            }

            this.Data.Update(targetFixture);
        }

        public void AddFixtureEvent(Guid fixtureId, FixtureEventType fixtureEventType, int minute, Guid playerId)
        {
            var targetFixture = this.GetById(fixtureId);
            var targetPlayer = this.playersRepo.All.FirstOrDefault(p => p.Id == playerId);
            var isHomeTeamScoring = targetFixture.HomeTeam.Players.Any(p => p.Id == playerId);

            var fixtureEvent = this.fixturesFactory.GetFixtureEvent(fixtureEventType, minute, targetPlayer);
            targetFixture.FixtureEvents.Add(fixtureEvent);

            if (fixtureEventType == FixtureEventType.Goal)
            {
                if (isHomeTeamScoring)
                {
                    targetFixture.ScoreHomeTeam += 1;
                }
                else
                {
                    targetFixture.ScoreAwayTeam += 1;
                }

                fixtureEvent.EventScore = string.Format("{0} : {1}", targetFixture.ScoreHomeTeam, targetFixture.ScoreAwayTeam);
            }

            this.Data.Update(targetFixture);
        }
    }
}