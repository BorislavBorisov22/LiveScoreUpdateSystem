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
    public class LeagueService : DataService<League>, ILeagueService
    {
        private readonly IEfRepository<Country> countriesRepo;
        private ISaveContext saveContext;

        public LeagueService(IEfRepository<League> leaguesRepo, IEfRepository<Country> countriesRepo, ISaveContext saveContext)
            : base(leaguesRepo)
        {
            Guard.WhenArgument(countriesRepo, "Countries Repo").IsNull().Throw();

            this.saveContext = saveContext;
            this.countriesRepo = countriesRepo;
        }

        public void Add(League leagueToAdd)
        {
            Guard.WhenArgument(leagueToAdd.Country, "LeagueToAdd Country").IsNull().Throw();
            Guard.WhenArgument(leagueToAdd.Country.Name, "LeagueToAdd Country Name").IsNull().Throw();

            var targetCountry = this.countriesRepo
                                .All
                                .FirstOrDefault(c => c.Name == leagueToAdd.Country.Name);

            Guard.WhenArgument(targetCountry, "TargetCountry with such name does not exist").IsNull().Throw();

            var existingLeague = this.Data
                              .All
                              .FirstOrDefault(l => l.Name == leagueToAdd.Name);

            if (existingLeague != null)
            {
                if (existingLeague.Season == leagueToAdd.Season)
                {
                    throw new InvalidOperationException(
                        string.Format("{0} for season {1} has already been added",
                        leagueToAdd.Name,
                        leagueToAdd.Season
                        ));
                }
                else if (existingLeague.Country.Name != leagueToAdd.Country.Name)
                {
                    throw new InvalidOperationException("Cannot add same league in different countries");
                }
            }

            leagueToAdd.Country = targetCountry;
            this.Data.Add(leagueToAdd);
        }

        public bool Delete(string leagueName)
        {
            var targetLeague = this.Data.All.FirstOrDefault(l => l.Name.ToLower() == leagueName.ToLower());

            if (targetLeague == null)
            {
                return false;
            }

            this.Data.Delete(targetLeague);
            return true;
        }

        public void Update(League updatedLeague)
        {
            var targetLeague = this.Data.All.FirstOrDefault(l => l.Id == updatedLeague.Id);

            var isLeagueTaken = this.Data.All.Any(l => l.Name == updatedLeague.Name && l.Season == updatedLeague.Season);

            if (targetLeague != null && !isLeagueTaken)
            {
                targetLeague.Name = updatedLeague.Name;
                targetLeague.Season = updatedLeague.Season;
            }

            this.Data.Update(targetLeague);
        }
    }
}
