using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface ICountryService
    {
        void Add(Country country);

        bool Delete(string countryName);

        void Update(Country updatedModel);

        IEnumerable<Country> GetAll();
    }
}
