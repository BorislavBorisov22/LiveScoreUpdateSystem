using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Web.Areas.Admin.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Areas.Admin.Models;
using System.Web.Mvc.Expressions;
using System.Web.Mvc;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnlyAttribute]
        public ActionResult AddCountry()
        {
            return this.PartialView(PartialViews.AddCountry);
        }
         
        [HttpPost]
        public ActionResult AddCountry(CountryViewModel countryModel)
        {
            return this.RedirectToAction(c => c.Index());
        }
    }
}