using LiveScoreUpdateSystem.Common;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Attributes
{
    public class AuthorizeLiveUpdaterAttribute : AuthorizeAttribute
    {
        public AuthorizeLiveUpdaterAttribute()
        {
            Roles = GlobalConstants.LiveUpdaterRole;
        }
    }
}