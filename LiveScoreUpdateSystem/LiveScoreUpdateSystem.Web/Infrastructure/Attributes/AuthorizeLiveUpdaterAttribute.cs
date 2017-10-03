using LiveScoreUpdateSystem.Web.Infrastructure.Enums;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Attributes
{
    public class AuthorizeLiveUpdaterAttribute : AuthorizeAttribute
    {
        public AuthorizeLiveUpdaterAttribute()
        {
            Roles = UserRole.LiveUpdater.ToString();
        }
    }
}