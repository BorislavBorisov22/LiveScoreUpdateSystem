using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers.Abstraction
{
    [AuthorizeLiveUpdater]
    public class LiveUpdaterController : BaseController
    {
    }
}