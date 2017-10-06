using System;
using System.Data.Entity;
using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using Bytes2you.Validation;

namespace LiveScoreUpdateSystem.Data.Repositories.SaveContext
{
    public class SaveContext : ISaveContext
    {
        private readonly DbContext context;

        public SaveContext(DbContext context)
        {
            Guard.WhenArgument(context, "DbContext").IsNull().Throw();
        
            this.context = context;
        }

        public void SaveChanges()
        {
            if (!this.context.ChangeTracker.HasChanges())
            {
                return;
            }

            this.context.SaveChanges();
        }
    }
}
