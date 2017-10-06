using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class PlayerService : DataService<Player>, IPlayerService
    {
        private readonly IEfRepository<Team> teamsRepo;
        private readonly IEfRepository<Country> countriesRepo;

        public PlayerService(
             IEfRepository<Player> dataSet,
             IEfRepository<Team> teamsRepo,
             IEfRepository<Country> countriesRepo)
            : base(dataSet)
        {
            Guard.WhenArgument(teamsRepo, "teamsRepo").IsNull().Throw();
            Guard.WhenArgument(countriesRepo, "coutriesRepo").IsNull().Throw();

            this.teamsRepo = teamsRepo;
            this.countriesRepo = countriesRepo;
        }

        public void Add(Player playerToAdd, string teamName, string countryName)
        {
            Guard.WhenArgument(playerToAdd, "playerToAdd").IsNull().Throw();
            Guard.WhenArgument(teamName, "teamName").IsNullOrEmpty().Throw();
            Guard.WhenArgument(countryName, "countryName").IsNullOrEmpty().Throw();

            var targetCountry = this.countriesRepo.All.FirstOrDefault(c => c.Name == countryName);
            Guard.WhenArgument(targetCountry, "targetCountry").IsNull().Throw();

            playerToAdd.Country = targetCountry;

            var targetTeam = teamsRepo.All.FirstOrDefault(t => t.Name == teamName);
            Guard.WhenArgument(targetTeam, "targetTeam").IsNull().Throw();

            playerToAdd.Team = targetTeam;

            var isShirtNumberTaken = targetTeam.Players.Any(p => p.ShirtNumber == playerToAdd.ShirtNumber);

            if (isShirtNumberTaken)
            {
                throw new InvalidOperationException("This shirt number is already taken!");
            }

            this.Data.Add(playerToAdd);
            targetTeam.Players.Add(playerToAdd);
        }
    }
}
