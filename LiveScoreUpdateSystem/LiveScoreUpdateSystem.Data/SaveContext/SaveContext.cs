using System;
using System.Data.Entity;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;

namespace LiveScoreUpdateSystem.Data.Repositories.SaveContext
{
    public class SaveContext : ISaveContext
    {
        private readonly DbContext context;

        public SaveContext(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException("DbContext cannot be null!");
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
