using LiveScoreUpdateSystem.Web.Infrastructure.Enums;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        public AuthorizeAdminAttribute()
        {
            Roles = UserRole.Admin.ToString();
        }
    }
}