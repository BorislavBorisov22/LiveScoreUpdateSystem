using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Services.Data.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface ICountriesService : IDataService<Country>
    {
    }
}
