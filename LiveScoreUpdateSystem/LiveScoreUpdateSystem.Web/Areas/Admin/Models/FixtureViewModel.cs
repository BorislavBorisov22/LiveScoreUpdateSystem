using System.Collections.Generic;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Areas.Admin.Models
{
    public class FixtureViewModel
    {
        public IEnumerable<SelectListItem> LeaguesAvailable { get; set; }
    }
}