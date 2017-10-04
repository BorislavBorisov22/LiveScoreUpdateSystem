using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    public abstract class DataService<T> : IDataService<T>
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

        public virtual void Add(T item)
        {
            this.Data.Add(item);
            this.SaveContext.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            var entity = this.GetById(id);

            Guard.WhenArgument(entity, "No entity with such id").IsNull().Throw();

            this.Data.Delete(entity);
            this.SaveContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.Data.All.ToList();
        }

        public virtual T GetById(Guid id)
        {
            return this.Data.All.FirstOrDefault(e => e.Id == id);
        }
    }
}
