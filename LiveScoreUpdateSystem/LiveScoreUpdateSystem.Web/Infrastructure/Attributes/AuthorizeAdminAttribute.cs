using LiveScoreUpdateSystem.Common;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminAttribute()
        {
            Roles = GlobalConstants.AdminRole;
        }
    }
}