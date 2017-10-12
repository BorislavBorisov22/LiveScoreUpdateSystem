using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;

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

        public void Delete(Guid playerId)
        {
            var targetPlayer = this.Data.All.FirstOrDefault(p => p.Id == playerId);

            if (targetPlayer != null)
            {
                this.Data.Delete(targetPlayer);
            }
        }

        public IEnumerable<Player> GetAll(Guid teamId)
        {
            var players = this.Data.All.Where(p => p.Team.Id == teamId);
            return players;
        }

        public void Update(Player updatedPlayer)
        {
            var playerToUpdate = this.Data.All.FirstOrDefault(p => p.Id == updatedPlayer.Id);

            if (playerToUpdate != null)
            {
                playerToUpdate.FirstName = updatedPlayer.FirstName;
                playerToUpdate.LastName = updatedPlayer.LastName;
                playerToUpdate.ShirtNumber = updatedPlayer.ShirtNumber;
                playerToUpdate.Age = updatedPlayer.Age;
                playerToUpdate.PictureUrl = updatedPlayer.PictureUrl;
            }

            this.Data.Update(playerToUpdate);
        }
    }
}
