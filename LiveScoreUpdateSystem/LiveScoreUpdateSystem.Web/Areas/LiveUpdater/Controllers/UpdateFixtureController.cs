using LiveScoreUpdateSystem.Common;
using LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers.Abstraction;
using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater.Controllers
{
    public class UpdateFixtureController : LiveUpdaterController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult Update(string teamName, Guid fixtureId)
        {
            return this.PartialView(PartialViews.UpdateFixtureFormPartial);
        }
    }
}