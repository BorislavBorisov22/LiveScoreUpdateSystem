using LiveScoreUpdateSystem.Web.Infrastructure.Attributes;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SaveChangesAttribute());
        }
    }
}
