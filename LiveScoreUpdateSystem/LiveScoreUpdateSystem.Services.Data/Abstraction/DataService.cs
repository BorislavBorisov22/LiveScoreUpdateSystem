using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.Contracts;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    public class DataService<T>
        where T : class, IAuditable, IDeletable, IDataModel
    {
        public DataService(IEfRepository<T> dataSet)
        {
            Guard.WhenArgument(dataSet, "IEfRepository dataSet").IsNull().Throw();

            this.Data = dataSet;
        }

        protected IEfRepository<T> Data { get; private set; }

        public IEnumerable<T> GetAll()
        {
            return this.Data.All.ToList();
        }

        public T GetById(Guid id)
        {
            var target = this.Data.All.FirstOrDefault(e => e.Id == id);
            if (target == null)
            {
                throw new ArgumentNullException("Id does not target any entity!");
            }

            return target;
        }
    }
}
