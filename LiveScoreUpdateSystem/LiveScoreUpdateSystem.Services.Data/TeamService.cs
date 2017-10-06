using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
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

        public TeamService(IEfRepository<Team> dataSet, IEfRepository<League> leagueRepo, ISaveContext saveContext) 
            : base(dataSet, saveContext)
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
            this.SaveContext.SaveChanges();
        }

        public IEnumerable<Team> GetAll()
        {
            return this.Data.All.ToList();
        }

        public IEnumerable<IGrouping<string,Team>> GroupTeamsByLeague()
        {
            var grouped = this.Data
                .All
                .GroupBy(t => t.League.Name);
   
            return grouped;
        }
    }
}
