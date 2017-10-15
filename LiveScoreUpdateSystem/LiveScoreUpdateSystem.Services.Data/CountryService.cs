using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class CountryService : DataService<Country>, ICountryService
    {
        public CountryService(IEfRepository<Country> dataSet) 
            : base(dataSet)
        {
        }

        public void Add(Country country)
        {
            Guard.WhenArgument(country, "Country").IsNull().Throw();

            this.Data.Add(country);
        }

        public bool Delete(string countryName)
        {
            var targetCountry = this.Data.All.FirstOrDefault(c => c.Name == countryName);

            if (targetCountry == null)
            {
                return false;
            }

            this.Data.Delete(targetCountry);
            return true;
        }

        public void Update(Country updatedModel)
        {
            var modelToUpdate = this.Data.All.FirstOrDefault(m => m.Id == updatedModel.Id);

            if (modelToUpdate != null)
            {
                modelToUpdate.FlagPictureUrl = updatedModel.FlagPictureUrl;
                modelToUpdate.Name = updatedModel.Name;

                this.Data.Update(modelToUpdate);
            }
        }
    }
}
