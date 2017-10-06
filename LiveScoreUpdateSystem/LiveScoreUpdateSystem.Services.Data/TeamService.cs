using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using Bytes2you.Validation;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class TeamService : DataService<Team>
    {
        private readonly IEfRepository<League> leagueRepo;

        public TeamService(IEfRepository<Team> dataSet, IEfRepository<League> leagueRepo, ISaveContext saveContext) 
            : base(dataSet, saveContext)
        {
            Guard.WhenArgument(leagueRepo, "League Repo").IsNull().Throw();

            this.leagueRepo = leagueRepo;
        }

        public void Add(Team teamToAdd)
        {
            Guard.WhenArgument(teamToAdd, "teamToAdd").IsNull().Throw();
            Guard.WhenArgument(teamToAdd.League, "teamToAdd League").IsNull().Throw();
            Guard.WhenArgument(teamToAdd.League.Name, "teamToAdd League name").IsNull().Throw();

            var targetLeague = this.leagueRepo.All.FirstOrDefault(l => l.Name == teamToAdd.League.Name);

            Guard.WhenArgument(targetLeague, "Target League to add team to").IsNull().Throw();

            var teamAlreadyExists = this.Data.All.Any(t => t.Name == teamToAdd.Name);

            if (teamAlreadyExists)
            {
                throw new InvalidOperationException("Team already exists!");
            }

            teamToAdd.League = targetLeague;
            this.Data.Add(teamToAdd);
        }
    }
}
