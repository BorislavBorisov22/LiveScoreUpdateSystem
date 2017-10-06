using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    public class DataService<T>
        where T : DataModel
    {
        public DataService(IEfRepository<T> dataSet)
        {
            Guard.WhenArgument(dataSet, "IEfRepository dataSet").IsNull().Throw();

            this.Data = dataSet;
        }
         
        protected IEfRepository<T> Data { get; private set; }
    }
}
