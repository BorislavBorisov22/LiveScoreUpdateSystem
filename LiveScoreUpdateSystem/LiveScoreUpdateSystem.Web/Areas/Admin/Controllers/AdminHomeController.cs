using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}