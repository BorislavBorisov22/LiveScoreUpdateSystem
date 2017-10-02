using System.Collections.Generic;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using LiveScoreUpdateSystem.Data.Models.Abstraction;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    namespace MvcTemplate.Services.Data
    {
        using System;
        using System.Linq;

        public abstract class DataService<T>
            where T : DataModel
        {
            public DataService(IEfRepository<T> dataSet, ISaveContext saveContext)
            {
                this.Data = dataSet ?? throw new ArgumentNullException("Passed Repository canot be null!");
                this.SaveContext = saveContext ?? throw new ArgumentNullException("Passed Save Context cannot be null!");
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
                var entity = this.GetById(id) ?? 
                    throw new InvalidOperationException("No entity with provided id found.");

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
}
