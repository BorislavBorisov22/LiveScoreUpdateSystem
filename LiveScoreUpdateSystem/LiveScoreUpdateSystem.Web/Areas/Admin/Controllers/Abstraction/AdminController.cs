using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction
{
    [AuthorizeAdmin]
    public abstract class AdminController : Controller
    {
    }
}