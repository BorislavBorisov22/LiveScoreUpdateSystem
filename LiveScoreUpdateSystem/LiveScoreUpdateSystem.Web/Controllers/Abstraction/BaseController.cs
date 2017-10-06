using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Controllers.Abstraction
{
    [SaveChanges]
    public abstract class BaseController : Controller
    {
    }
}