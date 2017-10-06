using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class LeagueService : DataService<League>, ILeagueService
    {
        private readonly IEfRepository<Country> countriesRepo;

        public LeagueService(IEfRepository<League> leaguesRepo, IEfRepository<Country> countriesRepo)
            : base(leaguesRepo)
        {
            Guard.WhenArgument(countriesRepo, "Countries Repo").IsNull().Throw();

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

        public IEnumerable<League> GetAll()
        {
            return this.Data.All;
        }
    }
}
