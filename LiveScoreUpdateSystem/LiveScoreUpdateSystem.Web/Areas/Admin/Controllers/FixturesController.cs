using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class FixturesController : AdminController
    {
        [HttpGet]
        public ActionResult AddFixture()
        {
            return this.View();
        }
    }
}