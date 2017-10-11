using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.LiveUpdater
{
    public class LiveUpdaterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LiveUpdater";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LiveUpdater_default",
                "LiveUpdater/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}