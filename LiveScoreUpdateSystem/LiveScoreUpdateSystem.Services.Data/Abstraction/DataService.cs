using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    public class DataService<T>
        where T : DataModel
    {
        public DataService(IEfRepository<T> dataSet, ISaveContext saveContext)
        {
            Guard.WhenArgument(dataSet, "IEfRepository dataSet").IsNull().Throw();
            Guard.WhenArgument(saveContext, "SaveContext").IsNull().Throw();

            this.Data = dataSet;
            this.SaveContext = saveContext;
        }
         
        protected IEfRepository<T> Data { get; private set; }

        protected ISaveContext SaveContext { get; private set; }
    }
}
