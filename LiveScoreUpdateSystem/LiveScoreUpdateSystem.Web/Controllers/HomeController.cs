using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Web.Controllers.Abstraction;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration =GlobalConstants.HomePageCacheDuration)]
        [ChildActionOnly]
        public ActionResult HomePage()
        {
            return this.PartialView(PartialViews.HomePagePartial);
        }

        [OutputCache(Duration =GlobalConstants.AboutPageCacheDuration)]
        [ChildActionOnly]
        public ActionResult AboutPage()
        {
            return this.PartialView(PartialViews.AboutPagePartial);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}