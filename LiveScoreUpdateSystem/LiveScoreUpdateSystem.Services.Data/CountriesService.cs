using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class CountriesService : DataService<Country>, ICountriesService
    {
        public CountriesService(IEfRepository<Country> countriesRepo, ISaveContext saveContext)
            : base(countriesRepo, saveContext)
        {
        }
    }
}
