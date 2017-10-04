using System;
using System.Collections.Generic;
using LiveScoreUpdateSystem.Data.Models.Abstraction;

namespace LiveScoreUpdateSystem.Services.Data.Abstraction
{
    public interface IDataService<T> where T : DataModel
    {
        void Add(T item);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}