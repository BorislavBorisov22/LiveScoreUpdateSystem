using Kendo.Mvc.UI;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids
{
    public class PlayersGridController : AdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadPlayers([DataSourceRequest] DataSourceRequest request)
        {

        }
    }
}