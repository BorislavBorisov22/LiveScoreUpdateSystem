using LivesScoreUpdateSystem.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LivesScoreUpdateSystem.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
    }
}
