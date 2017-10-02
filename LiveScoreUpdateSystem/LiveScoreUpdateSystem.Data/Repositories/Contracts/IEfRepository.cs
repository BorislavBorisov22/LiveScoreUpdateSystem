using System.Linq;
using LiveScoreUpdateSystem.Data.Models.Contracts;

namespace LivesScoreUpdateSystem.Data.Repositories.Contracts
{
    public interface IEfRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}