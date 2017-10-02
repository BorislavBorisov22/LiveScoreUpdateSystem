using System.Linq;
using LiveScoreUpdateSystem.Data.Models.Contracts;

namespace LiveScoreUpdateSystem.Data.Repositories.Contracts
{
    public interface IEfRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}