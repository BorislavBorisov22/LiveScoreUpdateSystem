using LiveScoreUpdateSystem.Data.Models;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEfRepository<User> usersRepo;
  
        public HomeController(IEfRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public ActionResult Index()
        {
            return View();
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