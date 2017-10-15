using LiveScoreUpdateSystem.Common;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Attributes
{
    public class AlreadyLoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.TempData[GlobalConstants.ErrorMessage] = "You are alredy logged in!";
                filterContext.HttpContext.Response.Redirect("/");
            }
        }
    }
}