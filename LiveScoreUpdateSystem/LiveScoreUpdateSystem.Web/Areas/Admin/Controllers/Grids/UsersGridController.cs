using LiveScoreUpdateSystem.Services.Data;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Grids
{
    public class UsersGridController : AdminController
    {
        private readonly UserService userService;

        public UsersGridController(UserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}