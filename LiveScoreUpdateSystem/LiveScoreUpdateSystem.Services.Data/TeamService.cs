using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class TeamService : DataService<Team>, ITeamService
    {
        private readonly IEfRepository<League> leagueRepo;

        public TeamService(IEfRepository<Team> dataSet, IEfRepository<League> leagueRepo) 
            : base(dataSet)
        {
            Guard.WhenArgument(leagueRepo, "League Repo").IsNull().Throw();

            this.leagueRepo = leagueRepo;
        }

        public void Add(Team teamToAdd, string leagueName)
        {
            Guard.WhenArgument(teamToAdd, "teamToAdd").IsNull().Throw();

            var teamAlreadyExists = this.Data.All.Any(t => t.Name == teamToAdd.Name);
            if (teamAlreadyExists)
            {
                throw new InvalidOperationException("Team already exists!");
            }

            var targetLeague = this.leagueRepo.All.FirstOrDefault(l => l.Name == leagueName);
            Guard.WhenArgument(targetLeague, "Target League to add team to").IsNull().Throw();

            teamToAdd.League = targetLeague;

            this.Data.Add(teamToAdd);
        }

        public void Delete(Guid teamId)
        {
            var targetTeam = this.Data.All.FirstOrDefault(t => t.Id == teamId);

            if (targetTeam != null)
            {
                this.Data.Delete(targetTeam);
            }
        }

        public IEnumerable<Team> GetTeamsByLeague(string leagueName)
        {
            var targetLeague = this.leagueRepo.All.FirstOrDefault(l => l.Name == leagueName);

            if (targetLeague == null)
            {
                throw new ArgumentNullException("No league with this name was found!");
            }

            return targetLeague.Teams;
        }

        public IEnumerable<IGrouping<string,Team>> GroupTeamsByLeague()
        {
            var grouped = this.Data
                .All
                .GroupBy(t => t.League.Name);
   
            return grouped;
        }

        public void Update(Guid id, string name, string logoUrl)
        {
            var isNewNameTaken = this.Data
                .All
                .Any(t => t.Name.ToLower() == name.ToLower() && t.Id != id);

            if (isNewNameTaken)
            {
                throw new ArgumentException("There is already a team with name " + name);
            }

            var targetTeamToUpdate = this.Data
                .All
                .FirstOrDefault(t => t.Id == id);

            if (targetTeamToUpdate != null)
            {
                targetTeamToUpdate.Name = name;
                targetTeamToUpdate.LogoUrl = logoUrl;

                this.Data.Update(targetTeamToUpdate);
            }
        }
    }
}
