using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction
{
    [AuthorizeAdmin]
    public abstract class AdminController : BaseController
    {
    }
}