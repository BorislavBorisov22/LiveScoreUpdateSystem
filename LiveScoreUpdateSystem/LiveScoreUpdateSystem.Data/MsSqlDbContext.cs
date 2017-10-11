using LiveScoreUpdateSystem.Data.Models;
using LiveScoreUpdateSystem.Data.Models.Contracts;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace LiveScoreUpdateSystem.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Fixture> Fixtures { get; set; }

        public virtual IDbSet<Player> Players { get; set; }

        public virtual IDbSet<Team> Teams { get; set; }

        public virtual IDbSet<League> Leagues { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public override int SaveChanges()
        {
            try
            {
                this.ApplyAuditInfoRules();
               return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var customException = new FormattedDbEntityValidationException(e);
                throw customException;
            }
        }        
  

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime?))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
