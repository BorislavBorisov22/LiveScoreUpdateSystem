using LiveScoreUpdateSystem.Data.Models.Contracts;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;

namespace LiveScoreUpdateSystem.Services.Data.Tests.Abstraction.DataServiceTests.Fakes
{
    internal class FakeDateService<T> : DataService<T>
        where T : class, IAuditable, IDeletable, IDataModel
    {
        public FakeDateService(IEfRepository<T> dataSet) 
            : base(dataSet)
        {
        }

        public IEfRepository<T> DataTest
        {
            get
            {
                return base.Data;
            }
        }
    }
}
